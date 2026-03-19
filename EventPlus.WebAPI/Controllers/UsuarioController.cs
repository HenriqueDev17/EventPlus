using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;// Atributo privado para armazenar a instância do repositório de usuários

    public UsuarioController(IUsuarioRepository usuarioRepository)// Método construtor que recebe uma instância do repositório de usuários por meio de injeção de dependência
    {
        _usuarioRepository = usuarioRepository;
    }

    /// <summary>
    /// Endpoint da API que faz chamada para o método de buscar um usuário pelo seu ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>


    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_usuarioRepository.BuscarPorId(id));
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz chamada para o método de cadastrar um novo usuário
    /// </summary>
    /// <param name="usuario">Usuário a ser cadastrado</param>
    /// <returns>Status code 201 e o usuário cadastrado</returns>

    [HttpPost]
    public IActionResult Cadastrar(UsuarioDTO usuario)
    {
        try
        {

            var novoUsuario = new Usuario
            {
                Nome = usuario.Nome!,
                Senha = usuario.Senha!,
                Email = usuario.Email!,
                IdTipoUsuario = usuario.IdTipoUsuario
            };


            _usuarioRepository.Cadastrar(novoUsuario);

            return StatusCode(201, novoUsuario);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}
