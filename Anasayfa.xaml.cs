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
using System.Windows.Shapes;
using WPFAkorList.UserControls;
using WPFAkorList.AkorService;

namespace WPFAkorList
{
    /// <summary>
    /// Interaction logic for Anasayfa.xaml
    /// </summary>
    public partial class Anasayfa : Window
    {

        public AkorController AkorCon = new AkorController();

        sarkiciAra UCsanatciAra = new sarkiciAra();
        MenuControl menu = new MenuControl();

        public Anasayfa()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            

            


        }

        private void btn_max_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }

        }

        private void btn_min_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btn_close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnSanatciAra_Click(object sender, RoutedEventArgs e)
        {
            AramaYap();
            
        }

       
        private void AramaYap()
        {
            UCsanatciAra.kelime = txtSanatciAra.Text;
            IcerikSayfasi.Content = UCsanatciAra;
        }

        private void txtSanatciAra_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AramaYap();
            }
        }
        
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (IcerikSayfasi.Content != menu)
            {
                IcerikSayfasi.Content = menu;
            }
            else
            {
                IcerikSayfasi.Content = UCsanatciAra;
            }
        }
    }
}
