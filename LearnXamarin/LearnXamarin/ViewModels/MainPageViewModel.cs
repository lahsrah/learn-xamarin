using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using LearnXamarin.Views;
using Xamarin.Forms;

namespace LearnXamarin.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public Command ShowXkcdCommand { get; }

        public MainPageViewModel()
        {
            ShowXkcdCommand = new Command(ShowXkcdPage);
        }

        public string Message { get; set; } = "Hello World";

        private void ShowXkcdPage()
        {
            Application.Current.MainPage.Navigation.PushAsync(new XkcdPage());
        }
    }
}
