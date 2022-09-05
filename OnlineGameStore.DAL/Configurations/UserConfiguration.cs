﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.DAL.Entities;

namespace OnlineGameStore.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasOne(u => u.Publisher)
                .WithMany(p => p.Users)
                .HasForeignKey(u => u.PublisherId);
        }
    }
}