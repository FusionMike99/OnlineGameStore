using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.DAL.Configurations
{
    internal class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(m => m.Id);
            
            builder.Property(m => m.Id)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Name)
                .IsRequired();

            builder.HasOne(g => g.Parent)
                .WithMany(g => g.SubGenres)
                .HasForeignKey(g => g.ParentId);

            builder.Property(g => g.IsDeleted)
                .HasDefaultValue(false);
            
            builder.Property(g => g.Description)
                .HasDefaultValue(string.Empty);

            builder.HasIndex(g => g.Name).IsUnique();
        }
    }
}