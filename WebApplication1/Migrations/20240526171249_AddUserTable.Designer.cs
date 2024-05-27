﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NeonTechAspNetCore.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240526171249_AddUserTable")]
    partial class AddUserTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("NeonTechAspNetCore.Models.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NeonTechAspNetCore.Models.Categoria", b =>
                {
                    b.Property<int>("IdCategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdCategoria"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("IdCategoria");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("NeonTechAspNetCore.Models.Compra", b =>
                {
                    b.Property<int>("IdCompra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdCompra"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("IdProveedor")
                        .HasColumnType("integer");

                    b.HasKey("IdCompra");

                    b.HasIndex("IdProveedor");

                    b.ToTable("Compras");
                });

            modelBuilder.Entity("NeonTechAspNetCore.Models.DetalleCompra", b =>
                {
                    b.Property<int>("IdDetalleCompra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdDetalleCompra"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("integer");

                    b.Property<int>("IdCompra")
                        .HasColumnType("integer");

                    b.Property<int>("IdProducto")
                        .HasColumnType("integer");

                    b.Property<decimal>("PrecioCompra")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdDetalleCompra");

                    b.HasIndex("IdCompra");

                    b.HasIndex("IdProducto");

                    b.ToTable("DetallesCompra");
                });

            modelBuilder.Entity("NeonTechAspNetCore.Models.Lote", b =>
                {
                    b.Property<int>("IdLote")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdLote"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("IdProducto")
                        .HasColumnType("integer");

                    b.HasKey("IdLote");

                    b.HasIndex("IdProducto");

                    b.ToTable("Lotes");
                });

            modelBuilder.Entity("NeonTechAspNetCore.Models.Producto", b =>
                {
                    b.Property<int>("IdProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdProducto"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("IdCategoria")
                        .HasColumnType("integer");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<decimal>("PrecioCompra")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PrecioVenta")
                        .HasColumnType("numeric");

                    b.HasKey("IdProducto");

                    b.HasIndex("IdCategoria");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("NeonTechAspNetCore.Models.Proveedor", b =>
                {
                    b.Property<int>("IdProveedor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdProveedor"));

                    b.Property<string>("Contacto")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdProveedor");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("NeonTechAspNetCore.Models.Compra", b =>
                {
                    b.HasOne("NeonTechAspNetCore.Models.Proveedor", "Proveedor")
                        .WithMany()
                        .HasForeignKey("IdProveedor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("NeonTechAspNetCore.Models.DetalleCompra", b =>
                {
                    b.HasOne("NeonTechAspNetCore.Models.Compra", "Compra")
                        .WithMany("DetallesCompra")
                        .HasForeignKey("IdCompra")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NeonTechAspNetCore.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("IdProducto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compra");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("NeonTechAspNetCore.Models.Lote", b =>
                {
                    b.HasOne("NeonTechAspNetCore.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("IdProducto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("NeonTechAspNetCore.Models.Producto", b =>
                {
                    b.HasOne("NeonTechAspNetCore.Models.Categoria", "Categoria")
                        .WithMany("Productos")
                        .HasForeignKey("IdCategoria")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("NeonTechAspNetCore.Models.Categoria", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("NeonTechAspNetCore.Models.Compra", b =>
                {
                    b.Navigation("DetallesCompra");
                });
#pragma warning restore 612, 618
        }
    }
}
