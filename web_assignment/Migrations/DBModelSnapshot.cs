﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using web_assignment.Models;

#nullable disable

namespace web_assignment.Migrations
{
    [DbContext(typeof(DB))]
    partial class DBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("web_assignment.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("id");

                    b.ToTable("Users");

                    b.HasDiscriminator().HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("web_assignment.Models.Admin", b =>
                {
                    b.HasBaseType("web_assignment.Models.User");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("web_assignment.Models.Cashier", b =>
                {
                    b.HasBaseType("web_assignment.Models.User");

                    b.HasDiscriminator().HasValue("Cashier");
                });

            modelBuilder.Entity("web_assignment.Models.Chef", b =>
                {
                    b.HasBaseType("web_assignment.Models.User");

                    b.HasDiscriminator().HasValue("Chef");
                });

            modelBuilder.Entity("web_assignment.Models.Manager", b =>
                {
                    b.HasBaseType("web_assignment.Models.User");

                    b.HasDiscriminator().HasValue("Manager");
                });

            modelBuilder.Entity("web_assignment.Models.Waiter", b =>
                {
                    b.HasBaseType("web_assignment.Models.User");

                    b.HasDiscriminator().HasValue("Waiter");
                });
#pragma warning restore 612, 618
        }
    }
}
