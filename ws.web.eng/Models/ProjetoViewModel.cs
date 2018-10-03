using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ws.eng.obj;

namespace ws.web.eng.Models
{
    public class ProjetoViewModel
    {
        public string CampoPesquisa { get; set; }
        public string ValorPesquisa { get; set; }
        public ClienteObj Cliente { get; set; }
        public List<ProjetoObj> Projetos { get; set; }
        public List<ProjetoCustomizado> ProjetosTela { get; set; }
    }

    public class ProjetoCustomizado
    {
        public string Id { get; set; }
        public string TipoProj { get; set; }
        public string DescrProj { get; set; }
        public string Regiao { get; set; }
        public string Valor { get; set; }
        public string Status { get; set; }
        public ICollection<ServicosCustomizado> Servicos { get; set; }
    }

    public class ServicosCustomizado
    {
        public string Servico { get; set; }
        public string Valor { get; set; }
        public string Data { get; set; }
        public string Status { get; set; }
        public string Pagamento { get; set; }
    }

    public class AcompanharViewModel
    {
        public ProjetoAcompanharObj NovoAcompanhamento { get; set; }
        public ProjetoObj Projeto { get; set; }
        public List<ProjetoAcompanharObj> Historico { get; set; }
    }
    
}