using System.ComponentModel.DataAnnotations;

namespace admin_back.Contracts;

public record RegisterUserRequest([Required] string UserName,[Required] string Password);