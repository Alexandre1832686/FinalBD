using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProduit.Model
{
    internal class TypeProduit
    {
        public int idTypeProduit { get; set; }
        public string type { get; set; }
        public string description { get; set; }

        public TypeProduit(int idTypeProduit, string type, string description)
        {
            this.idTypeProduit = idTypeProduit;
            this.type = type;
            this.description = description;
        }
    }
}
