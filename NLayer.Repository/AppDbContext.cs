using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using NLayer.Repository.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {          
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            //Burada da Seed işlemlerini yazabiliriz - ama Best Practices Açısından ayrı klasör oluşturup orda kodları yazmak daha temiz olur
            modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature()
            {
                Id=1,
                Color="Kırmızı",
                Height= 100,
                Width= 200,
                ProductId=1,
            },
            new ProductFeature()
            {
                Id = 2,
                Color = "Mavi",
                Height = 300,
                Width = 500,
                ProductId = 2,
            });

            //Tek Tek de çagırabilirsin !! Yukarda yazdığımız kod ile hepisini getiriir...
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
                         
            //AppDbContext'imizin iiçin kirli durmasın diye "Configurations" diye klasöer oluşturup onun içinede "class" oluşturup -> Burada belirtecegimiz Atribut kodlarını oraya atadık!!!!  (Configurations klasörüne gidip incelliuyebilirsin)
            //modelBuilder.Entity<Category>().HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder); 
        }
    }
}
