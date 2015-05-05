using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel
{
    public class Service
    {
        public static void SetContext(Func<SecurityContext> createContext)
        {
            Tools.SetContext(createContext);
        }

        public static void RenewContext()
        {
            Tools.RenewContext();
        }

        public static SecurityContext CurrentContext
        {
            get { return Tools.Context; }
        }
    }
}
