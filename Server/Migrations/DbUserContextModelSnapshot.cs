﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Models;

#nullable disable

namespace Server.Migrations
{
    [DbContext(typeof(DbUserContext))]
    partial class DbUserContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("Server.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Agility")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CharacterName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("CurrentArea")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrentEXP")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FreePoints")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FreelSpellPoints")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Intelligence")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Race")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Skill1")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Skill2")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Skill3")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Skill4")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Skill5")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Strength")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ToLevelExp")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalPoints")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalSpellPoints")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("Server.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
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

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Server.Models.User", b =>
                {
                    b.HasOne("Server.Models.Character", "Character")
                        .WithOne("User")
                        .HasForeignKey("Server.Models.User", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("Server.Models.Character", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
