namespace Interfaces
{
    public interface IUserRepository : IQueryableCollection<IUser>
    {
        void Add(string login, string displayName, string email, string usersid);
        void Edit(int idUser, string login, string displayName, string email, string usersid);
        void Edit(string login, string displayName, string email, string usersid);
        void Delete(string loginOrSid);
        void Delete(int idUser);
    }
}