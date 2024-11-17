﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using pos_koperasi.Data;

#nullable disable

namespace pos_koperasi.Migrations
{
    [DbContext(typeof(RazorPagesBarangContext))]
    partial class RazorPagesBarangContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("pos_koperasi.Models.Barang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Deskripsi")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Harga")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<string>("NamaBarang")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Stock")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TerakhirUpdate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Barang");
                });

            modelBuilder.Entity("pos_koperasi.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("pos_koperasi.Models.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BarangId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CartId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BarangId");

                    b.HasIndex("CartId", "BarangId")
                        .IsUnique();

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("pos_koperasi.Models.CartItem", b =>
                {
                    b.HasOne("pos_koperasi.Models.Barang", "Barang")
                        .WithMany("CartItems")
                        .HasForeignKey("BarangId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pos_koperasi.Models.Cart", "Cart")
                        .WithMany()
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Barang");

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("pos_koperasi.Models.Barang", b =>
                {
                    b.Navigation("CartItems");
                });
#pragma warning restore 612, 618
        }
    }
}
