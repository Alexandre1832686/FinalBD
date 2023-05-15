using GestionProduit.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GestionProduit.View
{
    /// <summary>
    /// Logique d'interaction pour MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        public MenuPrincipal()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = new VMGestionProduit();
            InitializeComponent();
        }

        private void Voir_Commande(object sender, RoutedEventArgs e)
        {
            GestionCommande d = new GestionCommande();
            d.Show();
            this.Close();
        }
    }
}
