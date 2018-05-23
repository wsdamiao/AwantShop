using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ws.eng.obj
{
    public class MensagemClienteObj
    {
        public int ID { get; set; }
        public int? UsuarioID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Mensagem { get; set; }
        public DateTime DtEnvio { get; set; }
        public DateTime? DtLeitura { get; set; }
        public UsuarioObj Usuario { get; set; }
    }
}
