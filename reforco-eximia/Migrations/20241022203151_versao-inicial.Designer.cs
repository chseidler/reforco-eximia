﻿// <auto-generated />
using System;
using ReforcoEximia.HttpService.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ReforcoEximia.HttpService.Migrations
{
    [DbContext(typeof(PropostasDbContext))]
    [Migration("20241022203151_versao-inicial")]
    partial class versaoinicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ReforcoEximia.HttpService.Dominio.Inscricoes.Aluno", b =>
                {
                    b.Property<string>("Cpf")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<int>("Idade")
                        .HasColumnType("int");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Cpf");

                    b.ToTable("Alunos", (string)null);
                });

            modelBuilder.Entity("ReforcoEximia.HttpService.Dominio.Inscricoes.Inscricao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AlunoCpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<bool>("Ativa")
                        .HasColumnType("bit");

                    b.Property<string>("Responsavel")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TurmaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Inscricoes", (string)null);
                });

            modelBuilder.Entity("ReforcoEximia.HttpService.Dominio.Inscricoes.Turma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Feminino")
                        .HasColumnType("bit");

                    b.Property<int>("LimiteIdade")
                        .HasColumnType("int");

                    b.Property<bool>("Masculino")
                        .HasColumnType("bit");

                    b.Property<int>("Vagas")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Turmas", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}