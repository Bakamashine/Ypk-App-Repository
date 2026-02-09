using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface IPasswordHasherServise
    {
        string HashPasword(string password);
        bool VerifyBcryptPassword(string password, string hash);
    }
}
