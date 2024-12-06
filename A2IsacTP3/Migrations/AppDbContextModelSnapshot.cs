﻿// <auto-generated />
using System;
using A2IsacTP3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace A2IsacTP3.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("A2IsacTP3.Models.Aviao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnoFabricacao")
                        .HasColumnType("int");

                    b.Property<double>("Autonomia")
                        .HasColumnType("float");

                    b.Property<int>("Capacidade")
                        .HasColumnType("int");

                    b.Property<double>("HorasVoo")
                        .HasColumnType("float");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TripulacaoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UltimaManutencao")
                        .HasColumnType("datetime2");

                    b.Property<double>("VelocidadeMedia")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("TripulacaoId");

                    b.ToTable("Avioes");
                });

            modelBuilder.Entity("A2IsacTP3.Models.Comissario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnosExperiencia")
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UltimoTreinamento")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Comissarios");
                });

            modelBuilder.Entity("A2IsacTP3.Models.Piloto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<double>("HorasVoo")
                        .HasColumnType("float");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TempoExperiencia")
                        .HasColumnType("int");

                    b.Property<string>("TipoLicenca")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UltimaAvaliacaoMedica")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UltimoTreinamento")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Pilotos");
                });

            modelBuilder.Entity("A2IsacTP3.Models.Rota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AviaoId")
                        .HasColumnType("int");

                    b.Property<string>("Destino")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("Distancia")
                        .HasColumnType("float");

                    b.Property<string>("Origem")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<TimeSpan>("TempoEstimado")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("AviaoId");

                    b.ToTable("Rotas");
                });

            modelBuilder.Entity("A2IsacTP3.Models.Tripulacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CoPilotoId")
                        .HasColumnType("int");

                    b.Property<string>("NomeTripulacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PilotoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CoPilotoId");

                    b.HasIndex("PilotoId");

                    b.ToTable("Tripulacoes");
                });

            modelBuilder.Entity("TripulacaoComissario", b =>
                {
                    b.Property<int>("ComissarioId")
                        .HasColumnType("int");

                    b.Property<int>("TripulacaoId")
                        .HasColumnType("int");

                    b.HasKey("ComissarioId", "TripulacaoId");

                    b.HasIndex("TripulacaoId");

                    b.ToTable("TripulacaoComissario");
                });

            modelBuilder.Entity("A2IsacTP3.Models.Aviao", b =>
                {
                    b.HasOne("A2IsacTP3.Models.Tripulacao", "Tripulacao")
                        .WithMany()
                        .HasForeignKey("TripulacaoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tripulacao");
                });

            modelBuilder.Entity("A2IsacTP3.Models.Rota", b =>
                {
                    b.HasOne("A2IsacTP3.Models.Aviao", "Aviao")
                        .WithMany("Rotas")
                        .HasForeignKey("AviaoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Aviao");
                });

            modelBuilder.Entity("A2IsacTP3.Models.Tripulacao", b =>
                {
                    b.HasOne("A2IsacTP3.Models.Piloto", "CoPiloto")
                        .WithMany()
                        .HasForeignKey("CoPilotoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("A2IsacTP3.Models.Piloto", "Piloto")
                        .WithMany()
                        .HasForeignKey("PilotoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CoPiloto");

                    b.Navigation("Piloto");
                });

            modelBuilder.Entity("TripulacaoComissario", b =>
                {
                    b.HasOne("A2IsacTP3.Models.Comissario", null)
                        .WithMany()
                        .HasForeignKey("ComissarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("A2IsacTP3.Models.Tripulacao", null)
                        .WithMany()
                        .HasForeignKey("TripulacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("A2IsacTP3.Models.Aviao", b =>
                {
                    b.Navigation("Rotas");
                });
#pragma warning restore 612, 618
        }
    }
}
