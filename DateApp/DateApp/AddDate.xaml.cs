using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
namespace DateApp
{
    /// <summary>
    /// Interaction logic for AddDate.xaml
    /// </summary>
    /// 

    public partial class AddDate : Page
    {
        public AddDate()
        {
            InitializeComponent();
        }
        private async void SendToDatabase(object sender, RoutedEventArgs e)
        {
            var apiURL = "http://datetestapp.azurewebsites.net/api/HttpTrigger1?name=";
            string usertext = UserText.Text;
            string userdate = UserDate.Text;
            var client = new HttpClient();
            var secondUrl = usertext + "&duedate=" + userdate;

            // simppeli Get-requesti Azure-functioon joka pyörii azuressa
            var request = new HttpRequestMessage(HttpMethod.Get, apiURL + secondUrl);
            HttpResponseMessage response = await client.SendAsync(request);

            // printataan teksti
            this.displayText.Text = "Onnistui!";
        }
    }
}
