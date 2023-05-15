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
    }
}
