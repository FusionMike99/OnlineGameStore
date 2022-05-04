﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.DAL.Configurations
{
    internal class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(g => g.Key)
                .IsRequired();

            builder.Property(g => g.Name)
                .IsRequired();

            builder.Property(g => g.Price)
                .HasColumnType("money")
                .IsRequired();

            builder.Property(g => g.UnitsInStock)
                .IsRequired();

            builder.Property(g => g.Discontinued)
                .IsRequired();

            builder.Property(g => g.IsDeleted)
                .HasDefaultValue(false);

            builder.HasIndex(g => g.Key).IsUnique();

            builder.HasIndex(g => g.Name).IsUnique();
        }
    }
}
