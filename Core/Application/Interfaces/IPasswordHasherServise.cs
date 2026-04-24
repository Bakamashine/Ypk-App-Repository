namespace Application.Interfaces;

public interface IPasswordHasherServise
{
    string HashPasword(string password);
    bool VerifyBcryptPassword(string password, string hash);
}