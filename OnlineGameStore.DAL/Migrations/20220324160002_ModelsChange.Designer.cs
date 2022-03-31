﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    [Migration("20220324160002_ModelsChange")]
    partial class ModelsChange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ReplyToId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("ReplyToId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Key")
                        .IsUnique();

                    b.ToTable("Games");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.GameGenre", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("GameId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("GameGenre");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.GamePlatformType", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("PlatformId")
                        .HasColumnType("int");

                    b.HasKey("GameId", "PlatformId");

                    b.HasIndex("PlatformId");

                    b.ToTable("GamePlatformType");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ParentId");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Strategy"
                        },
                        new
                        {
                            Id = 2,
                            Name = "RTS",
                            ParentId = 1
                        },
                        new
                        {
                            Id = 3,
                            Name = "TBS",
                            ParentId = 1
                        },
                        new
                        {
                            Id = 4,
                            Name = "RPG"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Sports"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Races"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Rally",
                            ParentId = 6
                        },
                        new
                        {
                            Id = 8,
                            Name = "Arcade",
                            ParentId = 6
                        },
                        new
                        {
                            Id = 9,
                            Name = "Formula",
                            ParentId = 6
                        },
                        new
                        {
                            Id = 10,
                            Name = "Off-road",
                            ParentId = 6
                        },
                        new
                        {
                            Id = 11,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 12,
                            Name = "FPS",
                            ParentId = 11
                        },
                        new
                        {
                            Id = 13,
                            Name = "TPS",
                            ParentId = 11
                        },
                        new
                        {
                            Id = 14,
                            Name = "Misc.",
                            ParentId = 11
                        },
                        new
                        {
                            Id = 15,
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = 16,
                            Name = "Puzzle & Skill"
                        });
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.PlatformType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Type")
                        .IsUnique();

                    b.ToTable("PlatformTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Type = "Mobile"
                        },
                        new
                        {
                            Id = 2,
                            Type = "Browser"
                        },
                        new
                        {
                            Id = 3,
                            Type = "Desktop"
                        },
                        new
                        {
                            Id = 4,
                            Type = "Console"
                        });
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Comment", b =>
                {
                    b.HasOne("OnlineGameStore.BLL.Entities.Game", "Game")
                        .WithMany("Comments")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineGameStore.BLL.Entities.Comment", "ReplyTo")
                        .WithMany("Replies")
                        .HasForeignKey("ReplyToId");

                    b.Navigation("Game");

                    b.Navigation("ReplyTo");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.GameGenre", b =>
                {
                    b.HasOne("OnlineGameStore.BLL.Entities.Game", "Game")
                        .WithMany("GameGenres")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineGameStore.BLL.Entities.Genre", "Genre")
                        .WithMany("GameGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.GamePlatformType", b =>
                {
                    b.HasOne("OnlineGameStore.BLL.Entities.Game", "Game")
                        .WithMany("GamePlatformTypes")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineGameStore.BLL.Entities.PlatformType", "PlatformType")
                        .WithMany("GamePlatformTypes")
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("PlatformType");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Genre", b =>
                {
                    b.HasOne("OnlineGameStore.BLL.Entities.Genre", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Comment", b =>
                {
                    b.Navigation("Replies");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Game", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("GameGenres");

                    b.Navigation("GamePlatformTypes");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Genre", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("GameGenres");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.PlatformType", b =>
                {
                    b.Navigation("GamePlatformTypes");
                });
#pragma warning restore 612, 618
        }
    }
}