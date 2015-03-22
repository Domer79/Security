using System.Runtime.InteropServices;
using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    internal class CommonUserRepository : SecurityDataModel.Repositories.CommonUserRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public CommonUserRepository() 
            : base(new WebMvcSecurityContext())
        {
        }
    }
}
