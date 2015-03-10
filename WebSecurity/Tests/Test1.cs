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
        public Test1()
        {
            ApplicationCustomizer.SecurityConnectionString = "data source=cito1;initial catalog=Taxorg_Temp;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
        }

        [TestMethod]
        public void GetIRoleOfMemberCollectionTest()
        {
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
            var repo = new UserRepository();
            var usr = new TestUser()
                {
                    IdMember = 22,
                    Name = "User1",
                    IdUser = 22,
                    Login = "User1",
                    DisplayName = "Пользователь 1",
                    Email = "User1@email.ru",
                    Usersid = "sdfsdf-sdafasdf-sdfwe-fghfh-cvdf-dfgsger"
                };
            repo.Edit(usr);

            var query = repo.GetQueryableCollection().Where(u => u.IdUser == 22);
            foreach (var model in query)
            {
                Debug.WriteLine(model.DisplayName);
                Debug.WriteLine(model.Email);
            }
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            var repo = new UserRepository();
            repo.Delete(22);

            var query = repo.GetQueryableCollection().Where(u => u.IdUser == 22);
            foreach (var model in query)
            {
                Debug.WriteLine(model.DisplayName);
                Debug.WriteLine(model.Email);
            }
        }

        [TestMethod]
        public void GetGroupCollectionTest()
        {
            var repo = new GroupRepository();
            var query = repo.GetQueryableCollection().Where(g => g.GroupName == "Users");
            foreach (var model in query)
            {
                Debug.WriteLine(model.GroupName);
                Debug.WriteLine(model.Description);
            }

        }

        [TestMethod]
        public void AddGroupTest()
        {
            var repo = new GroupRepository();
            repo.Add("Группа1", "Просто группа №1");

            var query = repo.GetQueryableCollection().Where(u => u.GroupName == "Группа1");
            foreach (var model in query)
            {
                Debug.WriteLine(model.GroupName);
                Debug.WriteLine(model.Description);
            }
        }

        [TestMethod]
        public void EditGroupTest()
        {
            var repo = new GroupRepository();
            repo.Edit(24, "Группа1", "Просто группа №1. Немного изменил.");

            var query = repo.GetQueryableCollection().Where(u => u.GroupName == "Группа1");
            foreach (var model in query)
            {
                Debug.WriteLine(model.GroupName);
                Debug.WriteLine(model.Description);
            }
        }

        [TestMethod]
        public void DeleteGroupTest()
        {
            var repo = new GroupRepository();
            repo.Delete(24);

            var query = repo.GetQueryableCollection().Where(u => u.GroupName == "Группа1");
            foreach (var model in query)
            {
                Debug.WriteLine(model.GroupName);
                Debug.WriteLine(model.Description);
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
