using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    public class UserRepository : SecurityDataModel.Repositories.UserRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public UserRepository() 
            : base(new WebMvcSecurityContext())
        {
        }
    }
}
