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
            lstArtist.Items.Clear();
            lstAlbum.Items.Clear();
            lstSong.Items.Clear();
            lstArtist.SelectedIndex = -1;
            lstAlbum.SelectedIndex = -1;
            lstSong.SelectedIndex = -1;
            imgalbum.Source = null;
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
            try
            {
                SearchItem search = Spot1.SearchItems(txtSearch.Text, SearchType.Artist);
                var s = search.Artists.Items.ToList();
                //lstArtist.Items.Add($"{s[0].Name}/{s[0].Id}");
                foreach (var item in s)
                {
                    ListBoxItem lbi = new ListBoxItem();
                    lbi.Content = item.Name;
                    lbi.Tag = item;
                    lstArtist.Items.Add(lbi);// $"{item.Name}/{item.Id}");
                }
            }
            catch(Exception ex)
            {//
                MessageBox.Show("Please enter a valid artist!", "ALERT!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                MessageBox.Show(ex.Message);
            }
            //var selection = lstAlbum.SelectedItem.ToString().Split('/');
            //Paging<SimpleTrack> st = Spot1.GetAlbumTracks(selection[1]);
            //st.Items.ForEach(x => lstSong.Items.Add(x.Name));
            //lstAlbum.Items.
            //FullArtist A = Spot1.GetArtist("1KpCi9BOfviCVhmpI4G2sY");
            //MessageBox.Show(A.Name);
            
        }

        private void lstArtist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            lstAlbum.Items.Clear();
            lstAlbum.SelectedIndex = -1;
            lstSong.SelectedIndex = -1;
            
            
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
            try
            {
                var s = search.Artists.Items.ToList();
                
                var selection = (FullArtist)((ListBoxItem) lstArtist.SelectedItem).Tag;
                Paging<SimpleAlbum> sev = Spot1.GetArtistsAlbums(selection.Id, AlbumType.All);
                //sev.Items.ForEach(x => lstAlbum.Items.Add($"{x.Name}"));
                var sevs = sev.Items.ToList();
                foreach (var item in sevs)
                {
                    ListBoxItem lbi = new ListBoxItem();
                    lbi.Content = item.Name;
                    lbi.Tag = item;
                    lstAlbum.Items.Add(lbi);// $"{item.Name}/{item.Id}");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                lstArtist.SelectedIndex = -1;
            }
        }

        private void lstAlbum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstSong.Items.Clear();
            txtbDesc.Inlines.Clear();
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
            try
            {
                
                var selection = (SimpleAlbum)((ListBoxItem) lstAlbum.SelectedItem).Tag;
                Paging<SimpleTrack> st = Spot1.GetAlbumTracks(selection.Id);
                st.Items.ForEach(x => lstSong.Items.Add(x.Name));
                FullAlbum imgs = Spot1.GetAlbum(selection.Id);
                
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(imgs.Images[0].Url);
                img.EndInit();
                imgalbum.Source = img;

                txtbDesc.Inlines.Add($"Popularity rating: {imgs.Popularity.ToString()}\nReleased on: {imgs.ReleaseDate}");
                //txtbDesc.Inlines.Add($"");
                
                
                
                
                
                
                   
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                lstArtist.Items.Clear();
                lstAlbum.Items.Clear();
                lstSong.Items.Clear();
            }
            
        }
    }
}
