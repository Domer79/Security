using System;
using System.Linq;
using SystemTools.Interfaces;
using SecurityDataModel.Repositories;
using AccessTypeRepository = WebSecurity.Repositories.AccessTypeRepository;
using GrantRepository = WebSecurity.Repositories.GrantRepository;
using RoleRepository = WebSecurity.Repositories.RoleRepository;

namespace WebSecurity
{
    public class PublicRole : IPublicRole
    {
        public const string Name = "public";
        private readonly IRole _role;
        private static PublicRole _instance;

        private PublicRole()
        {
            IRoleRepository repo = new RoleRepository();
            var query = repo.GetQueryableCollection();
            if (!query.Any(r => r.IdRole == IdRole))
                repo.Add(RoleName);

            _role = repo.GetRole(Name);
        }

        public int IdRole
        {
            get { return _role.IdRole; }
            set { throw new NotImplementedException(); }
        }

        public string RoleName
        {
            get { return _role.RoleName; }
            set { throw new NotImplementedException(); }
        }

        public void GrantToRole(ISecObject secObject, Enum accessType)
        {
            IGrantRepository repo = new GrantRepository();
            repo.AddGrant(secObject, this, AccessTypeRepository.GetAccessType(accessType));
        }

        public void GrantToRole(string secObject, string accessType)
        {
            IGrantRepository repo = new GrantRepository();
            repo.AddGrant(SecObjectRepository.GetSecurityObject(secObject), this, AccessTypeRepository.GetAccessTypeByName(accessType));
        }

        internal static PublicRole Instance
        {
            get { return _instance ?? (_instance = new PublicRole()); }
        }
    }
}
