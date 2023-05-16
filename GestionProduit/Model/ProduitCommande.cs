using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProduit.Model
{
    public class ProduitCommande
    {
        public int idCommande { get; set; }
        public int idProduit { get; set; }
        public string nomProduit { get; set; }
        public int quantiteProduit { get; set; }

        public ProduitCommande(int idCommande, int idProduit,string nomPRod, int quantiteProduit)
        {
            this.nomProduit = nomPRod;
            this.idCommande = idCommande;
            this.idProduit = idProduit;
            this.quantiteProduit = quantiteProduit;
        }
    }
}
