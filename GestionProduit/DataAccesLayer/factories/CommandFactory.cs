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
        DAL dal = new DAL();
        private Commande CreateFromReader(MySqlDataReader mySqlDataReader)
        {

            DateTime dateLivraison = Convert.ToDateTime(mySqlDataReader["dateLivraison"].ToString());
            DateTime dateCommande = Convert.ToDateTime(mySqlDataReader["dateCommande"].ToString());
            int idCommande = Int32.Parse(mySqlDataReader["idCommande"].ToString());
            int userID = Int32.Parse(mySqlDataReader["idUser"].ToString());
            string username = dal.UserFactory.GetUserById(userID).Prenom;
            string userlastname = dal.UserFactory.GetUserById(userID).Nom;

            return new Commande( idCommande,  userID,username,userlastname,  dateLivraison,  dateCommande);
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

        public bool SaveNewCommand(Commande c, List<Tuple<Produit,int>> produitcommande)
        {
            bool check = true;
            MySqlConnection mySqlCnn = null;
            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "Insert into tblcommande values (@id,@iduser,@datelivraison,@dateCommande)";

                mySqlCmd.Parameters.AddWithValue("@id", getlastidCommand());
                mySqlCmd.Parameters.AddWithValue("@iduser", c.UserID);
                mySqlCmd.Parameters.AddWithValue("@datelivraison", c.DateLivraison);
                mySqlCmd.Parameters.AddWithValue("@dateCommande", c.DateCommande);

                

                if (mySqlCmd.ExecuteNonQuery() == 0)
                {
                    mySqlCnn?.Close();
                    check = false;
                }
                

                for (int i = 0; i < produitcommande.Count; i++)
                {
                    mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                    mySqlCnn.Open();
                    mySqlCmd.CommandText = "Insert into tblproduitcommande values (" + c.IdCommande + ","+ produitcommande[i].Item1.NoProduit + ","+ produitcommande[i].Item2 + ")";

                    if (mySqlCmd.ExecuteNonQuery() == 0)
                    {
                        check = false;
                    }
                    mySqlCnn?.Close();
                }
            }
            finally
            {
                mySqlCnn?.Close();
            }
            return check;
        }

        public int getlastidCommand()
        {
            return GetAllCommande().Last().IdCommande + 1;
        }

        
    }
}
