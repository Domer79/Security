using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataRepository;
using Interfaces;
using SecurityDataModel.Exceptions;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly Repository<Role> _repo;

        public RoleRepository(SecurityContext context) 
        {
            _repo = new Repository<Role>(context);
        }

        public void Add(string roleName)
        {
            if(string.IsNullOrEmpty(roleName.Trim()))
                throw new ArgumentException("roleName");

            var role = new Role{RoleName = roleName};
            _repo.InsertOrUpdate(role);
            _repo.SaveChanges();
        }

        public void Edit(IRole role)
        {
            var r = CheckAndFindRole(role);

            r.RoleName = role.RoleName;

            _repo.SaveChanges();
        }

        public void Delete(IRole role)
        {
            var r = CheckAndFindRole(role);
            _repo.Delete(r);
        }

        private Role CheckAndFindRole(IRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            if (role.IdRole == default(int))
                throw new RoleIsNotValidException("Неправильный идентификатор роли");

            var r = _repo.Find(role.IdRole);
            if (r == null)
                throw new ModelNotFoundException(role.RoleName);

            return r;
        }

        public IQueryable<IRole> GetQueryableCollection()
        {
            return _repo;
        }
    }
}
