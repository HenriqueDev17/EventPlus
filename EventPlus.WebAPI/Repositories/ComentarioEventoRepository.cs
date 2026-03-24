using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class ComentarioEventoRepository : IComentarioEventoRepository
{
    private readonly EventContext _context;
    public ComentarioEventoRepository(EventContext context)
    {
        _context = context;
    }

    public ComentarioEvento BuscarPorIdUsuario(Guid IdUsuario, Guid IdEvento)
    {
        return _context.ComentarioEventos
            .FirstOrDefault(c => c.IdUsuario == IdUsuario && c.IdEvento == IdEvento)!;
    }

    public void Cadastrar(ComentarioEvento comentarioEvento)
    {
        _context.ComentarioEventos.Add(comentarioEvento);
        _context.SaveChanges();

    }

    public void Deletar(Guid id)
    {
        var comentarioEventoBuscado = _context.Eventos.Find(id);
        if (comentarioEventoBuscado != null)
        {
            _context.Eventos.Remove(comentarioEventoBuscado);
            _context.SaveChanges();

        }
    }

    public List<ComentarioEvento> Listar(Guid IdEvento)
    {
        return _context.ComentarioEventos
   .Include(e => e.IdEventoNavigation)
   .Include(e => e.IdUsuarioNavigation)
   .ToList();
    }

    public List<ComentarioEvento> ListarSomenteExibe(Guid IdEvento)
    {
        return _context.ComentarioEventos
            .Where(c => c.IdEvento == IdEvento && c.Exibe == true)
            .Include(e => e.IdUsuarioNavigation)
            .ToList();
    }
}
 