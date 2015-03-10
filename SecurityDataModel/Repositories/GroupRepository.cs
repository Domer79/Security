using System;
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
    public class GroupRepository : IGroupRepository
    {
        private readonly Repository<Group> _repo;

        public GroupRepository(SecurityContext context)
        {
            _repo = new Repository<Group>(context);
        }

        public void Add(string name, string description = null)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name");

            if (_repo.Any(g => g.GroupName == name))
                throw new GroupExistsException();

            _repo.InsertOrUpdate(new Group {GroupName = name, Description = description});
            _repo.SaveChanges();
        }

        public void Edit(int idGroup, string name, string description)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentException("name");

            var group = _repo.Find(idGroup);
            if (group == null)
                throw new MemberNotFoundException(idGroup, name, description);

            group.GroupName = name;
            group.Description = description;

            _repo.SaveChanges();
        }

        public void Delete(int idGroup)
        {
            var group = _repo.Find(idGroup);
            if (group == null)
                throw new MemberNotFoundException(idGroup);

            _repo.Delete(group);
            _repo.SaveChanges();
        }

        public IQueryable<IGroup> GetQueryableCollection()
        {
            return _repo;
        }
    }
}
