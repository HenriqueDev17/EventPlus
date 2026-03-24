using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class PresencaRepository : IPresencaRepository
{
    private readonly EventContext _eventContext;
    public PresencaRepository(EventContext context)
    {
        _eventContext = context; 
    }

    public void Atualizar(Guid IdPresencaEvento)
    {
        var presencaBuscado = _eventContext.Presencas.Find(IdPresencaEvento);
        if (presencaBuscado != null)
        {
            presencaBuscado.Situacao = !presencaBuscado.Situacao;

            //O SaveChanges() detecta a mudança na propriedade "Titulo" automaticamente

            _eventContext.SaveChanges();
        }
    }
    /// <summary>
    /// Busca uma presença por id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Presenca BuscarPorId(Guid id)
    {
        return _eventContext.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInstituicaoNavigation)
            .FirstOrDefault(p => p.IdPresenca == id)!;
    }

    public void Deletar(Guid id)
    {
        var presencaBuscado = _eventContext.Presencas .Find(id);
        if(presencaBuscado != null)
        {
            _eventContext.Presencas.Remove(presencaBuscado);
            _eventContext.SaveChanges();
        }
    }

    public void Inscrever(Presenca Inscricao)
    {
        _eventContext.Presencas.Add(Inscricao);
        _eventContext.SaveChanges();
    }

    public List<Presenca> Listar()
    {
        //return _eventContext.Presencas.Include(e => e.IdEventoNavigation)
        //    .Include(e => e!.IdUsuarioNavigation)
        //    .ToList();
        return _eventContext.Presencas.OrderBy(Presenca => Presenca.Situacao).ToList(); 
    }
    /// <summary>
    /// Lista as presenças de um usuario especifico
    /// </summary>
    /// <param name="IdUsuario"></param>
    /// <returns>uma lista de presencas de um usuario especifico</returns>
    public List<Presenca> ListarMinhas(Guid IdUsuario)
    {
        return _eventContext.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInstituicaoNavigation)
            .Where(p => p.IdUsuario == IdUsuario)
            .ToList();
    }
}
