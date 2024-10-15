using System.ComponentModel.DataAnnotations;

namespace admin_back.Contracts;

public record LoginUserRequest([Required] string Login, [Required] string Password);