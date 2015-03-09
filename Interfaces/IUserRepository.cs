namespace Interfaces
{
    public interface IUserRepository : IQueryableCollection<IUser>
    {
        void Add(string login, string displayName, string email, string usersid);
        void Edit(IUser user);
        void Delete(int idUser);
    }
}