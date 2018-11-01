using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnXamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearnXamarin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class XkcdPage : ContentPage
	{
		public XkcdPage ()
		{
			InitializeComponent ();
		    BindingContext = new XkcdPageViewModel();

        }
    }

  
}