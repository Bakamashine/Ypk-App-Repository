using Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class SelectedProducts : BaseModel
    {    
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }


        public User? User { get; set; }
        public Product? Product { get; set; }
}
}
