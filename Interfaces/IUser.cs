namespace Interfaces
{
    public interface IUser : IMember
    {
        int IdUser { get; set; }

        string Login { get; set; }

        string DisplayName { get; set; }

        string Email { get; set; }

        string Usersid { get; set; }
    }
}