using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SystemTools.Extensions;
using SystemTools.Interfaces;
using DataRepository;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{

    //TODO: Создать третий класс UserRepositoryBase, реализующий работу смешанного режима работы 
    public class CommonUserRepository : IUserRepository
    {
        private readonly SecurityContext _context;
        private UserRepositoryBase _userRepo;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public CommonUserRepository(SecurityContext context)
        {
            _context = context;

        }

        private static bool IsWindowsUser(string login, string usersid)
        {
            if (login.RxIsMatch(@"^[\w][-_\w.]+[\w]\\[\w][-_\w.]+[\w]$"))
                return usersid.RxIsMatch(@"^S-[\d]-[\d]-[\d-]+[\d]$");
            
            return false;
        }

        public IQueryable<IUser> GetQueryableCollection()
        {
            throw new NotImplementedException();
        }

        public void Add(string login, string email, string displayName, string usersid)
        {
            throw new NotImplementedException();
        }

        public void Edit(int idUser, string login, string email, string displayName, string usersid)
        {
            throw new NotImplementedException();
        }

        public void Edit(string login, string email, string displayName, string usersid)
        {
            throw new NotImplementedException();
        }

        public void Delete(string loginOrSid)
        {
            throw new NotImplementedException();
        }

        public void Delete(int idUser)
        {
            throw new NotImplementedException();
        }
    }
}
