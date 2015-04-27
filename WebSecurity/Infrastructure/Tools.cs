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
    public class Tools
    {
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
    }
}
