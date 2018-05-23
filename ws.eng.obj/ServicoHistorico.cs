using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ws.eng.obj
{
    public class ServicoHistoricoObj
    {
        public long ID { get; set; }
        public long IdServico { get; set; }
        public long IdUsuario { get; set; }
        public int IdStatus { get; set; }
        public DateTime DataHora { get; set; }
        public string Historico { get; set; }        
    }
}
