﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PublicAddressBook.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Entities.Models.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("ContactId");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name", "Address")
                        .IsUnique();

                    b.ToTable("Contacts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6aef342c-b2a0-410e-b8e2-d41df80fb360"),
                            Address = "Tina Ujevica 3, Krizevci",
                            DateOfBirth = new DateTime(1997, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Ivan Srbljinovic"
                        },
                        new
                        {
                            Id = new Guid("c536dde5-4e5b-440c-9801-74d4a8fc7440"),
                            Address = "Trg bana Jelacica 1, Zagreb",
                            DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "John Doe"
                        },
                        new
                        {
                            Id = new Guid("cd03283d-bc37-46fe-a974-915860680b5d"),
                            Address = "Trg bana Jelacica 1, Zagreb",
                            DateOfBirth = new DateTime(1991, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Jane Doe"
                        });
                });

            modelBuilder.Entity("Entities.Models.TelephoneNumber", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("TelephoneNumberId");

                    b.Property<Guid>("ContactId")
                        .HasColumnType("uuid");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.ToTable("TelephoneNumbers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1296ee0a-d753-4cb4-924b-25b32ed86506"),
                            ContactId = new Guid("6aef342c-b2a0-410e-b8e2-d41df80fb360"),
                            Number = "+3850901234567"
                        },
                        new
                        {
                            Id = new Guid("99713f98-b250-4706-93b8-4fdaeb10e082"),
                            ContactId = new Guid("6aef342c-b2a0-410e-b8e2-d41df80fb360"),
                            Number = "+3850907654321"
                        },
                        new
                        {
                            Id = new Guid("709d116e-8341-410f-8078-19562ecdfb3d"),
                            ContactId = new Guid("cd03283d-bc37-46fe-a974-915860680b5d"),
                            Number = "+3850912345678"
                        });
                });

            modelBuilder.Entity("Entities.Models.TelephoneNumber", b =>
                {
                    b.HasOne("Entities.Models.Contact", "Contact")
                        .WithMany("TelephoneNumbers")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("Entities.Models.Contact", b =>
                {
                    b.Navigation("TelephoneNumbers");
                });
#pragma warning restore 612, 618
        }
    }
}
