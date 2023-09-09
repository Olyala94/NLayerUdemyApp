using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core
{
    //abstract --> Bu BaseEntity'den bir Nesne örneginde alınmasın. (new classismi = ClasIsmi();  diye kullanıyoruzya işte "abstract" yapınca öyle kullanamazsın bu "BaseEntity'ni".......)
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
