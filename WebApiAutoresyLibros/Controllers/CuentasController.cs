﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiAutoresyLibros.Dtoos;
using WebApiAutoresyLibros.Services;

namespace WebApiAutoresyLibros.Controllers
{
    [ApiController]
    [Route("api/cuentas")]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly HashService hashService;
        private readonly IDataProtector dataProtector;

        public CuentasController(UserManager<IdentityUser> userManager, IConfiguration configuration,
            SignInManager<IdentityUser> signInManager, IDataProtectionProvider dataProtectionProvider,
            HashService hashService)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.hashService = hashService;
            dataProtector = dataProtectionProvider.CreateProtector("valor_unico_y_quizas_secreto");
        }

        [HttpGet("hash/{textoPlano}")]
        public ActionResult RealizarHash(string textoPlano)
        {
            var resultado1 = hashService.Hash(textoPlano);

            return Ok(new
            {
                textoPlano = textoPlano,

                Hash1 = resultado1
            });
        }

        [HttpGet("encriptar")]
        public ActionResult Encriptar()
        {
            var textoPlano = "Isaí Vázquez";

            var textoCifrado = dataProtector.Protect(textoPlano);

            var textoDesencriptado = dataProtector.Unprotect(textoCifrado);

            return Ok(new
            {
                textoPlano = textoPlano,

                textoCifrado = textoCifrado,

                textoDesencriptado = textoDesencriptado
            });
        }

        [HttpGet("encriptarPorTiemoo")]
        public ActionResult EncriptarPorTiempo()
        {
            var protectorLimitadoPorTiempo = dataProtector.ToTimeLimitedDataProtector();

            var textoPlano = "Isaí Vázquez";

            var textoCifrado = protectorLimitadoPorTiempo.Protect(textoPlano, lifetime: TimeSpan.FromMinutes(30));

            var textoDesencriptado = dataProtector.Unprotect(textoCifrado);

            return Ok(new
            {
                textoPlano = textoPlano,

                textoCifrado = textoCifrado,

                textoDesencriptado = textoDesencriptado
            });
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<RespuestaAuth>> Registrar(CredencialesUsuario credencialesUsuario)
        {
            var usuario = new IdentityUser { UserName = credencialesUsuario.Email,
                Email = credencialesUsuario.Email};

            var resultado = await userManager.CreateAsync(usuario, credencialesUsuario.Password);

            if (resultado.Succeeded)
            {
                return await ConstruirToken(credencialesUsuario);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult<RespuestaAuth>> Login (CredencialesUsuario credencialesUsuario)
        {
            var resultado = await signInManager.PasswordSignInAsync(credencialesUsuario.Email,
                credencialesUsuario.Password, isPersistent: false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return await ConstruirToken(credencialesUsuario);
            }
            else
            {
                return BadRequest("Login incorrecto");
            }
        }

        [HttpGet("RenovarToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaAuth>> Renovar()
        {
            {
                var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();

                var email = emailClaim.Value;

                var credencialesUsuario = new CredencialesUsuario()
                {
                    Email = email
                };

                return await ConstruirToken(credencialesUsuario);
            }
        }

        private async Task<RespuestaAuth> ConstruirToken(CredencialesUsuario credencialesUsuario)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", credencialesUsuario.Email),

                new Claim("Lo que yo quiera", "cualquier otro valor")
            };

            var usuario = await userManager.FindByEmailAsync(credencialesUsuario.Email);

            var claimsDB = await userManager.GetClaimsAsync(usuario);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));

            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddMinutes(60);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiracion, signingCredentials: creds);

            return new RespuestaAuth()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiracion
            };
        }

        [HttpPost("HacerAdmin")]
        public async Task<ActionResult> HacerAdmin(EditarAdminDTO editarAdminDTO)
        {
            var usuario = await userManager.FindByEmailAsync(editarAdminDTO.Email);

            await userManager.AddClaimAsync(usuario, new Claim("esAdmin", "1"));

            return NoContent();
        }

        [HttpPost("RemoverAdmin")]
        public async Task<ActionResult> RemoverAdmin(EditarAdminDTO editarAdminDTO)
        {
            var usuario = await userManager.FindByEmailAsync(editarAdminDTO.Email);

            await userManager.RemoveClaimAsync(usuario, new Claim("esAdmin", "1"));

            return NoContent();
        }
    }
}
