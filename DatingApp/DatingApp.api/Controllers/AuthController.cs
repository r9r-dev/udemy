using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.api.Data;
using DatingApp.api.Dtos;
using DatingApp.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;

namespace DatingApp.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper)
        {
            _repo = repo;
            _config = config;
            _mapper = mapper;
        }

        /// <summary>
        /// Register a user
        /// </summary>
        /// <remarks>
        /// Anonymous request
        /// </remarks>
        /// <response code="201">Returns the newly created user</response>
        /// <response code="400">If username already exists</response>
        /// <response code="500">For any unhandled exception</response>
        [HttpPost("register")]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<UserForDetailedDto>> Register([FromBody] UserForRegisterDto userDto)
        {
            userDto.Username = userDto.Username.ToLower();
            if (await _repo.UserExists(userDto.Username)) return BadRequest("Username already exists");

            var userToCreate = _mapper.Map<User>(userDto);

            var createdUser = await _repo.Register(userToCreate, userDto.Password);

            var userToReturn = _mapper.Map<UserForDetailedDto>(createdUser);

            return CreatedAtRoute("GetUser", new {controller = "Users", id = createdUser.Id}, userToReturn);
        }

        /// <summary>
        /// Login a user
        /// </summary>
        /// <remarks>
        /// Anonymous request
        /// </remarks>
        /// <response code="200">Returns the Jwt Token and some user informations</response>
        /// <response code="500">For any unhandled exception</response>
        [HttpPost("login")]        
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] UserForLoginDto userDto)
        {
            var userFromRepo = await _repo.Login(userDto.Username.ToLower(), userDto.Password);
            if (userFromRepo == null) return Unauthorized();

            var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, userFromRepo.Username)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var user = _mapper.Map<UserForListDto>(userFromRepo);

            return Ok(new LoginResponse
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            });
        }
    }
}