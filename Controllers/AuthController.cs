using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using preAceleracionDisney.Entities;
using preAceleracionDisney.Interfaces;
using preAceleracionDisney.ViewModels.Auth.Login;
using preAceleracionDisney.ViewModels.Auth.Register;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace preAceleracionDisney.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMailService _mailService;
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mailService = mailService;
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin(RegisterRequestViewModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);

            if (userExists != null)
            {
                return BadRequest(new
                {
                    Status = "Error",
                    Message = $"User creation failed for username {model.Username}"
                });
            }

            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                IsActive = true
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = "Error",
                    Message = $"User Creation Failed for Username {model.Username}! Errors:{string.Join(", ", result.Errors.Select(x => x.Description))}"
                });

            }

            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new IdentityRole("User"));

            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            await _userManager.AddToRoleAsync(user, "Admin");
            return Ok(new
            {
                Status = "Ok",
                Message = $"User creation was sucessful!"
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequestViewModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);

            if (userExists != null)
            {
                return BadRequest(new
                {
                        Status = "Error",
                        Message = $"User creation failed for username {model.Username}"
                });
            }

            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                IsActive = true
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = "Error",
                    Message = $"User Creation Failed for Username {model.Username}! Errors:{string.Join(", ", result.Errors.Select(x => x.Description))}"
                });

            }
            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new IdentityRole("User"));

            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            await _userManager.AddToRoleAsync(user, "User");

            await _mailService.SendEmail(user);
            return Ok(new
                {
                    Status = "Ok",
                    Message = $"User creation was sucessful!"
                });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequestViewModel model)
        {

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var currentUser = await _userManager.FindByNameAsync(model.Username);

                if (currentUser.IsActive)
                {
                    //Generar el token con un metodo
                    return Ok(await GetToken(currentUser));

                }
            }
            return BadRequest(new
            {
                Status = "Error",
                Message = $"Login failed for user with username {model.Username}"
            });
        }

        //GetToke: Metodo que genera un token al loguear
        private async Task<LoginResponseViewModel> GetToken(User currentUser)
        {
            var userRoles = await _userManager.GetRolesAsync(currentUser);

            var authClaims = new List<Claim>()
            {
                new Claim (ClaimTypes.Name,currentUser.UserName),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            
            authClaims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

            //levantamos nuestro signin key
            var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeySecretaSuperLargaDeAutorizacion"));

            //creo el token
            var token = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256));

            return new LoginResponseViewModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };
        }
    }
}
