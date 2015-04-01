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
        internal static bool IsWindowsUser(string login)
        {
            var rx = new Regex(@"^(?<login>[\w][-_\w.]*[\w])\\(?<domain>[\w][-_\w.]*[\w])$");
            return rx.IsMatch(login);
        }
    }
}
