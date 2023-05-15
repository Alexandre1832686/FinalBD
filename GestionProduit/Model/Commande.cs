using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProduit.Model
{
    public class Commande
    {
       
        public int IdCommande { get; set; }
        public int UserID { get; set; }

        public DateTime DateLivraison { get; set; }
        public DateTime DateCommande { get; set; }

        public Commande(int idCommande, int userID, DateTime dateLivraison, DateTime dateCommande)
        {
            
            IdCommande = idCommande;
            UserID = userID;
            DateLivraison = dateLivraison;
            DateCommande = dateCommande;
        }
        
    }
}
