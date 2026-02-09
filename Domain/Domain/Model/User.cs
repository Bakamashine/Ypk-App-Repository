using Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class User : BaseModel
    {
        public string Fullname { get; set; } = string.Empty;
        public string HashPassword { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public Guid RoleId { get; set; }
        public string? UserInfo { get; set; }
        public bool IsActive { get; set; }

        public Role? Role { get; set; }

        public ICollection<Feedback>? Feedbacks { get; set; }
        public ICollection<Product>? Products { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
