using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class InstituicaoRepository :
    IInstituicaoRepository
{
    private readonly EventContext _context;

    public InstituicaoRepository (EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="instituicao"></param>

    public void Atualizar(Guid id, Instituicao instituicao)
    {
        var instituicaoBuscado = _context.Instituicaos.Find(id);
        if(instituicaoBuscado != null)
        {
            instituicaoBuscado.NomeFantasia = String.IsNullOrWhiteSpace(instituicao.NomeFantasia) ? instituicao.NomeFantasia : instituicao.NomeFantasia;
            instituicaoBuscado.Cnpj = String.IsNullOrWhiteSpace(instituicao.Cnpj) ? instituicao.Cnpj : instituicao.Cnpj;
            instituicaoBuscado.Endereco = String.IsNullOrWhiteSpace(instituicao.Endereco) ? instituicao.Endereco : instituicao.Endereco;
            _context.SaveChanges();

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>

    public Instituicao BuscarPorId(Guid id)
    {
        return _context.Instituicaos.Find(id)!;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="instituicao"></param>


    public void Cadastrar(Instituicao instituicao)
    {
        _context.Instituicaos.Add(instituicao);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var instituicaoBuscado = _context.Instituicaos.Find(id);
        if (instituicaoBuscado != null)
        {
            _context.Instituicaos.Remove(instituicaoBuscado);
            _context.SaveChanges();

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    

    public List<Instituicao> Listar()
    {
        return _context.Instituicaos.OrderBy(instituicao => instituicao.NomeFantasia).ToList();
    }
}
