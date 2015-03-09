namespace Interfaces
{
    public interface IRoleOfMember
    {
        int IdRole { get; set; }

        string RoleName { get; set; }

        int IdMember { get; set; }

        string MemberName { get; set; }

        bool IsUser { get; set; }
    }
}