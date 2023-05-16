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
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public DateTime DateLivraison { get; set; }
        public DateTime DateCommande { get; set; }

        public Commande(int idCommande, int userID, string Username,string userLastName, DateTime dateLivraison, DateTime dateCommande)
        {
            UserLastName = userLastName;
            IdCommande = idCommande;
            UserID = userID;
            UserName = Username;
            DateLivraison = dateLivraison;
            DateCommande = dateCommande;
        }
        
    }
}
