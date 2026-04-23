using Application.Interfaces;

namespace ProductsWebApi.Services;

public class PasswordHasherService : IPasswordHasherServise
{
    public string HashPasword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, 8);
    }

    public bool VerifyBcryptPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}