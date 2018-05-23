using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ws.eng.obj
{
    public class ElementoObj
    {
        public int ID { get; set; }
        public string Item { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
    }

    public class ElementoValorObj
    {
        public int ID { get; set; }
        public int ElementoID { get; set; }
        public int PaisID { get; set; }
        public decimal Valor { get; set; }
        public DateTime? Vigenciafim { get; set; }        
    }
}
