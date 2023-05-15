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
    /// Logique d'interaction pour Confirmation.xaml
    /// </summary>
    public partial class Confirmation : Window
    {
        public Confirmation(string message)
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = new VMGestionProduit();
            InitializeComponent();
            Message.Text = message;
        }

        private void Ok_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
