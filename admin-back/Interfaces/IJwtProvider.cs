using admin_back.Models;

namespace admin_back.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(User user);
}