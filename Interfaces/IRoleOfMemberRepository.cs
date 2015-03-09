namespace Interfaces
{
    public interface IRoleOfMemberRepository : IQueryableCollection<IRoleOfMember>
    {
        void AddMemberToRole(IMember member, IRole role);
        void AddMemberToRoleAsync(IMember member, IRole role);
        void DeleteMemberFromRole(IMember member, IRole role);
        void DeleteMemberFromRoleAsync(IMember member, IRole role);
    }
}