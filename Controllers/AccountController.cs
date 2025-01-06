using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using api.Dtos.Account;
using api.Interfaces;
using api.Models;

using Serilog;

namespace api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController(UserManager<StockUser> userManager, ITokenService tokenService, SignInManager<StockUser> signInManager) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var stockUser = new StockUser
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.EmailAddress
                };

                var createdUser = await userManager.CreateAsync(stockUser, registerDto.Password!);

                if (createdUser.Succeeded)
                {
                    var roleResult = await userManager.AddToRoleAsync(stockUser, "User");

                    if (roleResult.Succeeded)
                    {
                        return Ok(new NewUserDto
                        {
                            UserName = stockUser.UserName!,
                            Email = stockUser.Email!,
                            Token = tokenService.CreateToken(stockUser)
                        });
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Unexpected error occurred.");
                return StatusCode(500, "Unexpected error occurred.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.Users.FirstOrDefaultAsync(user => user.UserName != null && user.UserName.ToLower() == loginDto.UserName.ToLower());

            if (user == null)
            {
                return Unauthorized("Invalid user.");
            }

            var result = await signInManager.CheckPasswordSignInAsync(user: user, password: loginDto.Password, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return Unauthorized("Username not found and/or password might incorrect.");
            }

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName!,
                    Email = user.Email!,
                    Token = tokenService.CreateToken(user)
                }
                );
        }
    }
}