using StandardStorage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Jukebox
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    //[DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ViewModel();
            /*
             *
            List<string> folders = new List<string>();
            foreach (Environment.SpecialFolder folder in (Environment.SpecialFolder[])Enum.GetValues(typeof(Environment.SpecialFolder)))
            {
                folders.Add(System.Environment.GetFolderPath(folder));
            }
            */
        }

        static void PrintFolderPath(System.Environment.SpecialFolder folder) => Console.WriteLine($"{folder}={System.Environment.GetFolderPath(folder)}");
    }

    public class ViewModel : INotifyPropertyChanged
    {
        private const string DefaultUrl = "http://localhost:8080/";

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _result;
        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged("Result");
            }
        }

        public ICommand AcceptEncoding_None_Command { get; set; }
        public ICommand AcceptEncoding_Gzip_Command { get; set; }

        public ViewModel()
        {
            AcceptEncoding_None_Command = new Command(async () => await AcceptEncoding_None());
            AcceptEncoding_Gzip_Command = new Command(async () => await AcceptEncoding_Gzip());

            Result = "Result will appear here";
        }

        private async Task AcceptEncoding_None()
        {
            try
            {
                Result = $"Trying AcceptEncoding = None{System.Environment.NewLine}";

                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync($"{DefaultUrl}api/testresponse").ConfigureAwait(false))
                    {
                        var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        Result += "Result = " + (string.IsNullOrEmpty(responseString) ? "<Empty>" : responseString);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private async Task AcceptEncoding_Gzip()
        {
            try
            {
                Result = $"Trying AcceptEncoding = Gzip{System.Environment.NewLine}";

                var handler = new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                };

                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                    client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

                    using (var response = await client.GetAsync($"{DefaultUrl}api/testresponse").ConfigureAwait(false))
                    {
                        var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        Result += "Result = " + (string.IsNullOrEmpty(responseString) ? "<Empty>" : responseString);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
