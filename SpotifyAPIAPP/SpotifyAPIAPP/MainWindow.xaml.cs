//Alexander Twigg
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
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;

namespace SpotifyAPIAPP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //AutorizationCodeAuth A = new AutorizationCodeAuth()
        //{
        //    ClientId = "84654d712193471199189e4f593781dc",
        //    
        //}
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            SpotifyWebAPI Spot1 = new SpotifyWebAPI()
            {
                AccessToken = "84654d712193471199189e4f593781dc",

            };
            //var search = Spot1.SearchItems(txtSearch.Text, SearchType.Artist);
            var result = Spot1.GetArtist(txtSearch.Text);
            lstArtist.Items.Add(result.Name);
            

        }
    }
}
