namespace Identity.Controllers
{
    using Data.Wrapper;
    using IdentityModel;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using Models;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using ViewModels;

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UnitOfWorkAuth _db;
        private readonly IPasswordHashService _hasher;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IConfiguration config, UnitOfWorkAuth db, IPasswordHashService hasher, ILogger<AccountController> logger)
        {
            _config = config;
            _db = db;
            _hasher = hasher;
            _logger = logger;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Authenticate(RegisterViewModel model)
        {
            var user = await _db.UserRepository!.FindByNameAsync(model.UserName);
            
            if (user is null)
            {
                user = new User{
                    Id = Guid.NewGuid(),
                    Role = model.Role,
                    UserName = model.UserName,
                    PasswordHash = _hasher.HashPassword(model.Password),
                };
                
                await _db.UserRepository.Add(user);
                await _db.SaveChangesAsync();
                await GenerateToken(model.UserName, model.Password);
            }

            return BadRequest("User is already exist.");
        }
        

        [HttpPost]
        [Route("token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AuthenticationResponse>> GenerateToken(string username, string password)
        {
            var user = await _db.UserRepository!.FindByNameAsync(username);
            _logger.LogInformation($"Got name {user?.UserName}");
            
            
            if (user is not null && _hasher.Verify(password, user.PasswordHash))
            {
                var claims = new List<Claim>{
                    new Claim(JwtClaimTypes.Id, user.Id.ToString()),
                    new Claim(JwtClaimTypes.Name, user.UserName),
                    new Claim(JwtClaimTypes.Role, user.Role),
                };
            
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config["Jwt:SecretKey"]!);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Issuer = _config["Jwt:Issuer"],
                    Audience = _config["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials
                        (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                    Expires = DateTime.UtcNow.Add(TimeSpan.FromHours(1)),
                };
                
                var jwt = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(jwt);
                return Ok(new AuthenticationResponse{UserName = user.UserName, Token = token});
            }
            
            return NotFound("User was not found");
        }
    }
}
