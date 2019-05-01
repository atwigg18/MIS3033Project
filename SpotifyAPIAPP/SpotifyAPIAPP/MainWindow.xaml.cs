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
        


        //AutorizationCodeAuth A = new AutorizationCodeAuth(Clie)
        
        public MainWindow()
        {       
         InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
                     
            string cid = "84654d712193471199189e4f593781dc";
            string sid = "ab7ead187ddd4f0aaa17542b26ca8d5e";

            ClientCredentialsAuth C = new ClientCredentialsAuth()
            {
                ClientId = cid,
                ClientSecret = sid,
            };
            Token T = C.DoAuth();
            

            SpotifyWebAPI Spot1 = new SpotifyWebAPI()
            {
                UseAuth = true,
                AccessToken = T.AccessToken,
                UseAutoRetry = true,
                TokenType = T.TokenType,
                
            };
            SearchItem search = Spot1.SearchItems(txtSearch.Text, SearchType.Artist);
            var s = search.Artists.Items.ToList();
            lstArtist.Items.Add(s[0].Name);
            Paging<SimpleAlbum> sev = Spot1.GetArtistsAlbums(s[0].Id, AlbumType.All);
            sev.Items.ForEach(x => lstAlbum.Items.Add(x.Name));
            //Paging<SimpleTrack> st = Spot1.GetAlbumTracks(lstAlbum)
            //lstAlbum.Items.
            //FullArtist A = Spot1.GetArtist("1KpCi9BOfviCVhmpI4G2sY");
            //MessageBox.Show(A.Name);
            
        }
    }
}
