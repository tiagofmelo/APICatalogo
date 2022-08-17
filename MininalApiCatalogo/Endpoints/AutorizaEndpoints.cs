using Domain.DTO;
using Domain.Services;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MininalApiCatalogo.Controllers
{
    public static class AutorizaEndpoints
    {
        public static void MapAtualizaEndpoints(this WebApplication app)
        {
            app.MapPost("/register", async (UsuarioDTO model, AppDbContext db,
                                            UserManager<IdentityUser> _userManager,
                                            SignInManager<IdentityUser> _signInManager,
                                            IConfiguration _configuration) =>
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    return Results.BadRequest(result.Errors);
                }

                await _signInManager.SignInAsync(user, false);

                return Results.Created("Token: ", Token.GerarToken(_configuration, model));
            });

            app.MapPost("/login", async ([FromBody] UsuarioDTO userInfo,
                                         SignInManager<IdentityUser> _signInManager,
                                         IConfiguration _configuration) =>
            {
                var result = await _signInManager.PasswordSignInAsync(userInfo.Email,
                userInfo.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return Results.Created("Token: ", Token.GerarToken(_configuration, userInfo));
                }
                else
                {
                    return Results.BadRequest("Login Inválido....");
                }
            });
        }
    }
}
