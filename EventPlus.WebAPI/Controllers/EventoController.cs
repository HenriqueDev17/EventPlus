using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventoController : ControllerBase
{
    private readonly IEventoRepository _eventoRepository;

    public EventoController(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de listar eventos filtrando pelo id do usuario
    /// </summary>
    /// <param name="IdUsuario">Id do usuario para a filtragem</param>
    /// 
    /// <returns>Status code 200 e uma lista de eventos</returns>
    [HttpGet("Usuario/{IdUsuario}")]
    public IActionResult ListarPorId(Guid IdUsuario)
    {
        try
        {
            return Ok(_eventoRepository.ListarPorId(IdUsuario));
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que faz a chamada para o método de listar os próximos eventos
    /// </summary>
    /// <returns></returns>
    [HttpGet("ListarProximos")]
    public IActionResult BuscarProximosEventos()
    {
        try
        {
            return Ok(_eventoRepository.ProximosEventos());
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

    [HttpPost]
    public IActionResult Cadastrar(EventoDTO evento)
    {
        try
        {
            var novoEvento = new Evento
            {
                Nome = evento.Nome!,
                DataEvento = evento.DataEvento!,
                Descricao = evento.Descricao!,
                IdTipoEvento = evento.IdTipoEvento,
                IdInstituicao = evento.IdInstituicao

            };
            _eventoRepository.Cadastrar(novoEvento);
            return StatusCode(201, novoEvento);
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="evento"></param>
    /// <returns></returns>
    [HttpPut("{id}")]

    public IActionResult Atualizar(Guid id, EventoDTO evento)
    {
        try
        {
            var eventoAtualizado = new Evento
            {
                Nome = evento.Nome!,
                DataEvento = evento.DataEvento!,
                Descricao = evento.Descricao!,
                IdTipoEvento = evento.IdTipoEvento!,
                IdInstituicao = evento.IdInstituicao!
            };

            _eventoRepository.Atualizar(id, eventoAtualizado);
            return StatusCode(204, eventoAtualizado);
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// 
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _eventoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

}
