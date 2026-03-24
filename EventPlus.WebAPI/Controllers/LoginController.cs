using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public LoginController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpPost]
    public IActionResult Login(LoginDTO loginDto)
    {
        try
        {
            Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmailESenha(loginDto.Email!, loginDto.Senha!);

            if (usuarioBuscado == null)
            {
                return NotFound(new { mensagem = "Email ou senha inválidos." });
            }

            //1 = Definir as inforções que serão fornecidas no token - Payload
            var claims = new[]
            {

             new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email!),

             new Claim(JwtRegisteredClaimNames.Name, usuarioBuscado.Nome!),

             new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),

             new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuarioNavigation!.Titulo!)

         };

            //2 = Definir a chave de acesso ao token
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("EventPlus-chave-autenticacao-webapi-dev"));

            //3 = Definir as credenciais do token
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //4 = Gerar o token
            var token = new JwtSecurityToken(
                issuer: "api_EventPlus", //Emissor do token

                audience: "api_EventPlus", //Destinatário do token

                claims: claims, //Dados definidos nas claims

                expires: DateTime.Now.AddMinutes(30), //Tempo de expiração do token

                signingCredentials: creds //Credenciais do token
                );

            //5 = Retornar o token para o cliente
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });

        }
        catch (Exception)

        {
            throw;
        }
    }
}
