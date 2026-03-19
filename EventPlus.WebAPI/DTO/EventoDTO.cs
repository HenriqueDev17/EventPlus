namespace EventPlus.WebAPI.DTO;

public class EventoDTO
{
    public string Nome { get; set; } = null!;

    public DateTime DataEvento { get; set; }

    public string Descricao { get; set; } = null!;

    public Guid? IdTipoEvento { get; set; }

    public Guid? IdInstituicao { get; set; }
}
