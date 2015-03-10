using Interfaces;

namespace SecurityDataModel.Exceptions
{
    public interface IGroupRepository : IQueryableCollection<IGroup>
    {
        void Add(string groupName, string description = null);
        void Edit(int idGroup, string groupName, string description);
        void Edit(string groupName, string newGroupName, string description);
        void Delete(int idGroup);
        void Delete(string groupName);
    }
}