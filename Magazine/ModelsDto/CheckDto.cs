using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazine.ModelsDto
{
    public class CheckDto
    {
        public int ID { get; set; }
        public ICollection<ProductInCheckDto> Products { get; set; }
        public double? SumPrice { get; set; } = 0;
        public DateTime? DateCreatingOfCheck { get; set; }
        public DateTime? DateCloseOfCheck { get; set; }
        public string TypeOfPay { get; set; }
    }
}
