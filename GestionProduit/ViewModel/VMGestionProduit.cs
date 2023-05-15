using GestionProduit.DataAccesLayer;
using GestionProduit.Model;
using GestionProduit.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GestionProduit.ViewModel
{
    public class VMGestionProduit : INotifyPropertyChanged
    {
        #region Déclarations
        DAL dal = new DAL();

        string _prenomAjout;
        string _nomAjout;
        List<User> _listeUsers;
        #endregion

        #region INotifyPropertyChange
        public event PropertyChangedEventHandler PropertyChanged;


        //Fonction qui gère le lancement de l'événément PropertyChanged
        protected virtual void ValeurChangee(string nomPropriete)
        {
            //Vérifie si il y a au moins 1 abonné
            if (this.PropertyChanged != null)
            {
                //Lance l'événement -> tous les abonnés seront notifiés
                this.PropertyChanged(this, new PropertyChangedEventArgs(nomPropriete));
            }
        }
        #endregion

        #region Propriétés pour le Databinding

        public string PrenomAjout
        {
            get
            {
                return _prenomAjout;
            }
            set
            {
                _prenomAjout = value;
                ValeurChangee("PrenomAjout");
            }
        }
        public string NomAjout
        {
            get
            {
                return _nomAjout;
            }
            set
            {
                _nomAjout = value;
                ValeurChangee("NomAjout");
            }
        }
        public List<User> ListUsers
        {
            get
            {
                return _listeUsers;
            }
            set
            {
                _listeUsers = value;
                ValeurChangee("ListUsers");
            }
        }

        #endregion

        #region Binding des commandes

        private ICommand _ajoutUser;
        public ICommand AjouterUser
        {
            get { return _ajoutUser; }
            set { _ajoutUser = value; }
        }

        private void AjouterUser_Execute(object parameter)
        {
            if (dal.UserFactory.AddUser(NomAjout, PrenomAjout))
            {
                Confirmation c = new Confirmation("Utilisateur Ajouté");
                c.Show();
            }
            else
            {
                Confirmation c = new Confirmation("Un Problème est survenu");
                c.Show();
            }
        }

        private bool AjouterUser_CanExecute(object parameter)
        {
            if (PrenomAjout != "" && NomAjout != "" && NotExistUser(PrenomAjout, NomAjout))
            {
                return true;
            }
            return false;
        }

        #endregion

        private bool NotExistUser(string prenom, string nom)
        {
            List<User> liste = dal.UserFactory.GetAllUser();
            foreach (User u in liste)
            {
                if (u.Nom == nom && u.Prenom == prenom)
                {
                    return false;
                }
            }
            return true;
        }

        public VMGestionProduit()
        {
            AjouterUser = new CommandeRelais(AjouterUser_Execute, AjouterUser_CanExecute);
            ListUsers = dal.UserFactory.GetAllUser();
        }
    }
}
