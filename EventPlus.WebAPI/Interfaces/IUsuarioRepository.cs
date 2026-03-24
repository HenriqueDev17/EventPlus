using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IUsuarioRepository
{
    void Cadastrar(Usuario usuario);
    List<Usuario> Listar();
    Usuario BuscarPorId(Guid id);
    Usuario BuscarPorEmailESenha(string Email, string Senha);
    
}
