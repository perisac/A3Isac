using A2IsacTP3.Models;

namespace A2IsacTP3.DTOs
{
    public class TripulacaoDto
    {

        public string NomeTripulacao { get; set; }
        public int PilotoId { get; set; }

        public int CoPilotoId { get; set; }

        public List<int> comissariosIds { get; set; }
    }
}
