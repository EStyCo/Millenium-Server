﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.EntityFramework;


#nullable disable

namespace Server.Migrations
{
    [DbContext(typeof(DbUserContext))]
    [Migration("20240713150304_ChangeItemEF")]
    partial class ChangeItemEF
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("Server.Models.EntityFramework.Character", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FreelSpellPoints")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Race")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Spells")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TotalSpellPoints")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("Server.Models.EntityFramework.ItemEF", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CharacterId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEquipped")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Server.Models.EntityFramework.Stats", b =>
                {
                    b.Property<int>("CharacterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Agility")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrentExp")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FreePoints")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Intelligence")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Strength")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalPoints")
                        .HasColumnType("INTEGER");

                    b.HasKey("CharacterId");

                    b.ToTable("Stats");
                });

            modelBuilder.Entity("Server.Models.EntityFramework.User", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CharacterId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Server.Models.EntityFramework.Character", b =>
                {
                    b.HasOne("Server.Models.EntityFramework.Stats", "Stats")
                        .WithOne("Character")
                        .HasForeignKey("Server.Models.EntityFramework.Character", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stats");
                });

            modelBuilder.Entity("Server.Models.EntityFramework.ItemEF", b =>
                {
                    b.HasOne("Server.Models.EntityFramework.Character", "Character")
                        .WithMany("Items")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("Server.Models.EntityFramework.User", b =>
                {
                    b.HasOne("Server.Models.EntityFramework.Character", "Character")
                        .WithOne("User")
                        .HasForeignKey("Server.Models.EntityFramework.User", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("Server.Models.EntityFramework.Character", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("Server.Models.EntityFramework.Stats", b =>
                {
                    b.Navigation("Character")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
