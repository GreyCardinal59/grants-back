using System.Text.Json;
using admin_back.Contracts;
using admin_back.DataAccess;
using admin_back.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace admin_back.Controllers;

[ApiController]
[Route("admin/api/v1/auth")]
public class UsersController(UsersService usersService,UsersDbContext dbContext) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IResult> Register([FromBody]RegisterUserRequest request, UsersService usersService)
    {
        await usersService.Register(request.UserName, request.Password);
        
        return Results.Ok();
    }
    
    [HttpPost ("login")]
    public async Task<IResult> Login(LoginUserRequest request, UsersService usersService)
    {
        if (request == null)
        {
            return Results.BadRequest("Error");
        }

        try
        { 
            var token = await usersService.Login(request.Login, request.Password);
            
           HttpContext.Response.Cookies.Append("tasty-cookies", token, new CookieOptions
           {
               HttpOnly = true,
               Secure = true,
               SameSite = SameSiteMode.Strict
           });
           // contextAccessor.HttpContext?.Response.Cookies.Append("tasty-cookies", token);
            
           return Results.Ok(token);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [Authorize]
    [HttpPost("check")]
    public IActionResult CheckAuth()
    {
        return NoContent();
    }
}