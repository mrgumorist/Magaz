using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magazine.ModelsDto
{
    public class CreditDto
    {
        public int ID { get; set; }
        public double Sum { get; set; }
        public string Initsials { get; set; }
        public DateTime dateOfGetCredit { get; set; }
    }
}
