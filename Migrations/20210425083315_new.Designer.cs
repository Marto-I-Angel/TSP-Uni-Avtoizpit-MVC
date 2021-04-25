﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TSP_Uni_Listovki.Data;

namespace TSP_Uni_Listovki.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210425083315_new")]
    partial class @new
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Identity")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Listovki_TSP_Uni.Models.ListovkaModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("tochki")
                        .HasColumnType("int");

                    b.Property<string>("userName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("ListovkaModel");
                });

            modelBuilder.Entity("Listovki_TSP_Uni.Models.OtgovorModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VuprosID")
                        .HasColumnType("int");

                    b.Property<string>("izobrajenie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("veren")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.HasIndex("VuprosID");

                    b.ToTable("OtgovorModel");
                });

            modelBuilder.Entity("Listovki_TSP_Uni.Models.VuprosModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("tochki")
                        .HasColumnType("int");

                    b.Property<string>("uslovie")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("VuprosModel");
                });

            modelBuilder.Entity("Listovki_TSP_Uni.Models.VuprosiZaListovka", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ListovkaID")
                        .HasColumnType("int");

                    b.Property<int?>("VuprosId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("ListovkaID");

                    b.HasIndex("VuprosId");

                    b.ToTable("VuprosiZaListovka");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("TSP_Uni_Listovki.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Listovki_TSP_Uni.Models.OtgovorModel", b =>
                {
                    b.HasOne("Listovki_TSP_Uni.Models.VuprosModel", "Vupros")
                        .WithMany("Otgovori")
                        .HasForeignKey("VuprosID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vupros");
                });

            modelBuilder.Entity("Listovki_TSP_Uni.Models.VuprosiZaListovka", b =>
                {
                    b.HasOne("Listovki_TSP_Uni.Models.ListovkaModel", "Listovka")
                        .WithMany()
                        .HasForeignKey("ListovkaID");

                    b.HasOne("Listovki_TSP_Uni.Models.VuprosModel", "Vupros")
                        .WithMany()
                        .HasForeignKey("VuprosId");

                    b.Navigation("Listovka");

                    b.Navigation("Vupros");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TSP_Uni_Listovki.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Listovki_TSP_Uni.Models.VuprosModel", b =>
                {
                    b.Navigation("Otgovori");
                });
#pragma warning restore 612, 618
        }
    }
}
