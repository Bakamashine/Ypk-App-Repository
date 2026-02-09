using Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class StatusProduct : BaseModel
    {
        public string StatusName { get; set; } = string.Empty;


        public ICollection<Product>? Products { get; set; }
    }
}
