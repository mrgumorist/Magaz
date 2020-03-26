using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazine.ModelsDto
{
    public class InAndOutsDto
    {
        public List<SpisannyaOnAnotherMarket> SpisannyaOnAnotherMarket { get; set; }
        public List<CalcIn> Prihods { get; set; }
        public List<CalcOut> Rozhods { get; set; }
        public List<Spisannya> Spisannya { get; set; }
        public List<Lesia> LesiaS { get; set; }
        public List<Lena> LenaS { get; set; }

    }
}
