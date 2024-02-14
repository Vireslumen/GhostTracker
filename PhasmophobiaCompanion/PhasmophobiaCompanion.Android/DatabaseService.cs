using System;
using System.IO;
using Android.App;
using PhasmophobiaCompanion.Interfaces;

namespace PhasmophobiaCompanion.Droid
{
    public class AndroidDatabasePathProvider : IDatabasePathProvider
    {
        public string GetDatabasePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "phasmaDATADB.db");
        }

        public void CopyDatabaseIfNeeded()
        {
            var dbPath = GetDatabasePath();
            if (!File.Exists(dbPath))
                // Копирование файла базы данных из Assets
                using (var br = new BinaryReader(Application.Context.Assets.Open("phasmaDATADB.db")))
                using (var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                {
                    var buffer = new byte[2048];
                    var length = 0;
                    while ((length = br.Read(buffer, 0, buffer.Length)) > 0) bw.Write(buffer, 0, length);
                }
        }
    }
}