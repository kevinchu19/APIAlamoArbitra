﻿using APIAlamoArbitra.Models.Response.Login;
using APIAlamoArbitra.Models.Usuario;
using APIAlamoArbitra.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIAlamoArbitra.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class LoginController: ControllerBase
    {
        private readonly ILogger<FacturacionController> Logger;

        public LoginController(ILogger<FacturacionController> logger, LoginRepository repository, IConfiguration configuration)
        {
            Logger = logger;
            Repository = repository;
            Configuration = configuration;
        }

        public LoginRepository Repository { get; }
        public IConfiguration Configuration { get; }

        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Post([FromBody] UsuarioDTO credentials)
        {
            var key = Configuration["key"];

            var errorMessage = await Repository.LoginWithJwt(credentials.usuario, credentials.password);

            if (errorMessage != null)
            {
                return Unauthorized(new LoginResponse() { mensaje = errorMessage });
            }

            


            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, credentials.usuario)
                 
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //tokenDescriptor.Subject.AddClaim(
            //        new Claim(ClaimTypes.Expiration, tokenDescriptor.Expires.ToString())
            //    ); 

            LoginResponse response = new LoginResponse();

            response.token = tokenHandler.WriteToken(token);
            response.expirationDate = tokenDescriptor.Expires;

            return Ok(response);




        }
    }
}
