using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazine.ModelsDto
{
    public class ProductDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SpecialCode { get; set; }
        public string Description { get; set; }
        public int? Count { get; set; } //Кількість або скільки грам
        public double? Massa { get; set; } //Кількість або скільки грам
        public bool? IsNumurable { get; set; } //1 - mass
        public double Price { get; set; } = 0; //Price by kg or 1
        public DateTime? CameToTheStorage { get; set; }
    }
}
