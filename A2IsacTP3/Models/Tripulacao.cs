namespace A2IsacTP3.Models
{
    public class Tripulacao
    {
        public int Id { get; set; }

        public string NomeTripulacao { get; set; }
        public int PilotoId { get; set; }

        public int CoPilotoId { get; set; }

        public virtual Piloto Piloto { get; set; }

        public virtual Piloto CoPiloto { get; set; }

        public virtual ICollection<Comissario> Comissarios { get; set; }
    }
}