namespace Interfaces
{
    public interface IUserGroupsDetailRepository : IQueryableCollection<IUserGroupsDetail>
    {
        void AddToGroup(int idUser, int idGroup);
        void AddToGroup(IUser user, IGroup group);
        void DeleteFromGroup(int idUser, int idGroup);
        void DeleteFromGroup(IUser user, IGroup group);
    }
}