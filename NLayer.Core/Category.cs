using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core
{
    //Entityler içersindeki böyle farklı class'lara farklı Entit'lere referance verdiğimiz propertilere biz --> "Navigation Property" diyoruz.(Neden Navigation? - Çünkü : Category'den --> Product'lara gidebiliyorum!!!! (yani Category'ye bağlı tüm Product'ları çekebilirim....))
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        //Categoryde birden fazla Product olabilir....
        public ICollection<Product> Products { get; set; }
    }
}
