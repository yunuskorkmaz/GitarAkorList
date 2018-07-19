using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
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
using WPFAkorList.AkorService;

namespace WPFAkorList.UserControls
{
    /// <summary>
    /// Interaction logic for sarkiciAra.xaml
    /// </summary>
    public partial class sarkiciAra : UserControl
    {


        public string kelime
        {   get
            {
                return kelime;
            }
            set
            {
                SonucAl(value);
            }
        }
        public ObservableCollection<SearchSanatci> SanactiSonuclari;
        public ObservableCollection<SearchAkor> AkorSonuclari;

        AkorController bot = new AkorController();

        private ResultSearch Sonuclar;
        public sarkiciAra()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void SonucAl(string a) 
        {

            lbBulunanSarkici.SelectedIndex = -1;
            lbBulunanAkor.SelectedIndex = -1;

            

            ResultSearch Sonuclar = new ResultSearch();

                Sonuclar = bot.AramaYap(a);

                 SanactiSonuclari = Sonuclar.Sanatcilar;
                 AkorSonuclari = Sonuclar.Akorlar;

            if (SanactiSonuclari.Count == 0)
            {
               
                GridLength yt = new GridLength(0.0);    
                GridSanatci.ColumnDefinitions[0].Width = yt;
            }
            else
            {
                GridLength yt = new GridLength(20.0, GridUnitType.Star);
                GridSanatci.ColumnDefinitions[0].Width = yt;
            }

                lbBulunanAkor.ItemsSource = AkorSonuclari;
                lbBulunanSarkici.ItemsSource = SanactiSonuclari;
            
           
        }
        private void AkorBul(string link)
        {
            lbBulunanAkor.ItemsSource = bot.SanatciAkorList(link);
        }

        private void lbBulunanSarkici_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lbBulunanSarkici.SelectedIndex != -1)
            {
                lbBulunanAkor.SelectedIndex = -1;
                SearchSanatci secim = lbBulunanSarkici.SelectedItems[0] as SearchSanatci;

                if (secim != null)
                {
                    AkorBul(secim.Link);


                }
            }
           

        }


        private void AkorDetay(string link)
        {

            Akor a = new Akor();
            a = bot.AkorDetayGetir(link) as Akor;
            txtSeciliAkor.Text = a.AkorDetay;

        

        }

        private void lbBulunanAkor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if(lbBulunanAkor.SelectedIndex != -1)
            {
                SearchAkor secim = lbBulunanAkor.SelectedItems[0] as SearchAkor;
                
                if (secim != null)
                {

                    AkorDetay(secim.Link);
                    txtSeciliAkor.FontSize = Properties.Settings.Default.Veriler;
                    akorMenu.Visibility = Visibility.Visible;
                }
            }

            
           
        }
        
        

        private void imgMinus_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtSeciliAkor.FontSize > 12)
            {
                txtSeciliAkor.FontSize -= 2;

            }
        }

        private void imgSave_MouseDown(object sender, MouseButtonEventArgs e)
        {

            Properties.Settings.Default.Veriler = (int)txtSeciliAkor.FontSize;
            Properties.Settings.Default.Save();
        }

        private void imgPlus_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtSeciliAkor.FontSize < 20)
            {
                txtSeciliAkor.FontSize += 2;
            }
        }
    }
}
