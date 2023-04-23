using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Termin3.DataAccess.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public virtual ICollection<OrderProduct> ProductOrders { get; set; } = new HashSet<OrderProduct>();
    }
}
