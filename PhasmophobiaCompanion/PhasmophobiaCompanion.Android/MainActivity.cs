using System;
using System.IO;
using System.Threading.Tasks;
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
    [Activity(Label = "PhasmAid", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                               ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : FormsAppCompatActivity
    {
        /// <summary>
        ///     Копирование базы данных из ассетов в доступную для обработки директорию, если база данных уже не существует в ней.
        /// </summary>
        private void CopyDatabase()
        {
            try
            {
                var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var dbPath = Path.Combine(folderPath, "phasmaDATADB.db");
                var dbVersionFilePath = Path.Combine(folderPath, "dbVersion.txt");
                var currentDbVersion = "1.2"; // Версия базы данных

                if (File.Exists(dbPath))
                {
                    if (File.Exists(dbVersionFilePath))
                    {
                        var installedDbVersion = File.ReadAllText(dbVersionFilePath);

                        if (installedDbVersion != currentDbVersion)
                        {
                            // Удаление кэша, так как версия базы данных изменилась
                            DeleteCacheFiles(folderPath);

                            // Копирование новой базы данных
                            CopyNewDatabase(dbPath);

                            // Обновление версии базы данных
                            File.WriteAllText(dbVersionFilePath, currentDbVersion);
                        }
                    }
                    else
                    {
                        CopyNewDatabase(dbPath);
                        DeleteCacheFiles(folderPath);
                        File.WriteAllText(dbVersionFilePath, currentDbVersion);
                    }
                }
                else
                {
                    // Копирование базы данных в первый раз
                    CopyNewDatabase(dbPath);
                    File.WriteAllText(dbVersionFilePath, currentDbVersion);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при копировании базы данных.");
            }
        }

        private void CopyNewDatabase(string dbPath)
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

        private void DeleteCacheFiles(string folderPath)
        {
            try
            {
                var cacheFiles = Directory.GetFiles(folderPath, "*.json");
                foreach (var cacheFile in cacheFiles) File.Delete(cacheFile);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при удалении файлов кэша.");
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            Popup.Init(this);

            // Глобальная обработка исключений для Android
            AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) =>
            {
                Log.Error(args.Exception, "Необработанное исключение");
                args.Handled = true; // Предотвращение завершения приложения
            };

            // Глобальная обработка исключений для .NET
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                Log.Error((Exception) args.ExceptionObject, "Необработанное исключение");
            };

            TaskScheduler.UnobservedTaskException += (sender, args) =>
            {
                Log.Error(args.Exception, "Необработанное исключение в задаче");
                args.SetObserved(); // Предотвращение завершения приложения
            };

            // Копирование базы данных, если она еще не существует
            CopyDatabase();

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}