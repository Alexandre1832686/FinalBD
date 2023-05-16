using GestionProduit.DataAccesLayer;
using GestionProduit.Model;
using GestionProduit.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<User> _listeUsers;
        ObservableCollection<Produit> _listeProduits;
        ObservableCollection<Tuple<Produit,int>> _commande;
        int _quantite;
        Produit _produitSelected;
        ObservableCollection<Commande> _listeCommande;
        User _userSelected;
        Commande _commandeChoisi;
        ObservableCollection<ProduitCommande> _produitCommandeChoisi;
        Tuple<Produit,int> _produitSelectedCommande;
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
        public ObservableCollection<User> ListUsers
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

        public ObservableCollection<Produit> ListeProduits
        {
            get
            {
                return _listeProduits;
            }
            set
            {
                _listeProduits = value;
                ValeurChangee("ListeProduits");
            }
        }

        public ObservableCollection<Tuple<Produit, int>> CommandeMenu
        {
            get
            {
                return _commande;
            }
            set 
            { 
                _commande = value;
                ValeurChangee("CommandeMenu");
            }
        }

        public int Quantite
        {
            get { return _quantite; }
            set
            {
                _quantite = value;
                ValeurChangee("Quantite");
            }
        }

        public Produit ProduitSelected
        {
            get
            {
                return _produitSelected;
            }
            set
            {
                _produitSelected = value;
                ValeurChangee("ProduitSelected");
            }
        }

        public User UserSelected
        {
            get
            {
                return _userSelected;
            }
            set
            {
                _userSelected = value;
                ValeurChangee("UserSelected");
            }
        }
        public Tuple<Produit, int> ProduitSelectedCommande
        {
            get
            {
                return _produitSelectedCommande;
            }
            set
            {
                _produitSelectedCommande = value;
                ValeurChangee("ProduitSelectedCommande");
            }
        }
        public Commande CommandeChoisi
        {
            get
            {
                return _commandeChoisi;
            }
            set
            {
                ProduitCommandeChoisi = new ObservableCollection<ProduitCommande>( dal.ProduitCommandeFactory.GetByID(value.IdCommande) );
                _commandeChoisi = value;
                ValeurChangee("CommandeChoisi");
            }
        }


        public ObservableCollection<Commande> ListeCommande
        {
            get
            {
                return _listeCommande;
            }
            set
            {
                _listeCommande = value;
                ValeurChangee("ListeCommande");
            }
        }
        public ObservableCollection<ProduitCommande> ProduitCommandeChoisi
        {
            get
            {
                return _produitCommandeChoisi;
            }
            set
            {
                _produitCommandeChoisi = value;
                ValeurChangee("ProduitCommandeChoisi");
            }
        }

        #endregion

        #region Binding des commandes
        private ICommand _envoyerCommande;
        public ICommand EnvoyerCommande
        {
            get { return _envoyerCommande; }
            set { _envoyerCommande = value; }
        }
        private ICommand _retirer;
        public ICommand Retirer
        {
            get { return _retirer; }
            set { _retirer = value; }
        }
        private ICommand _ajouterProduit;
        public ICommand AjouterProduit
        {
            get { return _ajouterProduit; }
            set { _ajouterProduit = value; }
        }

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
            ListUsers = new ObservableCollection<User>( dal.UserFactory.GetAllUser() );
            
        }

        private bool AjouterUser_CanExecute(object parameter)
        {
            if (PrenomAjout != "" && NomAjout != "" && NotExistUser(PrenomAjout, NomAjout))
            {
                return true;
            }
            return false;
        }
        private void retirer_Execute(object parameter)
        {
            Tuple<Produit, int> produitcontext = null;
            foreach(Tuple<Produit,int> produit in CommandeMenu)
            {
                if(produit.Item1.NoProduit == ProduitSelectedCommande.Item1.NoProduit)
                {
                    produitcontext = produit;
                }
            }
            if(produitcontext != null)
            {
                CommandeMenu.Remove(produitcontext);
            }
        }

        private bool retirer_CanExecute(object parameter)
        {
            if (ProduitSelectedCommande != null)
            {
                return true;
            }
            return false;
        }
        private void AjouterProduit_Execute(object parameter)
        {
            Tuple<Produit, int> existingproduct = null;
            bool present = false;
            foreach(Tuple<Produit,int> context in CommandeMenu)
            {
                if (ProduitSelected.NoProduit == context.Item1.NoProduit)
                {
                    present = true;
                    existingproduct = context;
                }
            }
            
            if(!present)
            {
                List<Tuple<Produit, int>> liste = CommandeMenu.ToList();
                liste.Add(Tuple.Create(ProduitSelected, Quantite));
                CommandeMenu = new ObservableCollection<Tuple<Produit, int>>(liste);
            }
            else
            {
                int prequantite = existingproduct.Item2;

                CommandeMenu.Remove(existingproduct);

                List<Tuple<Produit, int>> liste = CommandeMenu.ToList();
                liste.Add(Tuple.Create(ProduitSelected, Quantite + prequantite));
                CommandeMenu = new ObservableCollection<Tuple<Produit, int>>(liste);
            }
            
        }

        private bool AjouterProduit_CanExecute(object parameter)
        {
            if (Quantite != 0 && ProduitSelected != null && Quantite < 100 && Quantite>0)
            {
                return true;
            }
            return false;
        }

        private bool EnvoyerCommande_CanExecute(object parameter)
        {
            if (CommandeMenu.Count > 0 && UserSelected != null)
            {
                return true;
            }
            return false;
        }

        private void EnvoyerCommande_Execute(object parameter)
        {
            Commande c = new Commande(dal.CommandFactory.getlastidCommand(), UserSelected.Id,UserSelected.Prenom, UserSelected.Nom, DateTime.Now.AddDays(3), DateTime.Now);
            ListeCommande.Add(c);
            if(dal.CommandFactory.SaveNewCommand(c,CommandeMenu.ToList()))
            {
                Confirmation confi = new Confirmation("Commande ajouté!");
                confi.Show();
                CommandeMenu.Clear();
                UserSelected = null;
                ProduitSelected = null;
                Quantite = 0;
            }
            else
            {
                Confirmation confi = new Confirmation("Il y a eu un problème lors de votre commande");
                confi.Show();
            }
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
            AjouterProduit = new CommandeRelais(AjouterProduit_Execute, AjouterProduit_CanExecute);
            Retirer = new CommandeRelais(retirer_Execute, retirer_CanExecute);
            EnvoyerCommande = new CommandeRelais(EnvoyerCommande_Execute, EnvoyerCommande_CanExecute);
            AjouterUser = new CommandeRelais(AjouterUser_Execute, AjouterUser_CanExecute);
            ListUsers = new ObservableCollection<User>( dal.UserFactory.GetAllUser());
            ListeProduits = new ObservableCollection<Produit>(dal.ProductFactory.GetAllProduit());
            ListeCommande = new ObservableCollection<Commande>(dal.CommandFactory.GetAllCommande());
            CommandeMenu = new ObservableCollection<Tuple<Produit, int>>();
        }

        
    }
}
