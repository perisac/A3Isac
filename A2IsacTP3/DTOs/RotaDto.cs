using System.ComponentModel.DataAnnotations;

namespace A2IsacTP3.DTOs
{
    public class RotaDto
    {
        [Required(ErrorMessage = "A origem é obrigatória.")]
        [StringLength(100, ErrorMessage = "A origem deve ter no máximo 100 caracteres.")]
        public string Origem { get; set; }

        [Required(ErrorMessage = "O destino é obrigatório.")]
        [StringLength(100, ErrorMessage = "O destino deve ter no máximo 100 caracteres.")]
        public string Destino { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "A distância deve ser um valor positivo.")]
        public double Distancia { get; set; }

        [Required(ErrorMessage = "O tempo estimado é obrigatório.")]
        public TimeSpan TempoEstimado { get; set; }

        [Required(ErrorMessage = "O ID do avião é obrigatório.")]
        public int AviaoId { get; set; }
    }
}
