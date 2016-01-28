using Neo4jClient;
using Person_Phone.Entities;
using PersonDB_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Person_Phone.CyberRepo
{
    public class CyberRepository : IRepository<User>
    {
        GraphClient client;
        public CyberRepository()
        {
            client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "12345");
            client.Connect();
        }


        public void Create(User item)
        {
            ICollection<Phone> temp_phones = item.Phones;
            item.Phones = null;

            string hashString = item.FirstName + item.LastName + item.Age.ToString();
            item.Id = hashString.GetHashCode();

            client.Cypher
                .Create("(user:User {newUser})")
                .WithParam("newUser", item)
                .ExecuteWithoutResults();

            foreach (Phone phone in temp_phones)
            {
                phone.UserId = item.Id;

                client.Cypher
                    .Match("(owner:User)")
                    .Where((User owner) => owner.Id == item.Id)
                    .Create("owner-[:OWNED]->(owned:Phone {newPhone})")
                    .WithParam("newPhone", phone)
                    .ExecuteWithoutResults();
            }
        }

        public void Delete(int id)
        {
            client.Cypher
                  .OptionalMatch("(user:User)<-[r]->()")
                  .Where((User user) => user.Id == id)
                  .Delete("r, user")
                  .ExecuteWithoutResults();

            client.Cypher
                 .OptionalMatch("(phone:Phone)")
                 .Where((Phone phone) => phone.UserId == id)
                 .Delete("phone")
                 .ExecuteWithoutResults();
        }

        public IEnumerable<User> GetAll()
        {
            var result = client.Cypher
                 .OptionalMatch("(user:User)-[OWNED]-(phone:Phone)")
                 .Return((user, phone) => new
                 {
                     User = user.As<User>(),
                     Phones = phone.CollectAs<Phone>()
                 })
                 .Results;
            ICollection<User> users = new List<User>();

            foreach (var item in result)
            {
                item.User.Phones = (List<Phone>)item.Phones;
                users.Add(item.User);
            }
            return users;
        }

        public User GetById(int id)
        {
            var result = client.Cypher
                .OptionalMatch("(user:User)-[OWNED_BY]-(friend:Phone)")
                .Where((User user) => user.Id == id)
                .Return((user, friend) => new
                {
                    User = user.As<User>(),
                    Friends = friend.CollectAs<Phone>()
                })
                .Results.FirstOrDefault();

            User newUser = new User();

            newUser = result.User;
            newUser.Phones = (result.Friends as List<Phone>);

            return newUser;
        }

        public void Save()
        {
            return;
        }

        public void Update(User item)
        {
            //client.Cypher
            //    .Match("(user:User)")
            //    .Where((User user) => user.Id == item.Id)
            //    .Set("user = {newUser}")
            //    .WithParam("newUser", new User { FirstName = item.FirstName, LastName = item.LastName, Age = item.Age})
            //    .ExecuteWithoutResults();

            //client.Cypher
            //     .OptionalMatch("(phone:Phone)<-[r]-()")
            //     .Where((Phone phone) => phone.UserId == item.Id)
            //     .Delete("r, phone")
            //     .ExecuteWithoutResults();

            //foreach (Phone phone in item.Phones)
            //{
            //    phone.UserId = item.Id;

            //    client.Cypher
            //        .Match("(owner:User)")
            //        .Where((User owner) => owner.Id == item.Id)
            //        .Create("owner<-[:OWNED]-(owned:Phone {newPhone})")
            //        .WithParam("newPhone", phone)
            //        .ExecuteWithoutResults();
            //}

            Delete(item.Id);
            Create(item);
        }
    }
}