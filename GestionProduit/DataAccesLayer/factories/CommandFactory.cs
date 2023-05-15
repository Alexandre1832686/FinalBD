using GestionProduit.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProduit.DataAccesLayer.factories
{
    public class CommandFactory
    {
        private Commande CreateFromReader(MySqlDataReader mySqlDataReader)
        {

            DateTime dateLivraison = Convert.ToDateTime(mySqlDataReader["dateLivraison"].ToString());
            DateTime dateCommande = Convert.ToDateTime(mySqlDataReader["dateCommande"].ToString());
            int idCommande = Int32.Parse(mySqlDataReader["idCommande"].ToString());
            int userID = Int32.Parse(mySqlDataReader["idUser"].ToString());

            return new Commande( idCommande,  userID,  dateLivraison,  dateCommande);
        }

        public List<Commande> GetAllCommande()
        {
            List<Commande> commandes = new List<Commande>();
            MySqlConnection mySqlCnn = null;
            MySqlDataReader mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tblcommande ORDER BY idCommande";

                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    Commande c = CreateFromReader(mySqlDataReader);
                    commandes.Add(c);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return commandes;
        }
    }
}
