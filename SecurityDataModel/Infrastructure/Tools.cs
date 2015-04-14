using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityDataModel.Models;

namespace SecurityDataModel.Infrastructure
{
    public class Tools
    {
        private static SecurityContext _context;
        private static Func<SecurityContext> _createContext;

        internal static SecurityContext Context
        {
            get
            {
                if (_context == null)
                    throw new InvalidOperationException("Контекст не объявлен");

                return _context;
            }
        }

        internal static SecurityContext CreateContext()
        {
            return _createContext();
        }

        public static void SetContext(Func<SecurityContext> createContext)
        {
            if (createContext == null) 
                throw new ArgumentNullException("createContext");

            _context = createContext();
            _createContext = createContext;
        }
    }
}
