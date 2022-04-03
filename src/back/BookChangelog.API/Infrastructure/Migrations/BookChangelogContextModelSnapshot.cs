﻿// <auto-generated />
using System;
using BookChangelog.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookChangelog.API.Infrastructure.Migrations
{
    [DbContext(typeof(BookChangelogContext))]
    partial class BookChangelogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BookChangelog.API.Features.Authors.BookAuthor", b =>
                {
                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid")
                        .HasColumnName("author_id");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uuid")
                        .HasColumnName("book_id");

                    b.HasKey("AuthorId", "BookId")
                        .HasName("pk_book_author");

                    b.HasIndex("BookId")
                        .HasDatabaseName("ix_book_author_book_id");

                    b.ToTable("book_author", (string)null);
                });

            modelBuilder.Entity("BookChangelog.API.Models.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_author");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_author_name");

                    b.ToTable("author", (string)null);
                });

            modelBuilder.Entity("BookChangelog.API.Models.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<LocalDate>("PublicationDate")
                        .HasColumnType("date")
                        .HasColumnName("publication_date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_book");

                    b.ToTable("book", (string)null);
                });

            modelBuilder.Entity("BookChangelog.API.Models.BookChangeHistory", b =>
                {
                    b.Property<Guid>("BookId")
                        .HasColumnType("uuid")
                        .HasColumnName("book_id");

                    b.Property<int>("ChangeNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("change_number");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ChangeNumber"));

                    b.Property<string>("Change")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("change");

                    b.Property<DateTime>("ChangeDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("change_date_time");

                    b.HasKey("BookId", "ChangeNumber")
                        .HasName("pk_book_change_history");

                    b.ToTable("book_change_history", (string)null);
                });

            modelBuilder.Entity("BookChangelog.API.Features.Authors.BookAuthor", b =>
                {
                    b.HasOne("BookChangelog.API.Models.Author", "Author")
                        .WithMany("BookAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_book_author_authors_author_id");

                    b.HasOne("BookChangelog.API.Models.Book", "Book")
                        .WithMany("BookAuthors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_book_author_books_book_id");

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("BookChangelog.API.Models.Author", b =>
                {
                    b.Navigation("BookAuthors");
                });

            modelBuilder.Entity("BookChangelog.API.Models.Book", b =>
                {
                    b.Navigation("BookAuthors");
                });
#pragma warning restore 612, 618
        }
    }
}
