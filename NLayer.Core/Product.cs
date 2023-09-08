using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }

        //Bir Tane Product'ın Bir Tane Category'si Olur......
        public int CategoryId { get; set;}  //bire çok ilişki //BuCategoryId - bu Product Entity'si için bir ForeginKey'dir
         
        public Category Category { get; set; } //Navigation Property'dir

        public ProductFeature ProductFeature { get; set; }  //Navigation Preperty'dir 
    }
}
