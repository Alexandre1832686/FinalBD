using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProduit.Model
{
    internal class ProduitCommande
    {
        public int idCommande { get; set; }
        public int idProduit { get; set; }
        public int quantiteProduit { get; set; }

        public ProduitCommande(int idCommande, int idProduit, int quantiteProduit)
        {
            this.idCommande = idCommande;
            this.idProduit = idProduit;
            this.quantiteProduit = quantiteProduit;
        }
    }
}
