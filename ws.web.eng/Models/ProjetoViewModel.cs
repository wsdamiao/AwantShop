using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public long ProjetoID { get; set; }
        public long SolicitacaoID { get; set; }
        public String NovoAcompanhamento { get; set; }
        public ProjetoObj Projeto { get; set; }
        public ProjetoSolicitacaoObj ProjetoSolicitacao { get; set; }
        public List<HistoricoViewModel> Historico { get; set; }
    }

    public class InformarViewModel
    {
        public long ProjetoID { get; set; }
        public int StatusID { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
        public String Observação { get; set; }
        public ProjetoObj Projeto { get; set; }        
    }

    public class HistoricoViewModel
    {
        public long SolicitacaoID { get; set; }
        public DateTime Data { get; set; }
        public long UsuarioID { get; set; }
        public int EstadoID { get; set; }
        public string Texto { get; set; }
        public bool LeituraRealizada { get; set; }
    }


}