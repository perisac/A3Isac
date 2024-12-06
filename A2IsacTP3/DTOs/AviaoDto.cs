using A2IsacTP3.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace A2IsacTP3.DTOs
{
    public class AviaoDto
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "O modelo do avião é obrigatório.")]
        [StringLength(100, ErrorMessage = "O modelo deve ter no máximo 100 caracteres.")]
        public string Modelo { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "A autonomia deve ser um valor positivo.")]
        public double Autonomia { get; set; }

        [Range(1900, int.MaxValue, ErrorMessage = "O ano de fabricação deve ser válido.")]
        public int AnoFabricacao { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A capacidade deve ser pelo menos 1.")]
        public int Capacidade { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "A velocidade média deve ser um valor positivo.")]
        public double VelocidadeMedia { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "As horas de voo devem ser um valor positivo.")]
        public double HorasVoo { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "A data da última manutenção é obrigatória.")]
        public DateTime UltimaManutencao { get; set; }

        public List<int>? RotasId { get; set; }

        // ID da tripulação associada ao avião
        public int? TripulacaoId { get; set; }
    }
}
