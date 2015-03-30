using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebSecurity.Infrastructure
{
    public class Tools
    {
        internal static bool IsWindowsUser(string loginWithDomain, out string login, out string domain)
        {
            var rx = new Regex(@"^(?<login>[\w][-_\w.]*[\w])\\(?<domain>[\w][-_\w.]*[\w])$");

            var match = rx.Match(loginWithDomain);
            if (match.Success)
            {
                login = match.Groups["login"].Value;
                domain = match.Groups["domain"].Value;
                return true;
            }

            login = null;
            domain = null;
            return false;
        }
    }
}
