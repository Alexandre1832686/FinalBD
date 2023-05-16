using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using GestionProduit.Model;

namespace GestionProduit.DataAccesLayer.factories
{
    public class ProduitCommandeFactory
    {
        DAL dal = new DAL();
        private ProduitCommande CreateFromReader(MySqlDataReader mySqlDataReader)
        {

            int idCommande = Int32.Parse(mySqlDataReader["idCommande"].ToString());
            int idProduit = Int32.Parse(mySqlDataReader["idProduit"].ToString());
            int quantiteProduit = Int32.Parse(mySqlDataReader["quantiteProduit"].ToString());
            string nom = dal.ProductFactory.GetProduitbyid(idProduit).NomProduit;

            return new ProduitCommande(idCommande, idProduit,nom, quantiteProduit);
        }

        public List<ProduitCommande> GetByID(int id)
        {
            List<ProduitCommande> produits = new List<ProduitCommande>();
            MySqlConnection mySqlCnn = null;
            MySqlDataReader mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tblproduitcommande WHERE idCommande=@id";
                mySqlCmd.Parameters.AddWithValue("@id", id);
                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    ProduitCommande p = CreateFromReader(mySqlDataReader);
                    produits.Add(p);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return produits;
        }
    }
}
