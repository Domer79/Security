namespace Interfaces
{
    public interface IRoleRepository : IQueryableCollection<IRole>
    {
        void Add(string roleName);
        void Edit(IRole role);
        void Delete(IRole role);
    }
}