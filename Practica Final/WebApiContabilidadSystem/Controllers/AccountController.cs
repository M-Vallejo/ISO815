using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiContabilidadSystem.Data;
using WebApiContabilidadSystem.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using WebApiContabilidadSystem.Utils;
using WebApiContabilidadSystem.Models;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using System.Reflection;

namespace WebApiContabilidadSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly ContabilidadDbContext _db;
        private readonly AppSettings _appSettings;

        public AccountController(ContabilidadDbContext _dbContext, IOptions<AppSettings> appSetting)
        {
            _db = _dbContext;
            _appSettings = appSetting.Value;
        }

        private string GenerateJwtToken(USUARIO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.USUARIO_ID.ToString()),
                    new Claim("nombre_usuario", user.NOMBRE_USUARIO),
                    new Claim("nombre", user.NOMBRE),
                    new Claim("rol", (user.ROL).ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.ValidMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpPost]
        public ActionResult Login([FromBody] LoginViewModel model) 
        {
            const string invalidUsernameOrPasswordMessage = "Usuario y/o contraseña incorrectos";

            if (!model.IsValid())
                return Unauthorized(invalidUsernameOrPasswordMessage);

            string encryptedPassword = model.Password.Encrypt();
            var user = _db.Usuario.FirstOrDefault(x => 
                                                    x.NOMBRE_USUARIO == model.Username 
                                                    && x.CLAVE == encryptedPassword
                                                    && x.ESTADO == (int)Estado.Activo);

            if (user is null) return Unauthorized(invalidUsernameOrPasswordMessage);

            var token = new TokenViewModel { Token = this.GenerateJwtToken(user) };

            return Ok(token);
        }

        [HttpGet]
        [Authorize]
        public ActionResult IsAuthorized() 
        {
            return Ok((USUARIO)Request.HttpContext.Items["User"]);
        }


        [HttpGet]
        public ActionResult Index() 
        {
            var appVersion = typeof(Program).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
            return Ok(appVersion);
        }
    }
}
