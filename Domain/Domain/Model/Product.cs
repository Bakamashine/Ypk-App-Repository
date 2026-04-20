using Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Product : BaseModel
    {
        public string ProductName { get; set; } = string.Empty;
        public Guid YpkId { get; set; }
        public Guid UserId { get; set; }
        public Guid StatusProductId { get; set; }
        public decimal ProductCost{ get; set; }
        public string ProductInfo { get; set; } = string.Empty;
        public bool IsProduct { get; set; }
        public string? PhotoPath { get; set; }
        public string Adress { get; set; } = string.Empty;



        public Ypk? Ypk { get; set; }
        public User? User { get; set; }
        public StatusProduct? StatusProduct { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
