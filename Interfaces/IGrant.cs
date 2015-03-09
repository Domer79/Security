namespace Interfaces
{
    public interface IGrant
    {
        int IdGrants { get; set; }

        int IdSecObject { get; set; }

        int IdRole { get; set; }

        int IdAccessType { get; set; }
    }
}