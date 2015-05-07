using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SystemTools.WebTools.Attributes;
using SystemTools.WebTools.Helpers;
using SystemTools.WebTools.Infrastructure;
using DataRepository.Infrastructure;

namespace WebSecurity.Infrastructure
{
    internal class Tools
    {
        private const string AllSecurityObjectsName = "allsecurityobjects";
        private static string _allSecurityObjects = AllSecurityObjectsName;
        internal static bool IsWindowsUser(string login)
        {
            var rx = new Regex(@"^(?<login>[\w][-_\w.]*[\w])\\(?<domain>[\w][-_\w.]*[\w])$");
            return rx.IsMatch(login);
        }

        internal static IEnumerable<AliasAttributeBase> GetSecurityObjects()
        {
            var securityObjects = GetControllerActionAliases().Concat(GetEntityAliases());
            return securityObjects;
        }

        internal static IEnumerable<AliasAttributeBase> GetControllerActionAliases()
        {
            return ControllerHelper.ControllerCollection.Select(ci => ci.ActionAliasAttribute);
        }

        internal static IEnumerable<AliasAttributeBase> GetEntityAliases()
        {
            return ContextInfo.ContextInfoCollection.SelectMany(ci => ci.EntityMetadataCollection).Select(em => em.EntityAliasAttribute);
        }

        internal static string AllSecurityObjects
        {
            get
            {
                if (GetSecurityObjects().All(on => on.Alias != _allSecurityObjects))
                    return _allSecurityObjects;

                var indexValue = Regex.Match(AllSecurityObjectsName, @"\d*").Value;
                var index = string.IsNullOrEmpty(indexValue) ? default(int) : int.Parse(indexValue);

                _allSecurityObjects = string.Format("{0}_{1}", _allSecurityObjects, index);
                return AllSecurityObjects;
            }
        }
    }
}
