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
        public const string RoleDescription = "Публичная роль предназначенная для всех";
        private readonly IRole _role;
        private static PublicRole _instance;

        private PublicRole()
        {
            IRoleRepository repo = new RoleRepository();
            SetPublic();

            _role = repo.GetRole(Name);
        }

        public static void SetPublic()
        {
            var repo = new RoleRepository();
            var query = repo.GetQueryableCollection();
            if (!query.Any(r => r.RoleName == Name))
                repo.Add(Name, RoleDescription);
        }

        public int IdRole
        {
            get { return _role.IdRole; }
            set { }
        }

        public string RoleName
        {
            get { return _role.RoleName; }
            set { }
        }

        public string Description
        {
            get { return _role.Description; }
            set { }
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
