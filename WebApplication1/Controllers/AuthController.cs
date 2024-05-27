using Microsoft.AspNetCore.Mvc;
using NeonTechAspNetCore.Data;
using NeonTechAspNetCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using static WebApplication1.helpers.AuthHelpers;
using WebApplication1.helpers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthHelpers _authHelpers;
    private readonly ApplicationDbContext _context;

    public AuthController(AuthHelpers authHelpers, ApplicationDbContext context)
    {
        _authHelpers = authHelpers;
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(ApplicationUser user)
    {
        // Guarda el usuario en la base de datos.
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(ApplicationUser user)
    {
        // Busca al usuario en la base de datos.
        var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);

        // Verifica la contraseña.
        if (dbUser == null || dbUser.Password != user.Password) // En una aplicación real, debes hashear y verificar la contraseña de manera segura.
        {
            return Unauthorized();
        }

        // Genera un token JWT.
        var token = _authHelpers.GenerateJWTToken(new SystemUser { Id = dbUser.Id, Name = dbUser.Username });

        // Devuelve el token en la respuesta.
        return Ok(new { token = token });
    }
}