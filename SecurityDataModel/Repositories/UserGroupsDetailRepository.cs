using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTools.Interfaces;
using DataRepository;
using SecurityDataModel.Exceptions;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public class UserGroupsDetailRepository : IUserGroupsDetailRepository
    {
        private readonly UserGroupsDetailRepositoryLocal _repo;
        private readonly Repository<User> _userRepo;
        private readonly Repository<Group> _groupRepo;

        public UserGroupsDetailRepository(SecurityContext context)
        {
            _repo = new UserGroupsDetailRepositoryLocal(context);
            _userRepo = new Repository<User>(context);
            _groupRepo = new Repository<Group>(context);
        }

        public void AddToGroup(int idUser, int idGroup)
        {
            var user = _userRepo.FirstOrDefault(u => u.IdUser == idUser);
            if (user == null)
                throw new MemberNotFoundException("IdUser", idUser);

            var group = _groupRepo.FirstOrDefault(g => g.IdGroup == idGroup);
            if (group == null)
                throw new MemberNotFoundException("IdGroup", idGroup);

            AddToGroup(user, group);
        }

        public void AddToGroup(IUser user, IGroup group)
        {
            if (user == null) 
                throw new ArgumentNullException("user");

            if (group == null) 
                throw new ArgumentNullException("group");

            var ug = _repo.Find(user.IdUser, group.IdGroup);
            if (ug != null)
                throw new UserGroupExistsException("Пользователь уже имеется в этой группе. IdUser={0}, IdGroup={1}", user.IdUser, group.IdGroup);

            _repo.InsertOrUpdate(new UserGroupsDetail(){IdUser = user.IdUser, IdGroup = group.IdGroup});
            _repo.SaveChanges();
        }

        public void DeleteFromGroup(int idUser, int idGroup)
        {
            var user = _userRepo.FirstOrDefault(u => u.IdUser == idUser);
            if (user == null)
                throw new MemberNotFoundException("IdUser", idUser);

            var group = _groupRepo.FirstOrDefault(g => g.IdGroup == idGroup);
            if (group == null)
                throw new MemberNotFoundException("IdGroup", idGroup);

            DeleteFromGroup(user, group);
        }

        public void DeleteFromGroup(IUser user, IGroup group)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (@group == null)
                throw new ArgumentNullException("group");

            var ug = _repo.Find(user.IdUser, group.IdGroup);
            if (ug == null)
                throw new UserGroupNotFoundException("Отсутствует пользователь в группе или сама группа. IdUser={0}, IdGroup={1}", user.IdUser, group.IdGroup);

            _repo.Delete(ug);
            _repo.SaveChanges();
        }

        public IQueryable<IUserGroupsDetail> GetQueryableCollection()
        {
            return _repo;
        }
    }
}
