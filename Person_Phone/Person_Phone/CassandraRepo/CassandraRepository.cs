using Person_Phone.Entities;
using PersonDB_project;
using PersonDB_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Person_Phone;
using Cassandra;

namespace Person_Phone.CassandraRepo
{
    public class CassandraRepository : IRepository<User>
    {
        Cluster cluster;
        ISession session; 
        
        public CassandraRepository()
        {
            cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            session = cluster.Connect("users");
        }

        public void Create(User user)
        {
            CassandraUser csUser = UserToCassandraUser(user);

            string hashString = csUser.FirstName + csUser.LastName + csUser.Age.ToString();
            csUser.Id = hashString.GetHashCode();

            session.Execute("insert into users (Id, FirstName, LastName, Age) values (" + csUser.Id + ",'" + csUser.FirstName + "','" + csUser.LastName + "'," + csUser.Age + ")");

            foreach (CassandraPhone phone in csUser.Phones)
            {
                session.Execute("insert into phones (id, phonetype, phonenumber, userid) values (" + phone.Id + ",'" + phone.PhoneType + "','" + phone.PhoneNumber + "'," + csUser.Id + ")");
            }
            
        }

        public void Delete(int id)
        {
            session.Execute("delete from users where ID = " + id);
            session.Execute("delete from phones where userid = " + id);
        }

        public IEnumerable<User> GetAll()
        {
            List<CassandraUser> csList = new List<CassandraUser>();
            List<User> users = new List<User>();

            var records = session.Execute("select * from users");

            foreach (var row in records)
            {
                    csList.Add(new CassandraUser
                    {
                        Id = row.GetValue<int>("id")
                        ,
                        FirstName = row.GetValue<string>("firstname")
                        ,
                        LastName = row.GetValue<string>("lastname")
                        ,
                        Age = row.GetValue<int>("age")
                    });
            }
            foreach (var csUser in csList)
            {
                User user = CassandraUserToUser(csUser);
                users.Add(user);
            }

            return users;
        }
    

        public User GetById(int id)
        {
            var record = session.Execute("select * from users where id = " + id).FirstOrDefault();

            CassandraUser csUser = new CassandraUser
                    {
                        Id = record.GetValue<int>("id")
                        ,
                        FirstName = record.GetValue<string>("firstname")
                        ,
                        LastName = record.GetValue<string>("lastname")
                        ,
                        Age = record.GetValue<int>("age")
                    };
            User user = CassandraUserToUser(csUser);

            return user;
        }

        public void Save()
        {
            return;
        }

        public void Update(User item)
        {
            CassandraUser csUser = UserToCassandraUser(item);
            session.Execute("update users set firstname = '" + csUser.FirstName + "', lastname = '" + csUser.LastName + "', age = " + csUser.Age + " where ID = " + csUser.Id);
        }

        private User CassandraUserToUser(CassandraUser csUser)
        {
            User user = new User();

            user.Id = csUser.Id;
            user.FirstName = csUser.FirstName;
            user.LastName = csUser.LastName;
            user.Age = csUser.Age;

            foreach (var csPhone in csUser.Phones)
            {
                Phone phone = CassandraPhoneToPhone(csPhone);
                user.Phones.Add(phone);
            }

            return user;
        }

        private Phone CassandraPhoneToPhone(CassandraPhone csPhone)
        {
            Phone phone = new Phone();

            phone.Id = csPhone.Id;
            phone.PhoneNumber = csPhone.PhoneNumber;
            phone.PhoneType = csPhone.PhoneType;

            return phone;
        }

        private CassandraUser UserToCassandraUser(User user)
        {
            CassandraUser csUser = new CassandraUser();

            csUser.Id = user.Id;
            csUser.FirstName = user.FirstName;
            csUser.LastName = user.LastName;
            csUser.Age = user.Age;

            foreach (Phone phone in user.Phones)
            {
                CassandraPhone csPhone = PhoneToCassandraPhone(phone);
                csUser.Phones.Add(csPhone);
            }

            return csUser;
        }

        private CassandraPhone PhoneToCassandraPhone(Phone phone)
        {
            CassandraPhone csPhone = new CassandraPhone();

            csPhone.Id = phone.Id;
            csPhone.PhoneNumber = phone.PhoneNumber;
            csPhone.PhoneType = phone.PhoneType;

            return csPhone;
        }

    }
}