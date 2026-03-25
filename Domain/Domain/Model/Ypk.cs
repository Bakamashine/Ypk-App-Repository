using Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Ypk : BaseModel
    {
        public string YpkName{ get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public ICollection<Product>? Products { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}
