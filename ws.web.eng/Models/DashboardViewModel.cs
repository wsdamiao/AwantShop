using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ws.eng.obj;

namespace ws.web.eng.Models
{
    public class DashboardViewModel
    {
        public string Id { get; set; }
        public string DescrProj { get; set; }
        public string Regiao { get; set; }
        public string Valor { get; set; }
        public string Status { get; set; }
        public ICollection<ServicosViewModel> Servicos { get; set; }
    }

    public class ServicosViewModel
    {
        public string Servico { get; set; }
        public string Valor { get; set; }
        public string Data { get; set; }
        public string Status { get; set; }
        public string Pagamento { get; set; }
    }
}