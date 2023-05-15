using GestionProduit.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProduit.DataAccesLayer.factories
{
    public class ProductFactory
    {
        private Produit CreateFromReader(MySqlDataReader mySqlDataReader)
        {

            string nom = mySqlDataReader["nomProduit"].ToString() ?? string.Empty;
            int idProduit = Int32.Parse(mySqlDataReader["idProduit"].ToString());
            int idTypeProduit = Int32.Parse(mySqlDataReader["idTypeProduit"].ToString());

            return new Produit(idProduit, idTypeProduit, nom);
        }

        public List<Produit> GetAllProduit()
        {
            List<Produit> produits = new List<Produit>();
            MySqlConnection mySqlCnn = null;
            MySqlDataReader mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tblproduit ORDER BY idProduit";

                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    Produit p = CreateFromReader(mySqlDataReader);
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
