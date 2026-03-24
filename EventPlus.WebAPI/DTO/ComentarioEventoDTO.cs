using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlus.WebAPI.DTO;

public class ComentarioEventoDTO
{
    public string Descricao { get; set; } = null!;
    public Guid IdUsuario { get; set; }
    public Guid IdEvento { get; set; }

    
}
