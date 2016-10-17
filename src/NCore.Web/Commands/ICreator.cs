namespace NCore.Web.Commands
{
    public interface ICreator : ICommand
    {
        long? AssignedId { get; set; }
    }
}