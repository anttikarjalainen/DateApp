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

        public  async void GetMarks(object sender, RoutedEventArgs e)
        {
            //määritetään url
            string url = "https://datetestapp.azurewebsites.net/api/HttpGET";

            try
            {
                using (HttpClient client = new HttpClient())

                {
                    using (HttpResponseMessage res = await client.GetAsync(url))
                    {
                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();

                            if (data != null)
                            {

                                // tähän tulostus käyttöliittymään
                                Console.WriteLine("data------------{0}", data);
                                var dataObj = JArray.Parse(data);
                                dynamic result = JObject.Parse(dataObj[0].ToString());
                                this.displayName.Text = result.name;
                            }
                            else
                            {
                                Console.WriteLine("NO Data ------------");
                            }
                        } 
                    }

                }
            } catch (Exception exception)
            {
                // error handlaus tänne
            }


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
