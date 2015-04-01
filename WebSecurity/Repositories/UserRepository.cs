using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using SystemTools.Interfaces;
using WebSecurity.Data;
using WebSecurity.Infrastructure;

namespace WebSecurity.Repositories
{
    public class UserRepository : SecurityDataModel.Repositories.UserRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public UserRepository()
            :base(new WebMvcSecurityContext())
        {
        }
    }
}
