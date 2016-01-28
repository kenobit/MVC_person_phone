using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Person_Phone;
using PersonDB_project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PersonDB_project.AdoRepo
{
    public class AdoNetRepository : IRepository<User>
    {
        private Database _sqlDB;
        private SqlConnection _sqlCon;

        private readonly String _connection;

        public AdoNetRepository(String connection)
        {
            _sqlCon = new SqlConnection(connection);
            _connection = connection;
            _sqlDB = new SqlDatabase(_connection);
        }

        public IEnumerable<User> GetAll()
        {
            List<User> users = GetAllUsers().ToList();
            List<Phone> phones = GetAllPhones().ToList();

            foreach (User user in users)
            {
                List<Phone> current = phones.Where(x => x.UserId == user.Id).ToList();
                foreach (Phone phone in current)
                {
                    user.Phones.Add(phone);
                }
            }

            return users;
        }
        private IEnumerable<User> GetAllUsers()
        {
            var result = new List<User>();
            string sqlStatement = "SELECT * FROM [Users]";
            using (DbCommand sqlCmd = _sqlDB.GetSqlStringCommand(sqlStatement))
            {

                using (IDataReader reader = _sqlDB.ExecuteReader(sqlCmd))
                {
                    result = MapperUser(reader);
                }
            }

            return result;
        }

        private IEnumerable<Phone> GetAllPhones()
        {
            var result = new List<Phone>();
            string sqlStatement = "SELECT * FROM [PhoneViewModels]";
            using (DbCommand sqlCmd = _sqlDB.GetSqlStringCommand(sqlStatement))
            {

                using (IDataReader reader = _sqlDB.ExecuteReader(sqlCmd))
                {
                    result = MapperPhone(reader);
                }
            }

            return result;
        }

        public User GetById(int id)
        {
            User user = GetUserById(id);
            foreach (Phone phone in GetAllPhones())
            {
                if (phone.UserId == user.Id)
                {
                    user.Phones.Add(phone);
                }
            }
            return user;
        }
        private User GetUserById(int id)
        {
            string sqlStatement = "SELECT * FROM [Users] WHERE Id LIKE @Id";
            using (DbCommand sqlCmd = _sqlDB.GetSqlStringCommand(sqlStatement))
            {
                _sqlDB.AddInParameter(sqlCmd, "Id", DbType.Int32, id);
                using (IDataReader reader = _sqlDB.ExecuteReader(sqlCmd))
                {
                    var user = MapperForSingleUser(reader);
                    return user;
                }
            }
        }

        private List<Phone> GetPhonesByUserId(int userId)
        {
            List<Phone> phones = new List<Phone>();

            string sqlStatement = "SELECT * FROM [PhoneViewModels] WHERE UserId LIKE @UserId";
            using (DbCommand sqlCmd = _sqlDB.GetSqlStringCommand(sqlStatement))
            {
                _sqlDB.AddInParameter(sqlCmd, "UserId", DbType.Int32, userId);
                using (IDataReader reader = _sqlDB.ExecuteReader(sqlCmd))
                {
                    var phone = MapperForSinglePhone(reader);
                    phones.Add(phone);
                }
            }

            return phones;
        }

        public void Create(User person)
        {
            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();

                var command = new SqlCommand
                {
                    CommandText = "INSERT INTO Users (FirstName, LastName, Age) VALUES (@FirstName, @LastName, @Age); " +
                                  @"SELECT @Id = SCOPE_IDENTITY()",
                    Connection = connection
                };

                var firstNameParam = new SqlParameter("@FirstName", person.FirstName);
                command.Parameters.Add(firstNameParam);

                var lastNameParam = new SqlParameter("@LastName", person.LastName);
                command.Parameters.Add(lastNameParam);

                var ageParam = new SqlParameter("@Age", person.Age);
                command.Parameters.Add(ageParam);

                var output = new SqlParameter(@"Id", SqlDbType.Int);
                command.Parameters.Add(output);
                output.Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();

                foreach (var phone in person.Phones)
                {
                    var phoneCommand = new SqlCommand
                    {
                        CommandText = "INSERT INTO PhoneViewModels (PhoneNumber, PhoneType, UserId) VALUES (@PhoneNumber, @PhoneType, @UserId)",
                        Connection = connection
                    };

                    var numberParam = new SqlParameter("@PhoneNumber", phone.PhoneNumber);
                    phoneCommand.Parameters.Add(numberParam);

                    var typeParam = new SqlParameter("@PhoneType", phone.PhoneType);
                    phoneCommand.Parameters.Add(typeParam);

                    var personIdParam = new SqlParameter("@UserId", Convert.ToInt32(output.Value));
                    phoneCommand.Parameters.Add(personIdParam);

                    phoneCommand.ExecuteNonQuery();
                }
            }
        }

        private int CreateUser(User user)
        {
            int res = 0;
            string sqlStatement = "INSERT INTO [Users] ([Age],[FirstName],[LastName])" +
       "VALUES (@Age,@FirstName,@LastName);";
            using (DbCommand sqlCmd = _sqlDB.GetSqlStringCommand(sqlStatement))
            {
                sqlCmd.CommandText += "; SELECT SCOPE_IDENTITY();";
                _sqlDB.AddInParameter(sqlCmd, "Age", DbType.Int32, user.Age);
                _sqlDB.AddInParameter(sqlCmd, "FirstName", DbType.String, user.FirstName);
                _sqlDB.AddInParameter(sqlCmd, "LastName", DbType.String, user.LastName);
                using (_sqlDB.ExecuteReader(sqlCmd))
                {
                    res = Convert.ToInt32(sqlCmd.ExecuteScalar());
                }
            }
            return res;
        }

        private void CreatePhone(Phone phone)
        {
            string sqlStatement = "INSERT INTO [PhoneViewModels] ([PhoneType],[PhoneNumber],[UserId])" +
       "VALUES (@PhoneType,@PhoneNumber,@UserId);";
            using (DbCommand sqlCmd = _sqlDB.GetSqlStringCommand(sqlStatement))
            {
                _sqlDB.AddInParameter(sqlCmd, "PhoneType", DbType.String, phone.PhoneType);
                _sqlDB.AddInParameter(sqlCmd, "PhoneNumber", DbType.String, phone.PhoneNumber);
                _sqlDB.AddInParameter(sqlCmd, "UserId", DbType.Int32, phone.user.Id);
                using (_sqlDB.ExecuteReader(sqlCmd))
                {
                    return;
                }
            }
        }

        //public void Create(User user)
        //{
        //    int userId = CreateUser(user);
        //    foreach (Phone phone in user.Phones)
        //    {
        //        phone.user = user;
        //        CreatePhone(phone);
        //    }

        //}

        public void Update(User user)
        {
            var sqlStatement = "UPDATE [Users] SET FirstName=@FirstName,LastName=@LastName,Age=@Age WHERE Id LIKE @Id";
            using (DbCommand sqlCmd = _sqlDB.GetSqlStringCommand(sqlStatement))
            {
                _sqlDB.AddInParameter(sqlCmd, "Age", DbType.Int32, user.Age);
                _sqlDB.AddInParameter(sqlCmd, "FirstName", DbType.String, user.FirstName);
                _sqlDB.AddInParameter(sqlCmd, "LastName", DbType.String, user.LastName);
                _sqlDB.AddInParameter(sqlCmd, "Id", DbType.String, user.Id);

                _sqlDB.ExecuteReader(sqlCmd);
            }

            PhonesUpdate(user);

            return;
        }

        public void PhonesUpdate(User user)
        {
            foreach (Phone phone in user.Phones)
            {
                var sqlStatement = "UPDATE [PhoneViewModels] SET PhoneType=@PhoneType,PhoneNumber=@PhoneNumber,UserId=@UserId WHERE Id LIKE @Id";
                using (DbCommand sqlCmd = _sqlDB.GetSqlStringCommand(sqlStatement))
                {
                    _sqlDB.AddInParameter(sqlCmd, "UserId", DbType.Int32, phone.UserId);
                    _sqlDB.AddInParameter(sqlCmd, "PhoneType", DbType.String, phone.PhoneType);
                    _sqlDB.AddInParameter(sqlCmd, "PhoneNumber", DbType.String, phone.PhoneNumber);
                    _sqlDB.AddInParameter(sqlCmd, "Id", DbType.String, phone.Id);

                    _sqlDB.ExecuteReader(sqlCmd);
                }
            }

            return;
        }

        public void Delete(int id)
        {
            User user = GetById(id);
            var sqlStatement = "DELETE FROM [Users] WHERE Id Like @Id";
            using (DbCommand sqlCmd = _sqlDB.GetSqlStringCommand(sqlStatement))
            {
                _sqlDB.AddInParameter(sqlCmd, "Id", DbType.Int32, user.Id);
                using (IDataReader reader = _sqlDB.ExecuteReader(sqlCmd))
                {

                }
            }
        }

        public void Save()
        {
            return;
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    Dispose(true);
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private User MapperForSingleUser(IDataReader reader)
        {
            while (reader.Read())
            {
                var user = new User
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                };
                return user;
            }
            return new User();
        }

        private Phone MapperForSinglePhone(IDataReader reader)
        {
            while (reader.Read())
            {
                var phone = new Phone
                {
                    Id = (int)reader["Id"],
                    PhoneType = (string)reader["PhoneType"],
                    PhoneNumber = (string)reader["PhoneNumber"],
                    UserId = (int)reader["UserId"]
                };
                return phone;
            }
            return new Phone();
        }

        private List<User> MapperUser(IDataReader reader)
        {
            var list = new List<User>();
            while (reader.Read())
            {
                var user = new User
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"],
                };
                list.Add(user);
            }
            return list;
        }

        private List<Phone> MapperPhone(IDataReader reader)
        {
            var list = new List<Phone>();
            while (reader.Read())
            {
                var phone = new Phone
                {
                    Id = (int)reader["Id"],
                    PhoneNumber = (string)reader["PhoneNumber"],
                    PhoneType = (string)reader["PhoneType"],
                    UserId = (int)reader["UserId"]
                };
                list.Add(phone);
            }
            return list;
        }

        //private List<User> MapperPhones(IDataReader reader)
        //{
        //    var list = new List<User>();
        //    while (reader.Read())
        //    {
        //        var user = new User
        //        {
        //            Id = (int)reader["Id"],
        //            FirstName = (string)reader["FirstName"],
        //            LastName = (string)reader["LastName"],
        //            Age = (int)reader["Age"],
        //        };
        //        list.Add(user);
        //    }
        //    return list;
        //}
    }
}