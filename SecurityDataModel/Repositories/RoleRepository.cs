﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTools.Interfaces;
using DataRepository;
using SecurityDataModel.Exceptions;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SecurityRepository<Role> _repo;

        public RoleRepository() 
        {
            _repo = new SecurityRepository<Role>();
        }

        public void Add(string roleName, string description)
        {
            if(string.IsNullOrEmpty(roleName.Trim()))
                throw new ArgumentException("roleName");

            var role = new Role{RoleName = roleName, Description = description};
            _repo.InsertOrUpdate(role);
            _repo.SaveChanges();
        }

        public void Edit(string roleName, string newRoleName, string newDescription)
        {
            var role = _repo.FirstOrDefault(r => r.RoleName == roleName);
            if (role == null)
                throw new RoleNotFoundException(roleName);

            role.RoleName = newRoleName;
            role.Description = newDescription ?? role.Description;
            _repo.SaveChanges();
        }

        public void Edit(int idRole, string roleName, string description)
        {
            var role = _repo.Find(idRole);
            if (role == null)
                throw new RoleNotFoundException(idRole, roleName);

            role.RoleName = roleName;
            role.Description = description ?? role.Description;
            _repo.SaveChanges();
        }

        public void Delete(string roleName)
        {
            var role = _repo.FirstOrDefault(r => r.RoleName == roleName);

            if (role == null)
                throw new RoleNotFoundException(roleName);

            Delete(role.IdRole);
        }

        public void Delete(int idRole)
        {
            var role = _repo.Find(idRole);
            if (role == null)
                throw new RoleNotFoundException(idRole);

            _repo.Delete(role);
            _repo.SaveChanges();
        }

        public IRole GetRole(int idRole)
        {
            if (idRole < 1)
                throw new IndexOutOfRangeException("idRole");

            return _repo.First(r => r.IdRole == idRole);
        }

        public IRole GetRole(string roleName)
        {
            if (roleName == null) 
                throw new ArgumentNullException("roleName");

            return _repo.First(r => r.RoleName == roleName);
        }

        public IQueryable<IRole> GetQueryableCollection()
        {
            return _repo;
        }
    }
}
