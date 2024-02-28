namespace PhasmophobiaCompanion.Interfaces
{
    public interface IDisplayableItem: ITitledItem
    {
        string Description { get; }
        string ImageFilePath { get; }
        string Title { get; }
    }
}