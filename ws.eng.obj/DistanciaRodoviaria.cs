using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ws.eng.obj
{
    public class DistanciaRodoviariaObj
    {
        public int ID { get; set; }
        public int LogradouroID { get; set; }
        public decimal ValorPercentual { get; set; }
        public decimal? Distancia { get; set; }
    }
}
