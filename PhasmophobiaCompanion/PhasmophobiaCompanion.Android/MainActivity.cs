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
                // Путь к локальной базе данных
                var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var dbPath = Path.Combine(folderPath, "phasmaDATADB.db");
                if (!File.Exists(dbPath))
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
                Log.Error((Exception)args.ExceptionObject, "Необработанное исключение");
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