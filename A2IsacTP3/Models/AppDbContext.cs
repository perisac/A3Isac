using Microsoft.EntityFrameworkCore;

namespace A2IsacTP3.Models
{
    public class AppDbContext :  DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }   

        public DbSet<Aviao> Avioes { get; set; }
        public DbSet<Comissario> Comissarios { get; set; }
        public DbSet<Piloto> Pilotos { get; set; }  
        public DbSet<Rota> Rotas { get; set; }
        public DbSet<Tripulacao> Tripulacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da relação muitos-para-muitos entre Comissario e Tripulacao
            modelBuilder.Entity<Tripulacao>()
                .HasMany(t => t.Comissarios)
                .WithMany(c => c.Tripulacoes) // Assuma que Comissario possui uma coleção Tripulacoes
                .UsingEntity<Dictionary<string, object>>(
                    "TripulacaoComissario", // Nome da tabela intermediária
                    t => t.HasOne<Comissario>().WithMany().HasForeignKey("ComissarioId"),
                    c => c.HasOne<Tripulacao>().WithMany().HasForeignKey("TripulacaoId"));

            // Configuração da relação um-para-um entre Tripulacao e Piloto
            modelBuilder.Entity<Tripulacao>()
                .HasOne(t => t.Piloto)
                .WithMany() // Piloto não tem referência reversa
                .HasForeignKey(t => t.PilotoId)
                .OnDelete(DeleteBehavior.Restrict); // Evita exclusão em cascata

            // Configuração da relação um-para-um entre Tripulacao e CoPiloto
            modelBuilder.Entity<Tripulacao>()
                .HasOne(t => t.CoPiloto)
                .WithMany() // CoPiloto não tem referência reversa
                .HasForeignKey(t => t.CoPilotoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuração da relação um-para-muitos entre Aviao e Rota
            modelBuilder.Entity<Aviao>()
                .HasMany(a => a.Rotas)
                .WithOne(r => r.Aviao)
                .HasForeignKey(r => r.AviaoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuração da relação obrigatória entre Aviao e Tripulacao
            modelBuilder.Entity<Aviao>()
                .HasOne(a => a.Tripulacao)
                .WithMany() // Tripulacao não tem referência reversa
                .HasForeignKey(a => a.TripulacaoId) // Assuma que existe uma FK TripulacaoId em Aviao
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
