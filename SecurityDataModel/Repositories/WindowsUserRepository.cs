using System;
using System.Linq;
using SystemTools.Extensions;
using SystemTools.Interfaces;
using DataRepository;
using SecurityDataModel.Exceptions;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    internal class WindowsUserRepository : UserRepositoryBase
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public WindowsUserRepository(SecurityContext context) 
            : base(context)
        {
        }

        protected override void SetPasswordOrSid(User user, string passwordOrSid)
        {
            if (user == null) 
                throw new ArgumentNullException("user");

            if (!passwordOrSid.RxIsMatch(@"^S-[\d]-[\d]-[\d-]+[\d]$"))
                throw new ArgumentException("passwordOrSid");

            user.Usersid = passwordOrSid;
        }
    }
}
