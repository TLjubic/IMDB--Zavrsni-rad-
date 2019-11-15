﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(KinoDb))]
    [Migration("20191113165303_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Model.Director", b =>
                {
                    b.Property<int>("DirectorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birth");

                    b.Property<string>("Description");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("DirectorId");

                    b.ToTable("Directors");
                });

            modelBuilder.Entity("Model.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");

                    b.HasData(
                        new { GenreId = 1, Name = "Crime" },
                        new { GenreId = 2, Name = "War" },
                        new { GenreId = 3, Name = "Comedy" },
                        new { GenreId = 4, Name = "Adventure" },
                        new { GenreId = 5, Name = "Family" },
                        new { GenreId = 6, Name = "Drama" },
                        new { GenreId = 7, Name = "Horror" },
                        new { GenreId = 8, Name = "Thriller" }
                    );
                });

            modelBuilder.Entity("Model.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int>("DirectorId");

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<int>("Runtime");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("MovieId");

                    b.HasIndex("DirectorId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Model.MovieGenre", b =>
                {
                    b.Property<int>("MovieId");

                    b.Property<int>("GenreId");

                    b.HasKey("MovieId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("MovieGenres");
                });

            modelBuilder.Entity("Model.MovieStar", b =>
                {
                    b.Property<int>("MovieId");

                    b.Property<int>("StarId");

                    b.HasKey("MovieId", "StarId");

                    b.HasIndex("StarId");

                    b.ToTable("MovieStars");
                });

            modelBuilder.Entity("Model.Star", b =>
                {
                    b.Property<int>("StarId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birth");

                    b.Property<string>("Description");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("StarId");

                    b.ToTable("Stars");
                });

            modelBuilder.Entity("Model.TVShow", b =>
                {
                    b.Property<int>("TVShowId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int>("NumberOfEpisode");

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("TVShowId");

                    b.ToTable("TVShows");
                });

            modelBuilder.Entity("Model.TVShowGenre", b =>
                {
                    b.Property<int>("TVShowId");

                    b.Property<int>("GenreId");

                    b.HasKey("TVShowId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("TVShowGenres");
                });

            modelBuilder.Entity("Model.TVShowStar", b =>
                {
                    b.Property<int>("TVShowId");

                    b.Property<int>("StarId");

                    b.HasKey("TVShowId", "StarId");

                    b.HasIndex("StarId");

                    b.ToTable("TVShowStars");
                });

            modelBuilder.Entity("Model.Movie", b =>
                {
                    b.HasOne("Model.Director", "Director")
                        .WithMany()
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.MovieGenre", b =>
                {
                    b.HasOne("Model.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Movie", "Movie")
                        .WithMany("ListOfGenres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.MovieStar", b =>
                {
                    b.HasOne("Model.Movie", "Movie")
                        .WithMany("ListOfStars")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Star", "Star")
                        .WithMany()
                        .HasForeignKey("StarId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.TVShowGenre", b =>
                {
                    b.HasOne("Model.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.TVShow", "TVShow")
                        .WithMany("ListOfGenres")
                        .HasForeignKey("TVShowId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.TVShowStar", b =>
                {
                    b.HasOne("Model.Star", "Star")
                        .WithMany()
                        .HasForeignKey("StarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.TVShow", "TVShow")
                        .WithMany("ListOfStars")
                        .HasForeignKey("TVShowId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
