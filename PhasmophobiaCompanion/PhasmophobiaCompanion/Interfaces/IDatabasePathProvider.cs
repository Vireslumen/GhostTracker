namespace PhasmophobiaCompanion.Interfaces
{
    public interface IDatabasePathProvider
    {
        void CopyDatabaseIfNeeded();
        string GetDatabasePath();
    }
}