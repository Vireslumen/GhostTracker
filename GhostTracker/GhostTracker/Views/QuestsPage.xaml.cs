using GhostTracker.Models;
using GhostTracker.ViewModels;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GhostTracker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QuestsPage : ContentPage
    {
        public QuestsPage ()
        {
			try
			{
				InitializeComponent();
				var viewModel = new QuestsViewModel();
				BindingContext = viewModel;
			}
			catch (Exception ex)
			{
                Log.Error(ex, "Ошибка во время инициализации QuestsPage.");
			}
        }
	}
}