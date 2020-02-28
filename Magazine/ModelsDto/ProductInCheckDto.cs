using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazine.ModelsDto
{
    public class ProductInCheckDto
    {
        public int ID { get; set; }
        public int? IDOfProduct { get; set; }
        public string Name { get; set; }
        public string SpecialCode { get; set; }
        public string Description { get; set; }
        public int? Count { get; set; } = 0;
        public double? Massa { get; set; } = 0;
        public bool? IsNumurable { get; set; }
        public double? Price { get; set; }
        public virtual CheckDto Check { get; set; }
    }
}
