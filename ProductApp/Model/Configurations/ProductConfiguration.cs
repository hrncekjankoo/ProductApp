using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProductApp.Model.Configurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.ProductId);
        builder.Property(p => p.Name).HasMaxLength(100);
       
        builder.HasOne(p => p.Provider)
            .WithMany()
            .HasForeignKey(p => p.ProviderId);
    }
}
