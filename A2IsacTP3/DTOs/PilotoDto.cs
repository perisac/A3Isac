using System.ComponentModel.DataAnnotations;

namespace A2IsacTP3.DTOs
{
    public class PilotoDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(14, ErrorMessage = "O CPF deve ter no máximo 14 caracteres.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O tipo de licença é obrigatório.")]
        [StringLength(50, ErrorMessage = "O tipo de licença deve ter no máximo 50 caracteres.")]
        public string TipoLicenca { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O tempo de experiência deve ser um valor positivo.")]
        public int TempoExperiencia { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "As horas de voo devem ser um valor positivo.")]
        public double HorasVoo { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "A última avaliação médica é obrigatória.")]
        public DateTime UltimaAvaliacaoMedica { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "A data do último treinamento é obrigatória.")]
        public DateTime UltimoTreinamento { get; set; }
    }
}
