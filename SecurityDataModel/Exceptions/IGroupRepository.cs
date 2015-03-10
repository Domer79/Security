using Interfaces;

namespace SecurityDataModel.Exceptions
{
    public interface IGroupRepository : IQueryableCollection<IGroup>
    {
        void Add(string name, string description = null);
        void Edit(int idGroup, string name, string description);
        void Delete(int idGroup);
    }
}