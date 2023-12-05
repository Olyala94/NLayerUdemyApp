using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;


namespace NLayer.Repository.Seeds
{
    internal class CatrgorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category { Id = 1, Name = "Kalemler" });
            builder.HasData(new Category { Id = 2, Name = "Kitaplar" });
            builder.HasData(new Category { Id = 3, Name = "Defterler" });
        }
    }
}
