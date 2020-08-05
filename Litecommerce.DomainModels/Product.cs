using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litecommerce.DomainModels
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public int UnitPrice { get; set; }
        public string Descriptions { get; set; }
        public string PhotoPath { get; set; }

        //public virtual Category Category { get; set; }
        //public virtual Supplier Supplier { get; set; }
    }
}
