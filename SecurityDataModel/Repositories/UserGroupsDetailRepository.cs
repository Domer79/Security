using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataRepository;
using Interfaces;
using SecurityDataModel.Exceptions;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public interface IUserGroupsDetailRepository : IQueryableCollection<IUserGroupsDetail>
    {
        void AddToGroup(int idUser, int idGroup);
        void DeleteFromGroup(int idUser, int idGroup);
    }

    public class UserGroupsDetailRepository : IUserGroupsDetailRepository
    {
        private readonly Repository<UserGroupsDetail> _repo;
        private readonly Repository<User> _userRepo;
        private readonly Repository<Group> _groupRepo;


        public UserGroupsDetailRepository(SecurityContext context)
        {
            _repo = new Repository<UserGroupsDetail>(context);
            _userRepo = new Repository<User>(context);
            _groupRepo = new Repository<Group>(context);
        }

        public void AddToGroup(int idUser, int idGroup)
        {
            if (!_userRepo.Any(u => u.IdUser == idUser))
                throw new MemberNotFoundException("IdUser", idUser);

            if (!_groupRepo.Any(g => g.IdGroup == idGroup))
                throw new MemberNotFoundException("IdGroup", idGroup);

            var ug = _repo.Find(idUser, idGroup);
            if (ug != null)
                throw new UserGroupExistsException("Пользователь уже имеется в этой группе. IdUser={0}, IdGroup={1}", idUser, idGroup);

            _repo.InsertOrUpdate(new UserGroupsDetail{IdUser = idUser, IdGroup = idGroup});
            _repo.SaveChanges();
        }

        public void DeleteFromGroup(int idUser, int idGroup)
        {
            if (!_userRepo.Any(u => u.IdUser == idUser))
                throw new MemberNotFoundException("IdUser", idUser);

            if (!_groupRepo.Any(g => g.IdGroup == idGroup))
                throw new MemberNotFoundException("IdGroup", idGroup);

            var ug = _repo.Find(idUser, idGroup);
            if (ug != null)
                throw new UserGroupExistsException("Пользователь уже имеется в этой группе. IdUser={0}, IdGroup={1}", idUser, idGroup);
        }

        public IQueryable<IUserGroupsDetail> GetQueryableCollection()
        {
            return _repo;
        }
    }
}
