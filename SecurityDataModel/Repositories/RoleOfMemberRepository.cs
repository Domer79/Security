using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataRepository;
using Interfaces;
using SecurityDataModel.Exceptions;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public class RoleOfMemberRepository : RepositoryBase<RoleOfMember>, IRoleOfMemberRepository
    {
        private readonly Repository<Member> _repoMember;
        private readonly Repository<Role> _repoRole;
        private readonly SecurityContext _localContext;

        public RoleOfMemberRepository(SecurityContext context)
        {
            _localContext = context;
            _repoMember = new Repository<Member>(context);
            _repoRole = new Repository<Role>(context);
        }

        public sealed override void InsertOrUpdate(RoleOfMember item)
        {
            Set.Add(item);
        }

        public void AddMemberToRole(int idMember, int idRole)
        {
            var member = _repoMember.Find(idMember);
            if (member == null)
                throw new MemberNotFoundException(idMember);

            var role = _repoRole.Find(idRole);
            if(role == null)
                throw new RoleNotFoundException(idRole);
            
            AddMemberToRole(member, role);
        }

        public void AddMemberToRole(IMember member, IRole role)
        {
            var roleOfMember = CheckMemberRole(member, role, () => new RoleOfMember() { IdMember = member.IdMember, IdRole = role.IdRole });
            InsertOrUpdate(roleOfMember);
            SaveChanges();
        }

        public async void AddMemberToRoleAsync(IMember member, IRole role)
        {
            await AddMemberToRoleTask(member, role);
            Thread.Sleep(3000);
        }

        private Task AddMemberToRoleTask(IMember member, IRole role)
        {
            return new Task(() => AddMemberToRole(member, role));
        }

        public void DeleteMemberFromRole(IMember member, IRole role)
        {
            var mr = CheckMemberRole(member, role, () =>
            {
                var memberRole = Find(role.IdRole, member.IdMember);
                if (memberRole == null)
                    throw new ModelNotFoundException(string.Format("Связь {0}-{1}, для удаления не найдена", member.Name,
                        role.RoleName));
                return memberRole;
            });

            Delete(mr);
            SaveChanges();
        }

        public async void DeleteMemberFromRoleAsync(IMember member, IRole role)
        {
            await DeleteMemberFromRoleTask(member, role);
        }

        private Task DeleteMemberFromRoleTask(IMember member, IRole role)
        {
            return new Task(() => DeleteMemberFromRole(member, role));
        }

        private static RoleOfMember CheckMemberRole(IMember member, IRole role, Func<RoleOfMember> getRoleOfMember)
        {
            if (member == null)
                throw new ArgumentNullException("member");

            if (role == null)
                throw new ArgumentNullException("role");

            if (member.IdMember == default(int))
                throw new MemberIsNotValidException("Необходимо сначала добавить участника безопасности в базу");

            if (role.IdRole == default(int))
                throw new RoleIsNotValidException("Необходимо сначала добавить роль в базу");

            return getRoleOfMember();
        }

        public IQueryable<IRoleOfMember> GetQueryableCollection()
        {
            return this;
        }

        protected override DbContext GetContext()
        {
            return _localContext;
        }
    }
}
