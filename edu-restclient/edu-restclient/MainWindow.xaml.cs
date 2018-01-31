using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;

namespace De.Dk9mbs.Edu.Restclient
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var locations = ProcessRepositories().Result;

            foreach(var loc in locations)
            {
                System.Diagnostics.Trace.WriteLine(loc.LocationId);
            }

        }


        private async Task<List<Location>> ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            //var stringTask = client.GetStringAsync("http://localhost:56625/api/values");
            //var msg = await stringTask;
            //System.Diagnostics.Trace.WriteLine(msg);

            
            var serializer = new DataContractJsonSerializer(typeof(List<Location>));
            var streamTask = client.GetStreamAsync("http://localhost:56625/api/values");
            var repositories = serializer.ReadObject(await streamTask) as List<Location>;
            return repositories;

        }



    }
}
