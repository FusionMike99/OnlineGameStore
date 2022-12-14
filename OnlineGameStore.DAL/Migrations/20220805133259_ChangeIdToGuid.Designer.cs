// <auto-generated />
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
    [Migration("20220805133259_ChangeIdToGuid")]
    partial class ChangeIdToGuid
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsQuoted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ReplyToId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("ReplyToId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DatePublished")
                        .HasColumnType("datetime2");

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

                    b.Property<string>("PublisherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuantityPerUnit")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<int>("ReorderLevel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<short>("UnitsInStock")
                        .HasColumnType("smallint");

                    b.Property<int>("UnitsOnOrder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<decimal>("ViewsNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)")
                        .HasDefaultValue(0m);

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("Key")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Games");

                    b.HasData(
                        new
                        {
                            Id = new Guid("94c979fa-20e5-412e-895b-a694b94f5ad4"),
                            DateAdded = new DateTime(2022, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DatePublished = new DateTime(2015, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "As war rages on throughout the Northern Realms, you take on the greatest contract of your life — tracking down the Child of Prophecy, a living weapon that can alter the shape of the world.",
                            Discontinued = false,
                            IsDeleted = false,
                            Key = "the-witcher-3",
                            Name = "The Witcher 3",
                            Price = 49.99m,
                            PublisherName = "CD Projekt RED",
                            QuantityPerUnit = "units",
                            ReorderLevel = 0,
                            UnitsInStock = (short)50,
                            UnitsOnOrder = 0,
                            ViewsNumber = 0m
                        });
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.GameGenre", b =>
                {
                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GenreId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GameId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("GameGenre");

                    b.HasData(
                        new
                        {
                            GameId = new Guid("94c979fa-20e5-412e-895b-a694b94f5ad4"),
                            GenreId = new Guid("e49f4755-02d6-444a-b25c-9e65c5298cc5")
                        },
                        new
                        {
                            GameId = new Guid("94c979fa-20e5-412e-895b-a694b94f5ad4"),
                            GenreId = new Guid("2d96d846-dd30-4982-95ea-1bf4aadf38f9")
                        });
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.GamePlatformType", b =>
                {
                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlatformId")
                        .HasColumnType("uniqueidentifier");
                    
                    b.HasKey("GameId", "PlatformId");

                    b.HasIndex("PlatformId");

                    b.ToTable("GamePlatformType");

                    b.HasData(
                        new
                        {
                            GameId = new Guid("94c979fa-20e5-412e-895b-a694b94f5ad4"),
                            PlatformId = new Guid("9f07b51a-f2cb-4c1b-ada4-b4ebb652ce0b")
                        },
                        new
                        {
                            GameId = new Guid("94c979fa-20e5-412e-895b-a694b94f5ad4"),
                            PlatformId = new Guid("8dac1629-29ce-4054-89f0-6d5bba95280f")
                        });
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ParentId");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8978f06e-a703-4746-bbe7-cc16a7e0249e"),
                            IsDeleted = false,
                            Name = "Strategy"
                        },
                        new
                        {
                            Id = new Guid("46cc32ce-db19-4c56-86da-19713ddf6976"),
                            IsDeleted = false,
                            Name = "RTS",
                            ParentId = new Guid("8978f06e-a703-4746-bbe7-cc16a7e0249e")
                        },
                        new
                        {
                            Id = new Guid("773ec3e3-67fa-4a45-8d3a-1ad616b8913f"),
                            IsDeleted = false,
                            Name = "TBS",
                            ParentId = new Guid("8978f06e-a703-4746-bbe7-cc16a7e0249e")
                        },
                        new
                        {
                            Id = new Guid("e49f4755-02d6-444a-b25c-9e65c5298cc5"),
                            IsDeleted = false,
                            Name = "RPG"
                        },
                        new
                        {
                            Id = new Guid("ce5b782d-d609-4bd5-a1c2-230861d63e05"),
                            IsDeleted = false,
                            Name = "Sports"
                        },
                        new
                        {
                            Id = new Guid("cb800c29-33e6-439e-b05e-657730c43213"),
                            IsDeleted = false,
                            Name = "Races"
                        },
                        new
                        {
                            Id = new Guid("45e4e6b3-dbf6-4bd7-9349-174aa48ab2a4"),
                            IsDeleted = false,
                            Name = "Rally",
                            ParentId = new Guid("cb800c29-33e6-439e-b05e-657730c43213")
                        },
                        new
                        {
                            Id = new Guid("94a5f76b-cc14-447d-b9fd-babd65d6f4d4"),
                            IsDeleted = false,
                            Name = "Arcade",
                            ParentId = new Guid("cb800c29-33e6-439e-b05e-657730c43213")
                        },
                        new
                        {
                            Id = new Guid("3b9515a5-cb4e-4667-8ff9-36ac005db45f"),
                            IsDeleted = false,
                            Name = "Formula",
                            ParentId = new Guid("cb800c29-33e6-439e-b05e-657730c43213")
                        },
                        new
                        {
                            Id = new Guid("d4937448-4b9d-4277-bc77-a6ed012f0403"),
                            IsDeleted = false,
                            Name = "Off-road",
                            ParentId = new Guid("cb800c29-33e6-439e-b05e-657730c43213")
                        },
                        new
                        {
                            Id = new Guid("ae4c8069-231a-4030-8202-907a2a548792"),
                            IsDeleted = false,
                            Name = "Action"
                        },
                        new
                        {
                            Id = new Guid("59800dd6-a511-47b3-9420-60376c6a813d"),
                            IsDeleted = false,
                            Name = "FPS",
                            ParentId = new Guid("ae4c8069-231a-4030-8202-907a2a548792")
                        },
                        new
                        {
                            Id = new Guid("cbab23a6-5122-4a72-995c-d3cd8d5ad6c3"),
                            IsDeleted = false,
                            Name = "TPS",
                            ParentId = new Guid("ae4c8069-231a-4030-8202-907a2a548792")
                        },
                        new
                        {
                            Id = new Guid("2d96d846-dd30-4982-95ea-1bf4aadf38f9"),
                            IsDeleted = false,
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = new Guid("8f9804ae-fca1-4117-b89e-db24f038f30e"),
                            IsDeleted = false,
                            Name = "Puzzle & Skill"
                        },
                        new
                        {
                            Id = new Guid("90649709-a022-415f-9e4b-45f4f9affc78"),
                            IsDeleted = false,
                            Name = "Misc."
                        },
                        new
                        {
                            Id = new Guid("d41414d6-ce60-435d-809a-4f4350990604"),
                            Description = "Soft drinks, coffees, teas, beers, and ales",
                            IsDeleted = false,
                            Name = "Beverages"
                        },
                        new
                        {
                            Id = new Guid("ad5d6708-6675-4504-8a90-e2cb60ff4a4c"),
                            Description = "Sweet and savory sauces, relishes, spreads, and seasonings",
                            IsDeleted = false,
                            Name = "Condiments"
                        },
                        new
                        {
                            Id = new Guid("f5b883d4-1d0b-4dac-9c9e-a9cf69c30162"),
                            Description = "Desserts, candies, and sweet breads",
                            IsDeleted = false,
                            Name = "Confections"
                        },
                        new
                        {
                            Id = new Guid("a6079e7a-6363-43d1-8004-8c99b5d9b1b9"),
                            Description = "Cheeses",
                            IsDeleted = false,
                            Name = "Dairy Products"
                        },
                        new
                        {
                            Id = new Guid("5c75e7f8-3ac2-4877-a5c5-6b0e3b02f70a"),
                            Description = "Breads, crackers, pasta, and cereal",
                            IsDeleted = false,
                            Name = "Grains/Cereals"
                        },
                        new
                        {
                            Id = new Guid("455ef484-ab3d-4bbd-bd8b-41e01fb5f0d6"),
                            Description = "Prepared meats",
                            IsDeleted = false,
                            Name = "Meat/Poultry"
                        },
                        new
                        {
                            Id = new Guid("6b0663e6-d189-4536-a950-42e53d419175"),
                            Description = "Dried fruit and bean curd",
                            IsDeleted = false,
                            Name = "Produce"
                        },
                        new
                        {
                            Id = new Guid("06ea7b74-a38d-42fe-8e7f-4edadb8cfa02"),
                            Description = "Seaweed and fish",
                            IsDeleted = false,
                            Name = "Seafood"
                        });
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CancelledDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Freight")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderState")
                        .HasColumnType("int");

                    b.Property<string>("ShipAddress")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<string>("ShipCity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<string>("ShipCountry")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<string>("ShipName")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<string>("ShipPostalCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<string>("ShipRegion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<string>("ShipVia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ShippedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.OrderDetail", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GameKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Discount")
                        .HasColumnType("real");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<short>("Quantity")
                        .HasColumnType("smallint");

                    b.HasKey("OrderId", "GameKey");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.PlatformType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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
                            Id = new Guid("0d879fbc-75d9-4df6-8ea2-2c0484207e20"),
                            IsDeleted = false,
                            Type = "Mobile"
                        },
                        new
                        {
                            Id = new Guid("46c32720-7400-4e51-82bb-96d35d4eff18"),
                            IsDeleted = false,
                            Type = "Browser"
                        },
                        new
                        {
                            Id = new Guid("9f07b51a-f2cb-4c1b-ada4-b4ebb652ce0b"),
                            IsDeleted = false,
                            Type = "Desktop"
                        },
                        new
                        {
                            Id = new Guid("8dac1629-29ce-4054-89f0-6d5bba95280f"),
                            IsDeleted = false,
                            Type = "Console"
                        });
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Publisher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<string>("City")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(40)")
                        .HasDefaultValue("");

                    b.Property<string>("ContactName")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<string>("ContactTitle")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<string>("Country")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.Property<string>("Fax")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<string>("HomePage")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Phone")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<string>("PostalCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<string>("Region")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.HasKey("Id");

                    b.HasIndex("CompanyName")
                        .IsUnique();

                    b.HasIndex("IsDeleted");

                    b.ToTable("Publishers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8b750162-60ab-4de9-a58d-f7ea9fbd0321"),
                            CompanyName = "CD Projekt RED",
                            Description = "Develop Witcher",
                            HomePage = "https://en.cdprojektred.com/",
                            IsDeleted = false
                        },
                        new
                        {
                            Id = new Guid("f02a54ae-b7b2-4904-9b67-c14f5c9622a7"),
                            CompanyName = "Bethesda Softworks",
                            Description = "Develop The Elder Scrolls",
                            HomePage = "https://bethesda.net/dashboard",
                            IsDeleted = false
                        },
                        new
                        {
                            Id = new Guid("eaad9f95-7976-4893-a1ed-17c1679f2728"),
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

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.OrderDetail", b =>
                {
                    b.HasOne("OnlineGameStore.BLL.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
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
                    b.Navigation("GameGenres");

                    b.Navigation("SubGenres");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("OnlineGameStore.BLL.Entities.PlatformType", b =>
                {
                    b.Navigation("GamePlatformTypes");
                });
#pragma warning restore 612, 618
        }
    }
}
