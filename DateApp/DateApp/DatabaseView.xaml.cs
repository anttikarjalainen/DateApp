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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DateApp
{
    /// <summary>
    /// Interaction logic for DatabaseView.xaml
    /// </summary>
    public partial class DatabaseView : Page
    {
        public DatabaseView()
        {
            InitializeComponent();
        }
        public async void LoadData(object sender, RoutedEventArgs e)
        {
            string url = "https://datetestapp.azurewebsites.net/api/HttpGET";
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);
            // tässä päivämäärä jo väärässä muodossa?
            List<CalendarMark> calendarMark = JsonConvert.DeserializeObject<List<CalendarMark>>(response);
            // halutaan muuttaa date stringit datetimeksi ja tulostaa vain tietyltä aikaväliltä(Pvm 1 ja Pvm 2)
            // jostain syystä vaikka Azure cosmos DB:SSÄ päivämäärät muotoa "yyyy-MM-ddTHH:mm:ss.fffffffZ" httpGet-kutsun jälkeen ovat muotoa  "DD-MM-YYYY HH:MM:SS"     

            // testataan kovakoodatuilla arvoilla
            List<CalendarMark> calendarList = new List<CalendarMark>();
            calendarList.Add(new CalendarMark("1", "Antti", "2022-12-18T20:15:00.0000000Z"));
            calendarList.Add(new CalendarMark("2", "Pekka", "2022-12-18T10:30:00.0000000Z"));
            calendarList.Add(new CalendarMark("3", "Juuso", "2022-12-18T19:56:00.0000000Z"));

            foreach (var i in calendarList )
            {
                DateTime toDate = DateTime.Now;
                DateTime oDate = DateTime.Parse(i.Duedate);             
                if(toDate <= oDate)
                {
                    MessageBox.Show("Läp tuli!" + i.Duedate);
                    List<CalendarMark> filteredList = new List<CalendarMark>();
                    filteredList.Add(new CalendarMark(i.Id, i.Name, i.Duedate));
                    ListDataBinding.ItemsSource = filteredList;
                }
                else
                {
                    MessageBox.Show("EI löytynyt mitään! Yritä uusilla päivämäärillä!");
                }
            }
            //ListDataBinding.ItemsSource = calendarMark;
        }
    }
    public class CalendarMark
    {
        public CalendarMark(string id, string name, string duedate)
        {
            Id = id;
            Name = name;
            Duedate = duedate;
        }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Duedate { get; set; }

    }
}
