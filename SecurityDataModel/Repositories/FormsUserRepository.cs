using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataRepository;
using SecurityDataModel.Exceptions;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    internal class FormsUserRepository : UserRepositoryBase
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public FormsUserRepository(SecurityContext context) 
            : base(context)
        {
        }

        protected override void SetPasswordOrSid(User user, string passwordOrSid)
        {
            if (user == null) 
                throw new ArgumentNullException("user");

            if (string.IsNullOrEmpty(passwordOrSid))
                throw new ArgumentNullException("passwordOrSid");

            user.Password = SystemTools.Crypto.GetHashString(passwordOrSid);
        }
    }
}
