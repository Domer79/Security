﻿using System;
using System.Linq;
using SystemTools.Interfaces;
using DataRepository;
using SecurityDataModel.Exceptions;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public class UserGroupsDetailRepository : IUserGroupsDetailRepository
    {
        private readonly UserGroupsDetailRepositoryLocal _repo;
        private readonly SecurityRepository<User> _userRepo;
        private readonly SecurityRepository<Group> _groupRepo;

        public UserGroupsDetailRepository()
        {
            _repo = new UserGroupsDetailRepositoryLocal();
            _userRepo = new SecurityRepository<User>();
            _groupRepo = new SecurityRepository<Group>();
        }

        public void AddToGroup(string login, string groupName)
        {
            if (login == null) 
                throw new ArgumentNullException("login");

            if (groupName == null) 
                throw new ArgumentNullException("groupName");

            var user = _userRepo.First(u => u.Login == login);
            var group = _groupRepo.First(g => g.GroupName == groupName);

            AddToGroup(user, group);
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

        public void DeleteFromGroup(string userName, string groupName)
        {
            if (userName == null)
                throw new ArgumentNullException("userName");

            if (groupName == null)
                throw new ArgumentNullException("groupName");

            var user = _userRepo.First(m => m.Login == userName);
            var group = _groupRepo.First(r => r.GroupName == groupName);

            DeleteFromGroup(user, group);
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
