using System;
using System.Linq;
using Android.App;
using Android.Content;
using GhostTracker.Droid;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Picker), typeof(CustomPickerRenderer))]

namespace GhostTracker.Droid
{

    /// <summary>
    ///     Класс CustomPickerRenderer - рендерер для элемента Picker на платформе Android.
    ///     Позволяет кастомизировать поведение и внешний вид стандартного элемента Picker.
    /// </summary>
    public class CustomPickerRenderer : PickerRenderer
    {
        public CustomPickerRenderer(Context context) : base(context)
        {
        }

        /// <summary>
        ///     Метод-обработчик события клика на элемент управления Picker.
        ///     Отображает диалоговое окно для выбора значений из списка.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private void Control_Click(object sender, EventArgs e)
        {
            try
            {
                var model = Element;
                if (model.Items == null || model.Items.Count == 0)
                    return;

                var builder = new AlertDialog.Builder(Context);
                builder.SetTitle(model.Title ?? "");

                var items = model.Items.ToArray();
                builder.SetItems(items, (s, args) =>
                {
                    if (model.SelectedIndex == args.Which) return;
                    model.SelectedIndex = args.Which;
                    model.SelectedItem = items[args.Which];
                });

                var dialog = builder.Create();
                dialog?.Show();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка в появлении диалогового окна у Picker.");
            }
        }

        /// <summary>
        ///     Метод, вызываемый при изменении элемента управления Picker.
        ///     Устанавливает фокусируемость и кликабельность для элемента, а также добавляет обработчик события клика.
        /// </summary>
        /// <param name="e">Аргументы изменения элемента.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control == null) return;
            Control.Focusable = true;
            Control.Clickable = true;
            Control.Click += Control_Click;
        }
    }
}