using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTools;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSecurity.Repositories;

namespace WebSecurity.Tests
{
    [TestClass]
    public class Test1
    {
        [TestMethod]
        public void GetIRoleOfMemberCollectionTest()
        {
            ApplicationCustomizer.SecurityConnectionString = "data source=Domer-pc;initial catalog=Taxorg_Temp;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            var repo = new RoleOfMemberRepository();
            var query = repo.GetQueryableCollection().Where(rm => rm.MemberName == "Domer");
            foreach (var roleOfMember in query)
            {
                Debug.WriteLine(roleOfMember.RoleName);
            }
        }

        [TestMethod]
        public void AddRoleToMemberAsyncTest()
        {
//            ApplicationCustomizer.SecurityConnectionString = "data source=Domer-pc;initial catalog=Taxorg_Temp;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
//            var repo = new RoleOfMemberRepository();
//            repo.AddMemberToRoleAsync();
        }

        [TestMethod]
        public void GetIRoleCollectionTest()
        {
            ApplicationCustomizer.SecurityConnectionString = "data source=Domer-pc;initial catalog=Taxorg_Temp;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            var repo = new RoleRepository();
            var query = repo.GetQueryableCollection().Where(rm => rm.RoleName == "Role1");
            foreach (var model in query)
            {
                Debug.WriteLine(model.RoleName);
            }
        }

        [TestMethod]
        public void GetIUserCollectionTest()
        {
            ApplicationCustomizer.SecurityConnectionString = "data source=Domer-pc;initial catalog=Taxorg_Temp;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            var repo = new UserRepository();
            var query = repo.GetQueryableCollection().Where(rm => rm.Login == "Domer");
            foreach (var model in query)
            {
                Debug.WriteLine(model.Login);
            }
        }

        [TestMethod]
        public void AddUserTest()
        {
            ApplicationCustomizer.SecurityConnectionString = "data source=Domer-pc;initial catalog=Taxorg_Temp;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            var repo = new UserRepository();
            repo.Add("User1", "Пользователь 1", "user1@email.ru", "sdfsdf-sdafasdf-sdfwe-fghfh-cvdf-dfgsger");

            var query = repo.GetQueryableCollection().Where(u => u.Login == "User1");
            foreach (var model in query)
            {
                Debug.WriteLine(model.Login);
            }
        }

        [TestMethod]
        public void EditUserTest()
        {
            ApplicationCustomizer.SecurityConnectionString = "data source=Domer-pc;initial catalog=Taxorg_Temp;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            var repo = new UserRepository();
            var usr = new TestUser()
                {
                    IdMember = 21,
                    Name = "User1",
                    IdUser = 21,
                    Login = "User1",
                    DisplayName = "Пользователь 1",
                    Email = "User1@email.ru",
                    Usersid = "sdfsdf-sdafasdf-sdfwe-fghfh-cvdf-dfgsger"
                };
            repo.Edit(usr);

            var query = repo.GetQueryableCollection().Where(u => u.IdUser == 21);
            foreach (var model in query)
            {
                Debug.WriteLine(model.DisplayName);
                Debug.WriteLine(model.Email);
            }
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            ApplicationCustomizer.SecurityConnectionString = "data source=Domer-pc;initial catalog=Taxorg_Temp;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            var repo = new UserRepository();
            repo.Delete(21);

            var query = repo.GetQueryableCollection().Where(u => u.IdUser == 21);
            foreach (var model in query)
            {
                Debug.WriteLine(model.DisplayName);
                Debug.WriteLine(model.Email);
            }
        }
    }

    public class TestUser : IUser
    {
        public int IdMember { get; set; }
        public string Name { get; set; }
        public int IdUser { get; set; }
        public string Login { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Usersid { get; set; }
    }
}
