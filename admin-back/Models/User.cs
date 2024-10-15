using admin_back.Contracts;

namespace admin_back.Models;

public class User
{
    public User(Guid id, string login, string passwordHash)
    {
        Id = id;
        Login = login;
        PasswordHash = passwordHash;
    }

    public Guid Id { get; set; }
    public string Login { get; private set; }
    public string PasswordHash { get; private set; }

    public static User Create(Guid id, string login, string passwordHash)
    {
        return new User(id, login, passwordHash);
    }
}