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
    [Migration("20220512153458_SetGamePublisherIdNotRequired")]
    partial class SetGamePublisherIdNotRequired
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
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

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ReplyToId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("ReplyToId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Discontinued")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<int?>("PublisherId")
                        .HasColumnType("int");

                    b.Property<short>("UnitsInStock")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("Key")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("PublisherId");

                    b.ToTable("Games");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "As war rages on throughout the Northern Realms, you take on the greatest contract of your life — tracking down the Child of Prophecy, a living weapon that can alter the shape of the world.",
                            Discontinued = false,
                            IsDeleted = false,
                            Key = "the-witcher-3",
                            Name = "The Witcher 3",
                            Price = 49.99m,
                            PublisherId = 1,
                            UnitsInStock = (short)50
                        });
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

                    b.HasData(
                        new
                        {
                            GameId = 1,
                            GenreId = 11
                        },
                        new
                        {
                            GameId = 1,
                            GenreId = 14
                        });
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

                    b.HasData(
                        new
                        {
                            GameId = 1,
                            PlatformId = 3
                        },
                        new
                        {
                            GameId = 1,
                            PlatformId = 4
                        });
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ParentId");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            Name = "Strategy"
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            Name = "RTS",
                            ParentId = 1
                        },
                        new
                        {
                            Id = 3,
                            IsDeleted = false,
                            Name = "TBS",
                            ParentId = 1
                        },
                        new
                        {
                            Id = 4,
                            IsDeleted = false,
                            Name = "RPG"
                        },
                        new
                        {
                            Id = 5,
                            IsDeleted = false,
                            Name = "Sports"
                        },
                        new
                        {
                            Id = 6,
                            IsDeleted = false,
                            Name = "Races"
                        },
                        new
                        {
                            Id = 7,
                            IsDeleted = false,
                            Name = "Rally",
                            ParentId = 6
                        },
                        new
                        {
                            Id = 8,
                            IsDeleted = false,
                            Name = "Arcade",
                            ParentId = 6
                        },
                        new
                        {
                            Id = 9,
                            IsDeleted = false,
                            Name = "Formula",
                            ParentId = 6
                        },
                        new
                        {
                            Id = 10,
                            IsDeleted = false,
                            Name = "Off-road",
                            ParentId = 6
                        },
                        new
                        {
                            Id = 11,
                            IsDeleted = false,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 12,
                            IsDeleted = false,
                            Name = "FPS",
                            ParentId = 11
                        },
                        new
                        {
                            Id = 13,
                            IsDeleted = false,
                            Name = "TPS",
                            ParentId = 11
                        },
                        new
                        {
                            Id = 14,
                            IsDeleted = false,
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = 15,
                            IsDeleted = false,
                            Name = "Puzzle & Skill"
                        },
                        new
                        {
                            Id = 16,
                            IsDeleted = false,
                            Name = "Misc."
                        });
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("OrderStatusId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.OrderDetail", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<float>("Discount")
                        .HasColumnType("real");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<short>("Quantity")
                        .HasColumnType("smallint");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("OrderStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            Status = "Open"
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            Status = "In progress"
                        },
                        new
                        {
                            Id = 3,
                            IsDeleted = false,
                            Status = "Cancelled"
                        },
                        new
                        {
                            Id = 4,
                            IsDeleted = false,
                            Status = "Closed"
                        });
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.PlatformType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("Type")
                        .IsUnique();

                    b.ToTable("PlatformTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            Type = "Mobile"
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            Type = "Browser"
                        },
                        new
                        {
                            Id = 3,
                            IsDeleted = false,
                            Type = "Desktop"
                        },
                        new
                        {
                            Id = 4,
                            IsDeleted = false,
                            Type = "Console"
                        });
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.Property<string>("HomePage")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CompanyName")
                        .IsUnique();

                    b.HasIndex("IsDeleted");

                    b.ToTable("Publishers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CompanyName = "CD Projekt RED",
                            Description = "Develop Witcher",
                            HomePage = "https://en.cdprojektred.com/",
                            IsDeleted = false
                        },
                        new
                        {
                            Id = 2,
                            CompanyName = "Bethesda Softworks",
                            Description = "Develop The Elder Scrolls",
                            HomePage = "https://bethesda.net/dashboard",
                            IsDeleted = false
                        },
                        new
                        {
                            Id = 3,
                            CompanyName = "THQ Nordic",
                            Description = "Develop Star Wars",
                            HomePage = "https://www.thqnordic.com/",
                            IsDeleted = false
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

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Game", b =>
                {
                    b.HasOne("OnlineGameStore.BLL.Entities.Publisher", "Publisher")
                        .WithMany("Games")
                        .HasForeignKey("PublisherId");

                    b.Navigation("Publisher");
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
                        .WithMany("SubGenres")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Order", b =>
                {
                    b.HasOne("OnlineGameStore.BLL.Entities.OrderStatus", "OrderStatus")
                        .WithMany("Orders")
                        .HasForeignKey("OrderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderStatus");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.OrderDetail", b =>
                {
                    b.HasOne("OnlineGameStore.BLL.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineGameStore.BLL.Entities.Game", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
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

                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Genre", b =>
                {
                    b.Navigation("GameGenres");

                    b.Navigation("SubGenres");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.OrderStatus", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.PlatformType", b =>
                {
                    b.Navigation("GamePlatformTypes");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Publisher", b =>
                {
                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
