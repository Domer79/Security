using System;
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

                if (ContextDisposed)
                    throw new InvalidOperationException("Контекст удален");

                return _context;
            }
        }

        public static bool ContextDisposed { get; private set; }

        internal static SecurityContext CreateContext()
        {
            return _createContext();
        }

        public static void SetContext(Func<SecurityContext> createContext)
        {
            if (createContext == null) 
                throw new ArgumentNullException("createContext");

            if (_context == null || ContextDisposed)
            {
                _context = createContext();
                ContextDisposed = false;
            }

            _createContext = createContext;
        }

        public static void RenewContext()
        {
            if (_createContext == null)
                throw new NullReferenceException("Делегат CreateContext не проинициализирован");

            _context = _createContext();
        }
    }
}
