using GestionProduit.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GestionProduit.DataAccesLayer.factories
{
    /*
     
     
     ALTER TABLE `h23_intro_services_tp5_1832686`.`tblusers` 
CHANGE COLUMN `idUser` `idUser` INT NOT NULL AUTO_INCREMENT ;
     
     */
    public class UserFactory
    {
        private User CreateFromReader(MySqlDataReader mySqlDataReader)
        {

            string prenom = mySqlDataReader["prenomUser"].ToString() ?? string.Empty;
            string nom = mySqlDataReader["nomUser"].ToString() ?? string.Empty;
            int id = Int32.Parse(mySqlDataReader["idUser"].ToString());

            return new User(id,prenom, nom);
        }

        public List<User> GetAllUser()
        {
            List<User> users = new List<User>();
            MySqlConnection mySqlCnn = null;
            MySqlDataReader mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tblusers ORDER BY idUser";

                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    User user = CreateFromReader(mySqlDataReader);
                    users.Add(user);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return users;
        }
        public bool AddUser(string nom, string prenom)
        {
            MySqlConnection mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                                            
                mySqlCmd.CommandText = "Insert into tblusers values (@id,@nom,@prenom)";

                List<User> liste = GetAllUser();
                

                mySqlCmd.Parameters.AddWithValue("@nom", nom);
                mySqlCmd.Parameters.AddWithValue("@prenom", prenom);
                mySqlCmd.Parameters.AddWithValue("@id",liste.Last().Id + 1);

                if(mySqlCmd.ExecuteNonQuery()>0)
                {
                    mySqlCnn?.Close();
                    return true;
                }
            }
            finally
            {
                mySqlCnn?.Close();
            }
            return false;
        }

        public User GetUserById(int id)
        {
            User user = new User();
            MySqlConnection mySqlCnn = null;
            MySqlDataReader mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tblusers WHERE idUser = @id";
                mySqlCmd.Parameters.AddWithValue("@id", id);

                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    user = CreateFromReader(mySqlDataReader);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return user;
        }
    }
}
