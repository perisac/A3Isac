using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A2IsacTP3.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comissarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    AnosExperiencia = table.Column<int>(type: "int", nullable: false),
                    UltimoTreinamento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comissarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pilotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    TipoLicenca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TempoExperiencia = table.Column<int>(type: "int", nullable: false),
                    HorasVoo = table.Column<double>(type: "float", nullable: false),
                    UltimaAvaliacaoMedica = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UltimoTreinamento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pilotos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tripulacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeTripulacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PilotoId = table.Column<int>(type: "int", nullable: false),
                    CoPilotoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tripulacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tripulacoes_Pilotos_CoPilotoId",
                        column: x => x.CoPilotoId,
                        principalTable: "Pilotos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tripulacoes_Pilotos_PilotoId",
                        column: x => x.PilotoId,
                        principalTable: "Pilotos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Avioes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modelo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Autonomia = table.Column<double>(type: "float", nullable: false),
                    AnoFabricacao = table.Column<int>(type: "int", nullable: false),
                    Capacidade = table.Column<int>(type: "int", nullable: false),
                    VelocidadeMedia = table.Column<double>(type: "float", nullable: false),
                    HorasVoo = table.Column<double>(type: "float", nullable: false),
                    UltimaManutencao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TripulacaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avioes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avioes_Tripulacoes_TripulacaoId",
                        column: x => x.TripulacaoId,
                        principalTable: "Tripulacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripulacaoComissario",
                columns: table => new
                {
                    ComissarioId = table.Column<int>(type: "int", nullable: false),
                    TripulacaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripulacaoComissario", x => new { x.ComissarioId, x.TripulacaoId });
                    table.ForeignKey(
                        name: "FK_TripulacaoComissario_Comissarios_ComissarioId",
                        column: x => x.ComissarioId,
                        principalTable: "Comissarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripulacaoComissario_Tripulacoes_TripulacaoId",
                        column: x => x.TripulacaoId,
                        principalTable: "Tripulacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rotas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Origem = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Destino = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Distancia = table.Column<double>(type: "float", nullable: false),
                    TempoEstimado = table.Column<TimeSpan>(type: "time", nullable: false),
                    AviaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rotas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rotas_Avioes_AviaoId",
                        column: x => x.AviaoId,
                        principalTable: "Avioes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avioes_TripulacaoId",
                table: "Avioes",
                column: "TripulacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Rotas_AviaoId",
                table: "Rotas",
                column: "AviaoId");

            migrationBuilder.CreateIndex(
                name: "IX_TripulacaoComissario_TripulacaoId",
                table: "TripulacaoComissario",
                column: "TripulacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tripulacoes_CoPilotoId",
                table: "Tripulacoes",
                column: "CoPilotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tripulacoes_PilotoId",
                table: "Tripulacoes",
                column: "PilotoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rotas");

            migrationBuilder.DropTable(
                name: "TripulacaoComissario");

            migrationBuilder.DropTable(
                name: "Avioes");

            migrationBuilder.DropTable(
                name: "Comissarios");

            migrationBuilder.DropTable(
                name: "Tripulacoes");

            migrationBuilder.DropTable(
                name: "Pilotos");
        }
    }
}
