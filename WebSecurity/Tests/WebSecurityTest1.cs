﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using SystemTools;
using SystemTools.Interfaces;
using SystemTools.WebTools.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityDataModel.Models;
using WebSecurity.Repositories;

namespace WebSecurity.Tests
{
    [TestClass]
    public class WebSecurityTest1
    {
        public WebSecurityTest1()
        {
//            ApplicationCustomizer.SecurityConnectionString = "data source=cito1;initial catalog=Taxorg_Temp;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            ApplicationCustomizer.SecurityConnectionString = "data source=.;initial catalog=Taxorg_Temp;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
        }

        #region User test



        #endregion

        #region Group test

        [TestMethod]
        public void GetGroupCollectionTest()
        {
            var repo = new GroupRepository();
            var query = repo.GetQueryableCollection().Where(g => g.GroupName == "Group2");
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

            var query = repo.GetQueryableCollection();
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

            var query = repo.GetQueryableCollection();
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
            repo.Delete("Group2");

            var query = repo.GetQueryableCollection();
            foreach (var model in query)
            {
                Debug.WriteLine(model.GroupName);
                Debug.WriteLine(model.Description);
                Debug.WriteLine("");
            }
        }

        #endregion

        #region Role test

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
            repo.Add("Role5", null);

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
            repo.Edit("role5", "NewRole5", null);

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
            repo.Edit(6, "Role5", null);

            var query = repo.GetQueryableCollection();
            foreach (var role in query)
            {
                Debug.WriteLine(role.RoleName);
            }

        }

        #endregion

        #region RoleOfMember test

        [TestMethod]
        public void GetRoleOfMemberCollectionTest()
        {
            var repo = new RoleOfMemberRepository();

            var query = repo.GetQueryableCollection();
            foreach (var roleOfMember in query)
            {
                Debug.WriteLine(roleOfMember.Name, roleOfMember.RoleName);
            }
        }

        [TestMethod]
        public void AddRoleToMemberAsyncTest()
        {
            var repo = new RoleOfMemberRepository();
            var userRepo = new SecurityRepository<User>();
            var roleRepo = new SecurityRepository<Role>();

            var elina = userRepo.FirstOrDefault(u => u.Login == "Elina");
            var role1 = roleRepo.FirstOrDefault(r => r.RoleName == "Role2");

            repo.AddMemberToRole(elina, role1);

            var query = repo.GetQueryableCollection();
            foreach (var roleOfMember in query)
            {
                Debug.WriteLine(roleOfMember.Name, roleOfMember.RoleName);
            }
        }

        [TestMethod]
        public void DeleteMemberFromRole()
        {
            var repo = new RoleOfMemberRepository();
            var userRepo = new SecurityRepository<User>();
            var roleRepo = new SecurityRepository<Role>();

            var elina = userRepo.First(u => u.Login == "Elina");
            var role1 = roleRepo.First(r => r.RoleName == "Role2");

            repo.DeleteMemberFromRole(((IMember) elina).IdMember, role1.IdRole);

            var query = repo.GetQueryableCollection();
            foreach (var roleOfMember in query)
            {
                Debug.WriteLine(roleOfMember.Name, roleOfMember.RoleName);
            }
        }

        #endregion

        #region UserGroup test

        [TestMethod]
        public void GetUserGroupCollection()
        {
            var repo = new UserGroupsDetailRepository();
            var query = repo.GetQueryableCollection().OrderBy(e => e.IdGroup);
            foreach (var userGroups in query)
            {
                Debug.WriteLine(userGroups.Login, userGroups.GroupName);
            }
        }

        [TestMethod]
        public void AddUserGrousTest()
        {
            var repo = new UserGroupsDetailRepository();

            repo.AddToGroup(10, 30);

            var query = repo.GetQueryableCollection().OrderBy(e => e.IdGroup);
            foreach (var userGroups in query)
            {
                Debug.WriteLine(userGroups.Login, userGroups.GroupName);
            }
        }

        [TestMethod]
        public void DeleteUserFromGroup()
        {
            var repo = new UserGroupsDetailRepository();

            repo.DeleteFromGroup(10, 30);

            var query = repo.GetQueryableCollection().OrderBy(e => e.IdGroup);
            foreach (var userGroups in query)
            {
                Debug.WriteLine(userGroups.Login, userGroups.GroupName);
            }
        }

        #endregion

        #region AccessType test

        [TestMethod]
        public void GetAccessTypeCollectionTest()
        {
            var repo = new AccessTypeRepository();
            var query = repo.GetQueryableCollection();

            foreach (var accessType in query)
            {
                Debug.WriteLine(accessType);
            }
        }

        [TestMethod]
        public void SetNewAccessTypeTest()
        {
            var repo = new AccessTypeRepository();

            repo.SetNewAccessTypes<SecurityAccessType>();

            var query = repo.GetQueryableCollection();

            foreach (var accessType in query)
            {
                Debug.WriteLine(accessType);
            }
        }

        #endregion

        #region Grant test

        [TestMethod]
        public void GetGrantCollectionTest()
        {
            var repo = new GrantRepository();
            var query = repo.GetQueryableCollection();

            foreach (var grant in query)
            {
                Debug.WriteLine("IdSecObject = {0}, IdRole = {1}, IdAccessType = {2}", grant.IdSecObject, grant.IdRole, grant.IdAccessType);
            }
        }

        [TestMethod]
        public void AddGrantTest()
        {
            var repo = new GrantRepository();
            repo.AddGrant(6, 1, 11);
            repo.AddGrant(6, 1, 12);
            repo.AddGrant(6, 1, 13);
            repo.AddGrant(6, 1, 14);
            var query = repo.GetQueryableCollection();

            foreach (var grant in query)
            {
                Debug.WriteLine("IdSecObject = {0}, IdRole = {1}, IdAccessType = {2}", grant.IdSecObject, grant.IdRole, grant.IdAccessType);
            }
        }

        [TestMethod]
        public void RemoveGrantTest()
        {
            var repo = new GrantRepository();
            repo.RemoveGrant(6, 1, 11);
            var query = repo.GetQueryableCollection();

            foreach (var grant in query)
            {
                Debug.WriteLine("IdSecObject = {0}, IdRole = {1}, IdAccessType = {2}", grant.IdSecObject, grant.IdRole, grant.IdAccessType);
            }
        }

        #endregion
    }
}
