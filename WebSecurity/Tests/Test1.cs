using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTools;
using DataRepository;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityDataModel.Models;
using WebSecurity.Repositories;

namespace WebSecurity.Tests
{
    [TestClass]
    public class Test1
    {
        public Test1()
        {
//            ApplicationCustomizer.SecurityConnectionString = "data source=cito1;initial catalog=Taxorg_Temp;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            ApplicationCustomizer.SecurityConnectionString = "data source=Domer-pc;initial catalog=Taxorg_Temp;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
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
        public void GetIUserCollectionTest()
        {
            var repo = new UserRepository();
            var query = repo.GetQueryableCollection();
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

            var query = repo.GetQueryableCollection();
            foreach (var model in query)
            {
                Debug.WriteLine(model.Login);
                Debug.WriteLine("");
            }
        }

        [TestMethod]
        public void EditUserTest()
        {
            var repo = new UserRepository();
            repo.Edit("User11", "Пользователь 1. Изменен", "User1@email.ru", "sdfsdf-sdafasdf-sdfwe-fghfh-cvdf-dfgsger");

            var query = repo.GetQueryableCollection();
            foreach (var model in query)
            {
                Debug.WriteLine(model.DisplayName);
                Debug.WriteLine(model.Email);
                Debug.WriteLine("");
            }
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            var repo = new UserRepository();
            repo.Delete("User1");

            var query = repo.GetQueryableCollection();
            foreach (var model in query)
            {
                Debug.WriteLine(model.DisplayName);
                Debug.WriteLine(model.Email);
                Debug.WriteLine("");
            }
        }

        [TestMethod]
        public void GetGroupCollectionTest()
        {
            var repo = new GroupRepository();
            var query = repo.GetQueryableCollection();
            foreach (var model in query)
            {
                Debug.WriteLine(model.GroupName);
                Debug.WriteLine(model.Description);
                Debug.WriteLine("");
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
                Debug.WriteLine("");
            }
        }

        [TestMethod]
        public void EditGroupTest()
        {
            var repo = new GroupRepository();
            repo.Edit("Группа1", "Группа1", "Просто группа №1. Немного изменил.");

            var query = repo.GetQueryableCollection().Where(u => u.GroupName == "Группа1");
            foreach (var model in query)
            {
                Debug.WriteLine(model.GroupName);
                Debug.WriteLine(model.Description);
                Debug.WriteLine("");
            }
        }

        [TestMethod]
        public void DeleteGroupTest()
        {
            var repo = new GroupRepository();
            repo.Delete("Группа1");

            var query = repo.GetQueryableCollection();
            foreach (var model in query)
            {
                Debug.WriteLine(model.GroupName);
                Debug.WriteLine(model.Description);
                Debug.WriteLine("");
            }
        }

        [TestMethod]
        public void GetRoleCollectionTest()
        {
            var repo = new RoleRepository();
            var query = repo.GetQueryableCollection();
            foreach (var role in query)
            {
                Debug.WriteLine(role.IdRole, role.RoleName);
            }
        }

        [TestMethod]
        public void AddRoleTest()
        {
            var repo = new RoleRepository();
            repo.Add("Role5");

            var query = repo.GetQueryableCollection();
            foreach (var role in query)
            {
                Debug.WriteLine(role.RoleName);
            }
        }

        [TestMethod]
        public void EditRoleTest()
        {
            var repo = new RoleRepository();
            repo.Edit("role5", "NewRole5");

            var query = repo.GetQueryableCollection();
            foreach (var role in query)
            {
                Debug.WriteLine(role.RoleName);
            }

        }

        [TestMethod]
        public void EditRoleByIdTest()
        {
            var repo = new RoleRepository();
            repo.Edit(6, "Role5");

            var query = repo.GetQueryableCollection();
            foreach (var role in query)
            {
                Debug.WriteLine(role.RoleName);
            }

        }

        [TestMethod]
        public void GetRoleOfMemberCollectionTest()
        {
            var repo = new RoleOfMemberRepository();

            var query = repo.GetQueryableCollection();
            foreach (var roleOfMember in query)
            {
                Debug.WriteLine(roleOfMember.MemberName, roleOfMember.RoleName);
            }
        }

        [TestMethod]
        public void AddRoleToMemberAsyncTest()
        {
            var repo = new RoleOfMemberRepository();
            var userRepo = new SecurityRepository<User>();
            var roleRepo = new SecurityRepository<Role>();

            var elina = userRepo.FirstOrDefault(u => u.Login == "Elina");
            var role1 = roleRepo.FirstOrDefault(r => r.RoleName == "Role1");

            repo.AddMemberToRole(elina, role1);

            var query = repo.GetQueryableCollection();
            foreach (var roleOfMember in query)
            {
                Debug.WriteLine(roleOfMember.MemberName, roleOfMember.RoleName);
            }
        }
    }
}
