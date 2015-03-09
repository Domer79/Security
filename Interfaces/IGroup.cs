namespace Interfaces
{
    public interface IGroup : IMember
    {
        int IdGroup { get; set; }
        string Description { get; set; }
    }
}