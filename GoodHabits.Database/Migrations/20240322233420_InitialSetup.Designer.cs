﻿// <auto-generated />
using GoodHabits.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GoodHabits.Database.Migrations
{
    [DbContext(typeof(GoodHabitsDbContext))]
    [Migration("20240322233420_InitialSetup")]
    partial class InitialSetup
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GoodHabits.Database.Entities.Habit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Habits");

                    b.HasData(
                        new
                        {
                            Id = 100,
                            Description = "Do some exercise",
                            Name = "Exercise"
                        },
                        new
                        {
                            Id = 101,
                            Description = "Read a book",
                            Name = "Read"
                        },
                        new
                        {
                            Id = 102,
                            Description = "Meditate for 10 minutes",
                            Name = "Meditate"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
