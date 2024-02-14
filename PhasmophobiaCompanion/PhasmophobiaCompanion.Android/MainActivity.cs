using System;
using System.IO;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Rg.Plugins.Popup;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Application = Android.App.Application;
using Environment = System.Environment;
using Platform = Xamarin.Essentials.Platform;

namespace PhasmophobiaCompanion.Droid
{
    [Activity(Label = "PhasmophobiaCompanion", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                               ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : FormsAppCompatActivity
    {
        //private void CopyDatabaseIfNotExists(string dbPath)
        //{
        //    if (!File.Exists(dbPath))
        //    {
        //        using (var br = new BinaryReader(Android.App.Application.Context.Assets.Open("phasmaDATADB.db")))
        //        {
        //            using (var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
        //            {
        //                byte[] buffer = new byte[2048];
        //                int length = 0;
        //                while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
        //                {
        //                    bw.Write(buffer, 0, length);
        //                }
        //            }
        //        }
        //    }
        //}
        private void CopyOrUpdateDatabase(string dbPath)
        {
            try
            {
                using (var br = new BinaryReader(Application.Context.Assets.Open("phasmaDATADB.db")))
                {
                    using (var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        var buffer = new byte[2048];
                        var length = 0;
                        while ((length = br.Read(buffer, 0, buffer.Length)) > 0) bw.Write(buffer, 0, length);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при копировании базы данных.");
                throw;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                var logFilePath = Path.Combine("/storage/emulated/0/Download/", "logs", "log-.txt");
                var logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                    .CreateLogger();
                Log.Logger = logger;
                base.OnCreate(savedInstanceState);

                Platform.Init(this, savedInstanceState);
                Forms.Init(this, savedInstanceState);
                Popup.Init(this);
                // Путь к локальной базе данных
                var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var dbPath = Path.Combine(folderPath, "phasmaDATADB.db");

                // Копирование базы данных, если она еще не существует
                CopyOrUpdateDatabase(dbPath);
                LoadApplication(new App());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при создании приложения.");
                throw;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            try
            {
                Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при запросе разрешений.");
                throw;
            }
        }
    }
}