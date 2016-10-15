namespace NCore.Nancy.Commands
{
    public interface ICreator:ICommand
    {
        long? AssignedId { get; set; }
    }
}