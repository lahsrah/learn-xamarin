using LearnXamarin.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LearnXamarin.ViewModels
{
    public class XkcdPageViewModel : ViewModelBase
    {
        private string _cartoonTitle = "Loading...";
        private ImageSource _cartoonImage;
        private Random _random = null;
        private bool _isLoading = false;
        private const string _xkcdDailyCartoonUrl = @"https://xkcd.com/info.0.json";
        private const string _xkcdNumberedCartoonUrl = @"https://xkcd.com/{0}/info.0.json";
        public Command LoadTodayCartoonCommand { get; }
        public Command LoadRandomCartoonCommand { get; }

        private int TotalComics = 2000;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public string CartoonTitle
        {
            get => _cartoonTitle;
            set
            {
                _cartoonTitle = value;
                OnPropertyChanged();
            }
        }

        public ImageSource CartoonImage
        {
            get => _cartoonImage;
            set
            {
                _cartoonImage = value;
                OnPropertyChanged();
            }

        }

        public XkcdPageViewModel()
        {
            LoadTodayCartoonCommand = new Command(() => Init());
            LoadRandomCartoonCommand = new Command(() => Init(true));
            Init();
        }

        async void Init(bool randomCartoon = false)
        {
            var url = _xkcdDailyCartoonUrl;

            if (randomCartoon)
            {
                if (_random == null) _random = new Random();
                var randomCartoonNumber = _random.Next(1, TotalComics);
                url = string.Format(_xkcdNumberedCartoonUrl, randomCartoonNumber);
            }

            try
            {
                IsLoading = true;
                await LoadCartoon(url, randomCartoon);
            }
            catch (Exception e)
            {
                //TODO: Show error message
            }
            finally
            {
                IsLoading = false;
            }
        }

        async Task LoadCartoon(string url, bool randomCartoon)
        {
            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(url);
                var cartoon = JsonConvert.DeserializeObject<XkcdCartoon>(json);

                if (!string.IsNullOrEmpty(cartoon.Img))
                {
                    var imageBytes = await client.GetByteArrayAsync(cartoon.Img);
                    this.CartoonTitle = cartoon.Title;
                    CartoonImage = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                }

                if (!randomCartoon)
                {
                    TotalComics = cartoon.Num; // Todays comic is the highest number.
                }

            }
        }
    }
}
