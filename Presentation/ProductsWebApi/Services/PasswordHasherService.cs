using Aplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsWebApi.Services
{
    public class PasswordHasherService : IPasswordHasherServise
    {
        public string HashPasword(string password) => BCrypt.Net.BCrypt.HashPassword(password, 8);
        public bool VerifyBcryptPassword(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);

    }
}
