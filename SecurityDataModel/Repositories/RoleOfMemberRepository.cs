using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SystemTools.Interfaces;
using DataRepository;
using SecurityDataModel.Exceptions;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public class RoleOfMemberRepository : IRoleOfMemberRepository
    {
        private readonly Repository<Member> _repoMember;
        private readonly Repository<Role> _repoRole;
        private readonly RoleOfMemberRepositoryLocal _repo;

        public RoleOfMemberRepository(SecurityContext context)
        {
            _repo = new RoleOfMemberRepositoryLocal(context);
            _repoMember = new Repository<Member>(context);
            _repoRole = new Repository<Role>(context);
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
            _repo.InsertOrUpdate(roleOfMember);
            _repo.SaveChanges();
        }

        public async void AddMemberToRoleAsync(IMember member, IRole role)
        {
            await AddMemberToRoleTask(member, role);
        }

        private Task AddMemberToRoleTask(IMember member, IRole role)
        {
            return new Task(() => AddMemberToRole(member, role));
        }

        public void DeleteMemberFromRole(int idMember, int idRole)
        {
            var member = _repoMember.Find(idMember);
            if (member == null)
                throw new MemberNotFoundException(idMember);

            var role = _repoRole.Find(idRole);
            if (role == null)
                throw new RoleNotFoundException(idRole);

            DeleteMemberFromRole(member, role);
        }

        public void DeleteMemberFromRole(IMember member, IRole role)
        {
            var mr = CheckMemberRole(member, role, () =>
            {
                var memberRole = _repo.Find(role.IdRole, member.IdMember);
                if (memberRole == null)
                    throw new ModelNotFoundException(string.Format("Связь {0}-{1}, для удаления не найдена", member.Name,
                        role.RoleName));
                return memberRole;
            });

            _repo.Delete(mr);
            _repo.SaveChanges();
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
            return _repo;
        }
    }
}
