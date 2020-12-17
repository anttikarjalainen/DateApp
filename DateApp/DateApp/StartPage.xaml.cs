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

namespace DateApp
{
    /// <summary>
    /// Interaction logic for Button1Page.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void NavigateToDb(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("DatabaseView.xaml", UriKind.Relative);
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(uri);
        }

        private void NavigateToDate(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("AddDate.xaml", UriKind.Relative);
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(uri);

        }
    }
}
