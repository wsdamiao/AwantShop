using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ws.eng.obj
{
    public class ServicoObj
    {
        public int ID { get; set; }
        public int IDServicoTipo { get; set; }
        public int IDPais { get; set; }
        public bool Ativo { get; set; }
        public bool PossuiCidade { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public ServicoTipoObj Tipo { get; set; }
    }

    public class ServicoTipoObj
    {
        public int ID { get; set; }                
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
