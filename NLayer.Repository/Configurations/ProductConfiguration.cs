using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(200);

            //Parasal Değerimiz toplam 18 karakter olacak virgülden sonrada 2 karakter alacak
            //Örnek: ##################.##  (18,2)
            builder.Property(x=>x.Price).IsRequired().HasColumnType("decimal(18,2)");

            //Tablomuza isim verdik burada.
            builder.ToTable("Products");

            //Bir Categorinin birden çok Product'ı olabilir(Burada 1 e çok ilişki örnek kodu)
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);
        }
    }
}
