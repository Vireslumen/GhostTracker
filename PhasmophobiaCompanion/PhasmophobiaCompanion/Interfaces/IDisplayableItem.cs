namespace PhasmophobiaCompanion.Interfaces
{
    public interface IDisplayableItem
    {
        string Description { get; }
        string ImageFilePath { get; }
        string Title { get; }
    }
}