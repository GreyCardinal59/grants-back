namespace admin_back.Interfaces;

public interface IPasswordHasher
{
    string Generate(string password);

    bool Verify(string password, string hashedPassword) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
}