using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using PersonDB_project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace PersonDB_project.AdoRepo
{
    public class AdoNetRepository : IRepository<User>
    {
        private Database _sqlDB;

        private readonly String _connection;

        public AdoNetRepository(String connection)
        {
            _connection = connection;
            _sqlDB = new SqlDatabase(_connection);
        }

        public IEnumerable<User> GetAll()
        {
            string sqlStatement = "SELECT * FROM [Users] ";
            using (DbCommand sqlCmd = _sqlDB.GetSqlStringCommand(sqlStatement))
            {
                var result = new List<User>();
                using (IDataReader reader = _sqlDB.ExecuteReader(sqlCmd))
                {
                    result = Mapper(reader);
                }
                return result;
            }
        }

        public User GetById(int id)
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

        public void Create(User user)
        {
            string sqlStatement = "INSERT INTO [Users] ([Age],[FirstName],[LastName])" +
       "VALUES (@Age,@FirstName,@LastName)";
            using (DbCommand sqlCmd = _sqlDB.GetSqlStringCommand(sqlStatement))
            {
                _sqlDB.AddInParameter(sqlCmd, "Age", DbType.Int32, user.Age);
                _sqlDB.AddInParameter(sqlCmd, "FirstName", DbType.String, user.FirstName);
                _sqlDB.AddInParameter(sqlCmd, "LastName", DbType.String, user.LastName);
                using (_sqlDB.ExecuteReader(sqlCmd))
                {
                    return;
                }

            }

        }

        public void Update(User user)
        {
            var sqlStatement = "UPDATE [Users] SET FirstName=@FirstName,LastName=@LastName,Age=@Age WHERE Id LIKE @Id";
            using (DbCommand sqlCmd = _sqlDB.GetSqlStringCommand(sqlStatement))
            {
                _sqlDB.AddInParameter(sqlCmd, "Age", DbType.Int32, user.Age);
                _sqlDB.AddInParameter(sqlCmd, "FirstName", DbType.String, user.FirstName);
                _sqlDB.AddInParameter(sqlCmd, "LastName", DbType.String, user.LastName);
                _sqlDB.AddInParameter(sqlCmd, "Id", DbType.String, user.Id);

                using (_sqlDB.ExecuteReader(sqlCmd))
                {
                    return;
                }
            }
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
        private List<User> Mapper(IDataReader reader)
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
    }
}