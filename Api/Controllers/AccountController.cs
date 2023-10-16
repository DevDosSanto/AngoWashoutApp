using Api.DTOs.Account;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JWTServices _jwtServices;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(JWTServices jwtServices, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _jwtServices = jwtServices;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized("Email ou password inválido");
            if (user.EmailConfirmed == false) return Unauthorized("Por favor confirme o teu email");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return Unauthorized("Email ou Password inválido");
            return createApplicationUserDto(user);

        }
        [HttpPost("register")]
        
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (await checkEmailExistsAsync(model.Email))
            {
                return BadRequest($"Email já está em uso {model.Email } tente outro email.");

            }
            var userToAdd = new User
            {
                PrimeiroNome = model.PrimeiroNome,
                Email = model.Email.ToLower(),
                UserName = model.Email.ToLower()
,               SegundoNome = model.SegundoNome,
                EmailConfirmed = true,

            };
            var result = await _userManager.CreateAsync(userToAdd, model.Password);
            if(!result.Succeeded) return BadRequest(result.Errors);

            return Ok("Conta criada com sucesso. Faça Login");

        }
        #region Private Helper Methods
        private UserDto createApplicationUserDto(User user)
        {
            return new UserDto
            {
                PrimeiroNome = user.PrimeiroNome,
                SegundoNome = user.SegundoNome,
                JWT = _jwtServices.CreateJWT(user),
            };
        }

        private async Task<bool> checkEmailExistsAsync(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());

        }
        #endregion

    }
}
