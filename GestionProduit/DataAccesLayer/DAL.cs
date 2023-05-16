using GestionProduit.DataAccesLayer.factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProduit.DataAccesLayer
{
    public class DAL
    {
        private UserFactory userFactory = null;
        private ProductFactory productFactory = null;
        private CommandFactory commandFactory = null;
        private ProduitCommandeFactory produitCommandeFactory = null;

        public static string ConnectionString = "Server=sql.decinfo-cchic.ca;Port=33306;Database=h23_intro_services_tp5_1832686;Uid=dev-1832686;Pwd=Info2020";
        //{ get; set; }

        public UserFactory UserFactory
        {
            get
            {
                if (userFactory == null)
                {
                    userFactory = new UserFactory();
                }

                return userFactory;
            }
        }
        public ProductFactory ProductFactory
        {
            get
            {
                if (productFactory == null)
                {
                    productFactory = new ProductFactory();
                }

                return productFactory;
            }
        }
        public CommandFactory CommandFactory
        {
            get
            {
                if (commandFactory == null)
                {
                    commandFactory = new CommandFactory();
                }

                return commandFactory;
            }
        }
        public ProduitCommandeFactory ProduitCommandeFactory
        {
            get
            {
                if (produitCommandeFactory == null)
                {
                    produitCommandeFactory = new ProduitCommandeFactory();
                }

                return produitCommandeFactory;
            }
        }
    }
}
