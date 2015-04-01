using System.Linq;
using SystemTools.Interfaces;
using WebSecurity.Repositories;

namespace WebSecurity
{
    public class Security : ISecurity
    {
        private const string DefaultPassword = "DefaultPassword";

        /// <summary>
        /// »нициализирует новый экземпл€р класса <see cref="T:System.Object"/>.
        /// </summary>
        public Security(string login) 
            : this(login, DefaultPassword)
        {
        }

        /// <summary>
        /// »нициализирует новый экземпл€р класса <see cref="T:System.Object"/>.
        /// </summary>
        public Security(string login, string password)
        {
            User = GetUser(login, password);
        }

        public IUser User { get; set; }

        public static IUser GetUser(string login)
        {
            return GetUser(login, DefaultPassword);
        }

        public static IUser GetUser(string login, string password)
        {
            var repo = new UserRepository();
            return repo.GetUser(login, password);
        }
    }
}