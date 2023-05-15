using GestionProduit.ViewModel;
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

namespace GestionProduit.View
{
    /// <summary>
    /// Logique d'interaction pour GestionCommande.xaml
    /// </summary>
    public partial class GestionCommande : Window
    {
        public GestionCommande()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = new VMGestionProduit();
            InitializeComponent();
        }

        private void Return_MenuPrincipal(object sender, RoutedEventArgs e)
        {
            MenuPrincipal d = new MenuPrincipal();
            d.Show();
            this.Close();

        }
    }
}
