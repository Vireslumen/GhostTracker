using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PhasmophobiaCompanion.Models;

namespace PhasmophobiaCompanion.Droid
{
    public class AndroidDatabasePathProvider  : IDatabasePathProvider
    {
        public string GetDatabasePath()
        {
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "phasmaDATADB.db");
        }

        public void CopyDatabaseIfNeeded()
        {
            string dbPath = GetDatabasePath();
            if (!File.Exists(dbPath))
            {
                // Копирование файла базы данных из Assets
                using (var br = new BinaryReader(Android.App.Application.Context.Assets.Open("phasmaDATADB.db")))
                using (var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                {
                    byte[] buffer = new byte[2048];
                    int length = 0;
                    while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        bw.Write(buffer, 0, length);
                    }
                }
            }
        }
    }
}