using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProduit.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }

        public User(int id,string prenom, string nom)
        {
            Id = id;
            Prenom = prenom;
            Nom = nom;
        }
        public User() { }
    }
}
