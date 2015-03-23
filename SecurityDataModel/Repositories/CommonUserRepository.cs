using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SystemTools;
using SystemTools.Extensions;
using SystemTools.Interfaces;
using SystemTools.WebTools.Infrastructure;
using DataRepository;
using SecurityDataModel.Exceptions;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public class CommonUserRepository : IUserRepository
    {
        private readonly UserRepositoryBase _userRepo;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public CommonUserRepository(SecurityContext context)
        {
            switch (ApplicationCustomizer.SecuritySettings.IdentificationMode)
            {
                case IdentificationMode.Forms:
                    _userRepo = new FormsUserRepository(context);
                    break;
                case IdentificationMode.Windows:
                    _userRepo = new WindowsUserRepository(context);
                    break;
                case IdentificationMode.WindowsOrForms:
                    _userRepo = new WindowsFormsUserRepository(context);
                    break;
                default:
                    throw new IdentificationModeIsNotSetException();
            }
        }

        public IQueryable<IUser> GetQueryableCollection()
        {
            return _userRepo.GetQueryableCollection();
        }

        public void Add(string login, string email, string displayName, string passwordOrSid)
        {
            _userRepo.Add(login, email, displayName, passwordOrSid);
        }

        public void Edit(string login, string email, string usersid, string displayName, string passwordOrSid)
        {
            _userRepo.Edit(login, email, usersid, displayName, passwordOrSid);
        }

        public void Delete(string login, string password)
        {
            Delete(login, null, password);
        }

        public void Delete(string login, string email, string password)
        {
            Delete(login, email, null, password);
        }

        public void Delete(string login, string email, string usersid, string password)
        {
            _userRepo.Delete(login, email, usersid, password);
        }
    }
}
