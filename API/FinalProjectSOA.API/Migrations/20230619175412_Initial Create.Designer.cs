﻿// <auto-generated />
using System;
using FinalProjectSOA.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinalProjectSOA.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230619175412_Initial Create")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FinalProjectSOA.API.Models.Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FeaturedImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UrlHandle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Visible")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
