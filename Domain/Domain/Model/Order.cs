using Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Order : BaseModel
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid StatusOrderId { get; set; }
        public DateTime Date { get; set; }
        public string? CustomersComment { get; set; }
        public string? UserComment { get; set; }

        public User? User { get; set; }
        public Product? Product { get; set; }
        public StatusOrder? StatusOrder { get; set; }
    }
}
