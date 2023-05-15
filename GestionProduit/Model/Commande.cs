using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProduit.Model
{
    internal class Commande
    {
        public List<Tuple<Produit, int>> ListProduit { get; set; }
        public int IdCommande { get; set; }
        public string UserID { get; set; }
    }
}
