using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class InstituicaoDTO
{
    [Required(ErrorMessage = "O NomeFantasia do tipo de evento é obrigatorio!")]
    public string? NomeFantasia { get; set; }
    public string? Cnpj { get; set; }
    public string? Endereco { get; set; }
}
