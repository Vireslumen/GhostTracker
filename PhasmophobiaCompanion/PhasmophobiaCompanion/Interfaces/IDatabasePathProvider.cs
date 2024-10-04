namespace GhostTracker.Interfaces
{
    public interface IDatabasePathProvider
    {
        void CopyDatabaseIfNeeded();
        string GetDatabasePath();
    }
}