using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTools.Interfaces;
using SecurityDataModel.Exceptions;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public class GrantRepository : IGrantRepository
    {
        private readonly GrantRepositoryLocal _repo;

        public GrantRepository()
        {
            _repo = new GrantRepositoryLocal();
        }

        public void AddGrant(ISecObject securityObject, IRole role, IAccessType accessType)
        {
            AddGrant(securityObject.IdSecObject, role.IdRole, accessType.IdAccessType, false);
        }

        public void AddGrant(int idSecObject, int idRole, int idAccessType)
        {
            AddGrant(idSecObject, idRole, idAccessType, true);
        }

        private void AddGrant(int idSecObject, int idRole, int idAccessType, bool withCheck)
        {
            if (withCheck)
            {
                if (!CheckSecObject(idSecObject))
                    throw new SecObjectNotFoundException(idSecObject);

                if (!CheckRole(idRole))
                    throw new RoleNotFoundException("Идентификатор: ", idRole);

                if (!CheckAccessType(idAccessType))
                    throw new AccessTypeNotFoundException(idAccessType);
            }

            _repo.InsertOrUpdate(new Grant { IdSecObject = idSecObject, IdRole = idRole, IdAccessType = idAccessType });
            _repo.SaveChanges();
        }

        public void RemoveGrant(int idSecObject, int idRole, int idAccessType)
        {
            if (!CheckSecObject(idSecObject))
                throw new SecObjectNotFoundException(idSecObject);

            if (!CheckRole(idRole))
                throw new RoleNotFoundException("Идентификатор: ", idRole);

            if (!CheckAccessType(idAccessType))
                throw new AccessTypeNotFoundException(idAccessType);

            var grant = _repo.Find(idSecObject, idRole, idAccessType);

            if (grant == null)
                throw new GrantNotFoundException(idSecObject, idRole, idAccessType);

            _repo.Delete(grant);
            _repo.SaveChanges();
        }

        public void RemoveGrant(ISecObject secObject, IRole role, IAccessType accessType)
        {
            var grant = _repo.Find(secObject.IdSecObject, role.IdRole, accessType.IdAccessType);

            if (grant == null)
                throw new GrantNotFoundException(secObject.IdSecObject, role.IdRole, accessType.IdAccessType);

            _repo.Delete(grant);
            _repo.SaveChanges();
        }

        private bool CheckSecObject(int idSecObject)
        {
            var results = _repo.SqlQuery<int>("select 1 from sec.SecObject where idSecObject = @p0", idSecObject);
            return results.Any();
        }

        private bool CheckRole(int idRole)
        {
            var results = _repo.SqlQuery<int>("select 1 from sec._Role where idRole = @p0", idRole);
            return results.Any();
        }

        private bool CheckAccessType(int idAccessType)
        {
            var results = _repo.SqlQuery<int>("select 1 from sec.AccessType where idAccessType = @p0", idAccessType);
            return results.Any();
        }

        public bool IsAccess(string login, string objectName, string accessType)
        {
            return _repo.SqlQuery<bool>("select sec.IsAllowByName(@p0, @p1, @p2)", objectName, login, accessType).FirstOrDefault();
        }

        public IQueryable<IGrant> GetQueryableCollection()
        {
            return _repo;
        }
        public IQueryable<IRole> GetRoles()
        {
            return _repo;
        }

        public IQueryable<ISecObject> GetSecObjects()
        {
            return _repo;
        }

        public IQueryable<IAccessType> GetAccessTypes()
        {
            return _repo;
        }

    }
}
