namespace PhasmophobiaCompanion.Models
{
    public interface IDatabasePathProvider
    {
        string GetDatabasePath();
        void CopyDatabaseIfNeeded();
    }
}