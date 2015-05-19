using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTools.Interfaces;
using DataRepository;
using SecurityDataModel.Exceptions;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly SecurityRepository<Group> _repo;

        public GroupRepository()
        {
            _repo = new SecurityRepository<Group>();
        }

        public void SetContext(RepositoryDataContext context)
        {
            _repo.SetContext(context);
        }

        public void Add(string groupName, string description = null)
        {
            if (string.IsNullOrEmpty(groupName))
                throw new ArgumentException("groupName");

//            if (_repo.Any(g => String.Equals(g.GroupName, groupName, StringComparison.CurrentCultureIgnoreCase)))
            if (_repo.Any(g => g.GroupName == groupName))
                throw new GroupExistsException();

            _repo.InsertOrUpdate(new Group {GroupName = groupName, Description = description});
            _repo.SaveChanges();
        }

        public void Edit(string groupName, string newGroupName, string description)
        {
            var group = _repo.FirstOrDefault(g => g.GroupName == groupName);
            if (group == null)
                throw new MemberNotFoundException("[Is group]", groupName, "newGroupName=" + newGroupName, description);

            Edit(group.IdGroup, newGroupName, description);
        }

        public void Edit(int idGroup, string groupName, string description)
        {
            if(string.IsNullOrEmpty(groupName))
                throw new ArgumentException("groupName");

            var group = _repo.Find(idGroup);
            if (group == null)
                throw new MemberNotFoundException("[Is group]", idGroup, groupName, description);

            group.GroupName = groupName;
            group.Description = description;

            _repo.SaveChanges();
        }

        public void Delete(string groupName)
        {
            var group = _repo.FirstOrDefault(g => g.GroupName == groupName);
            if (group == null)
                throw new MemberNotFoundException("[Is group]", groupName);
            
            Delete(group.IdGroup);
        }

        public void Delete(int idGroup)
        {
            var group = _repo.Find(idGroup);
            if (group == null)
                throw new MemberNotFoundException("Is group", idGroup);

            _repo.Delete(group);
            _repo.SaveChanges();
        }

        public IQueryable<IGroup> GetQueryableCollection()
        {
            return _repo;
        }
    }
}
