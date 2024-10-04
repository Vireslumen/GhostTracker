using Android.Content;
using Android.Content.Res;
using Android.Webkit;
using GhostTracker.Droid;
using GhostTracker.Models;
using Java.Lang;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;
using WebView = Xamarin.Forms.WebView;

[assembly: ExportRenderer(typeof(WebView), typeof(CustomWebViewRenderer))]

namespace GhostTracker.Droid
{
    /// <summary>
    ///     Класс CustomWebViewRenderer предоставляет пользовательскую реализацию рендера для WebView на платформе Android.
    /// </summary>
    public class CustomWebViewRenderer : WebViewRenderer
    {
        public CustomWebViewRenderer(Context context) : base(context)
        {
        }

        /// <summary>
        ///     Метод, вызываемый при изменении элемента WebView.
        /// </summary>
        /// <param name="e">Аргументы события изменения элемента WebView.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            try
            {
                ConfigureWebViewSettings();
                LoadCustomHtmlContent();
                Control.SetWebViewClient(new CustomWebViewClient(this));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при настройке WebView.");
            }
        }

        /// <summary>
        ///     Настройка параметров WebView.
        /// </summary>
        private void ConfigureWebViewSettings()
        {
            var settings = Control.Settings;
            Control.SetBackgroundColor(Color.Transparent);
            settings.JavaScriptEnabled = true;
            settings.AllowUniversalAccessFromFileURLs = true;
        }

        /// <summary>
        ///     Получение цвета текста в зависимости от текущей темы.
        /// </summary>
        private string GetTextColorBasedOnTheme()
        {
            var uiModeFlags = Context?.Resources?.Configuration?.UiMode & UiMode.NightMask;
            return uiModeFlags == UiMode.NightYes ? "#FFF" : "#444";
        }

        /// <summary>
        ///     Загрузка HTML-контента с кастомными стилями.
        /// </summary>
        private void LoadCustomHtmlContent()
        {
            if (Element.Source is HtmlWebViewSource htmlSource)
            {
                var textColor = GetTextColorBasedOnTheme();
                var css = $@"<style>
                    @font-face {{
                        font-family: 'CustomFont';
                        src: url('file:///android_asset/Overpass_Regular.ttf') format('truetype');
                    }}
                    * {{
                        font-family: 'CustomFont', sans-serif;
                        font-size: 16px;
                        color: {textColor};
                    }}
                    table {{
                      border-radius: 3px;
                      border: 1px solid black;
                      font-size: 14px;
                      text-align: center;
                    }}
                    th, td {{
                      border-radius: 3px;
                      border: 1px solid black;
                      font-size: 14px;
                      text-align: center;
                    }}
                </style>";

                var htmlContent = $"{css}{htmlSource.Html}";
                Control.LoadDataWithBaseURL(null, htmlContent, "text/html", "utf-8", null);
            }
        }

        /// <summary>
        ///     Класс CustomWebViewClient обеспечивает пользовательскую обработку событий WebView.
        /// </summary>
        private class CustomWebViewClient : WebViewClient
        {
            private readonly CustomWebViewRenderer renderer;

            public CustomWebViewClient(CustomWebViewRenderer renderer)
            {
                this.renderer = renderer;
            }

            /// <summary>
            ///     Метод, вызываемый после завершения загрузки страницы.
            /// </summary>
            /// <param name="view">Экземпляр WebView.</param>
            /// <param name="url">URL загруженной страницы.</param>
            public override void OnPageFinished(Android.Webkit.WebView view, string url)
            {
                base.OnPageFinished(view, url);
                try
                {
                    view?.EvaluateJavascript("document.body.scrollHeight", new HeightValueCallback(renderer));
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Ошибка при выполнении JavaScript или обработке после загрузки страницы.");
                }
            }
        }

        /// <summary>
        ///     Класс HeightValueCallback обеспечивает обработку результата выполнения JavaScript для установки высоты WebView.
        /// </summary>
        private class HeightValueCallback : Object, IValueCallback
        {
            private readonly CustomWebViewRenderer renderer;

            public HeightValueCallback(CustomWebViewRenderer renderer)
            {
                this.renderer = renderer;
            }

            /// <summary>
            ///     Метод, вызываемый при получении значения из JavaScript.
            /// </summary>
            /// <param name="value">Значение, возвращенное из JavaScript.</param>
            public void OnReceiveValue(Object value)
            {
                try
                {
                    if (value != null && int.TryParse(value.ToString(), out var height)) renderer.Element.HeightRequest = height;

                    if (renderer.Element.BindingContext is UnfoldingItem item) item.IsExpanded = false;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Ошибка при установке высоты WebView.");
                }
            }
        }
    }
}