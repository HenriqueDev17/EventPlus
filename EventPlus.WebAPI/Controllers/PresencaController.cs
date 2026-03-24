using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PresencaController : ControllerBase
{
    private IPresencaRepository _presencaRepository;
    public PresencaController(IPresencaRepository presencaRepository)
    {
        _presencaRepository = presencaRepository;
    }
    /// <summary>
    /// Endpoint da API que retorna uma presença por id
    /// </summary>
    /// <param name="id">id da presença a ser buscada</param>
    /// <returns>Status code 200 e presença buscada</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_presencaRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que retorna uma lista de presenças filtrada por usuario
    /// </summary>
    /// <param name="idUsuario">id do usuario para filtragem</param>
    /// <returns>uma lista de presenca filtrada pelo usuario</returns>
    [HttpGet("ListarMinhas/{idUsuario}")]
    public IActionResult BuscarPorUsuario(Guid idUsuario)
    {
        try
        {
            return Ok(_presencaRepository.ListarMinhas(idUsuario));
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_presencaRepository.Listar());
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

    [HttpPost]
    public IActionResult Inscrever(PresencaDTO presenca)
    {
        try
        {
            var novoPresenca = new Presenca
            {
                Situacao = presenca.Situacao!,
                IdEvento = presenca.IdEvento,
                IdUsuario = presenca.IdUsuario,
            };
            _presencaRepository.Inscrever(novoPresenca);
            return StatusCode(201, novoPresenca);
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, PresencaDTO presenca)
    {
        try
        {
            var PresencaAtualizado = new Presenca
            {
                Situacao = presenca.Situacao!,
                IdUsuario = presenca.IdUsuario!,
                IdEvento = presenca.IdEvento!
            };
            _presencaRepository.Atualizar(id);
            return StatusCode(204, PresencaAtualizado);
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _presencaRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }
}
