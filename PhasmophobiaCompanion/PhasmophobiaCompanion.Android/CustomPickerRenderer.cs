using System;
using System.Linq;
using Android.App;
using Android.Content;
using Serilog;
using PhasmophobiaCompanion.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Picker), typeof(CustomPickerRenderer))]

namespace PhasmophobiaCompanion.Droid
{
    public class CustomPickerRenderer : PickerRenderer
    {
        public CustomPickerRenderer(Context context) : base(context)
        {
        }

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
                    if (model.SelectedIndex != args.Which)
                    {
                        model.SelectedIndex = args.Which;
                        model.SelectedItem = items[args.Which];
                    }
                });

                var dialog = builder.Create();
                dialog.Show();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время нажатия на Picker");
                throw;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Focusable = true;
                Control.Clickable = true;
                Control.Click += Control_Click;
            }
        }
    }
}