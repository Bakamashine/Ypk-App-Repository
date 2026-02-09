using Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Feedback : BaseModel
    {
        public Guid UserId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int Raiting { get; set; }

        public User? User { get; set; }
    }
}
