using System;
using System.Collections.Generic;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    public interface IDatabasePathProvider
    {
        string GetDatabasePath();
        void CopyDatabaseIfNeeded();
    }
}
