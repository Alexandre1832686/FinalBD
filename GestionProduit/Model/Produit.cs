using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProduit.Model
{
    public class Produit
    {
        public int NoProduit { get; set; }
        public int NoTypeProduit { get; set; }
        public string NomProduit { get; set; }

        public Produit(int noProduit, int noTypeProduit, string nomProduit)
        {
            NoProduit = noProduit;
            NoTypeProduit = noTypeProduit;
            NomProduit = nomProduit;
        }
        public Produit(){}
    }
}
