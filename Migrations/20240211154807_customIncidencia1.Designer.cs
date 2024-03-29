﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using proyectoApi.Data;

#nullable disable

namespace proyectoApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240211154807_customIncidencia1")]
    partial class customIncidencia1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("proyectoApi.Models.Camara", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("CameraId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("CameraName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Kilometer")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Latitude")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Longitude")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("url")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Camaras");
                });

            modelBuilder.Entity("proyectoApi.Models.Incidencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AutonomousRegion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("CarRegistration")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Cause")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("CityTown")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("IncidenceLevel")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("endDate")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("incidenceDescription")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("incidenceID")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Incidencias");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("proyectoApi.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("proyectoApi.Models.UsuarioCamaraFavorite", b =>
                {
                    b.Property<int>("UsuarioId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("CamaraId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.HasKey("UsuarioId", "CamaraId");

                    b.HasIndex("CamaraId");

                    b.ToTable("UsuarioCamaraFavorites");
                });

            modelBuilder.Entity("proyectoApi.Models.UsuarioIncidenciaFavorite", b =>
                {
                    b.Property<int>("UsuarioId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("IncidenciaId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.HasKey("UsuarioId", "IncidenciaId");

                    b.HasIndex("IncidenciaId");

                    b.ToTable("UsuarioIncidenciaFavorites");
                });

            modelBuilder.Entity("proyectoApi.Models.customIncidencia", b =>
                {
                    b.HasBaseType("proyectoApi.Models.Incidencia");

                    b.ToTable("CustomIncidencias", (string)null);
                });

            modelBuilder.Entity("proyectoApi.Models.UsuarioCamaraFavorite", b =>
                {
                    b.HasOne("proyectoApi.Models.Camara", "Camara")
                        .WithMany("FavoriteByUsuarios")
                        .HasForeignKey("CamaraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("proyectoApi.Models.Usuario", "Usuario")
                        .WithMany("FavoriteCameras")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Camara");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("proyectoApi.Models.UsuarioIncidenciaFavorite", b =>
                {
                    b.HasOne("proyectoApi.Models.Incidencia", "Incidencia")
                        .WithMany("FavoriteByUsuarios")
                        .HasForeignKey("IncidenciaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("proyectoApi.Models.Usuario", "Usuario")
                        .WithMany("FavoriteIncidencias")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Incidencia");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("proyectoApi.Models.customIncidencia", b =>
                {
                    b.HasOne("proyectoApi.Models.Incidencia", null)
                        .WithOne()
                        .HasForeignKey("proyectoApi.Models.customIncidencia", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("proyectoApi.Models.Camara", b =>
                {
                    b.Navigation("FavoriteByUsuarios");
                });

            modelBuilder.Entity("proyectoApi.Models.Incidencia", b =>
                {
                    b.Navigation("FavoriteByUsuarios");
                });

            modelBuilder.Entity("proyectoApi.Models.Usuario", b =>
                {
                    b.Navigation("FavoriteCameras");

                    b.Navigation("FavoriteIncidencias");
                });
#pragma warning restore 612, 618
        }
    }
}
