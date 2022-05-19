using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.DAL.Configurations
{
    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(c => c.Name)
                .IsRequired();

            builder.Property(c => c.Body)
                .IsRequired();

            builder.Property(c => c.IsQuoted)
                .HasDefaultValue(false);
            
            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false);

            builder.HasOne(c => c.Game)
                .WithMany(g => g.Comments)
                .HasForeignKey(c => c.GameId);
        }
    }
}