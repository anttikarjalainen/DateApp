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
            var i = FirstDate.Text;
            var i2 = SecondDate.Text;
            string url = "http://localhost:7071/api/Function1?" + "pvm1=" + i + "&pvm2=" + i2;
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);
            List<CalendarMark> calendarMark = JsonConvert.DeserializeObject<List<CalendarMark>>(response);
            ListDataBinding.ItemsSource = calendarMark;
        }
    }
    public class CalendarMark
    {
        public CalendarMark(string id, string name, DateTime duedate)
        {
            Id = id;
            Name = name;
            Duedate = duedate;
        }
        public string Name { get; set; }
        public string Id { get; set; }
        public DateTime Duedate { get; set; }

    }
}
