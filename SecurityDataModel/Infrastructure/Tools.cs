using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTools.Extensions;

namespace SecurityDataModel.Infrastructure
{
    public class Tools
    {
        internal static bool IsWindowsUser(string login, string usersid)
        {
            if (login.RxIsMatch(@"^[\w][-_\w.]+[\w]\\[\w][-_\w.]+[\w]$"))
                return usersid.RxIsMatch(@"^S-1-5-21-[\d]+-[\d]+-[\d]+-[\d]+[\d]$");

            return false;
        }
    }
}
