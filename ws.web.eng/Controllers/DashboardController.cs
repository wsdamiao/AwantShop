using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ws.eng.dll;
using ws.eng.obj;
using ws.web.eng.Models;
using ws.web.eng.Filter;


namespace ws.web.eng.Controllers
{
    [AutorizationUser]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (Session["usu"] == null)
                return RedirectToAction("index", "Home");

            UsuarioClienteDll usuCliDll = new UsuarioClienteDll();
            UsuarioObj usu = (UsuarioObj)Session["usu"];
            ICollection<DashboardViewModel> model = new List<DashboardViewModel>();

            if(usu.Cliente == null)
            {
                usu.Cliente = usuCliDll.BuscarCliente(usu.NomeUsuario);
            }

            ICollection<ProjetoObj> projetos = usuCliDll.ListarProjeto(usu.Cliente.ID);

            foreach (var item in projetos)
            {
                DashboardViewModel _model = new DashboardViewModel();
                _model.Id = item.ID.ToString().PadLeft(9, char.Parse("0"));
                _model.Regiao = item.Regiao.ToString();
                _model.DescrProj = string.Format("Área: {0} | Tipo: {0} | Padrão: {0} ", item.Area.ToString(), item.Projeto.ToString(), item.PadraoAcabamento.ToString());
                _model.Valor = usuCliDll.CalculaValorTotalServicos(item.Servicos, item.Regiao);
                _model.Status = usuCliDll.StatusProjetos(item.Servicos);

                _model.Servicos = new List<ServicosViewModel>();


                foreach (var sub in item.Servicos)
                {
                    ServicosViewModel _sub = new ServicosViewModel();

                    _sub.Data = sub.DataCad.ToShortDateString();
                    _sub.Servico = EnumObj.GetEnumDescription((ServicosProjeto)sub.ServicoID);
                    _sub.Status = EnumObj.GetEnumDescription((StatusServico)sub.Status);
                    _sub.Valor = string.Format("{0}{1:N2}", usuCliDll.UnidadeMonetaria(item.Regiao), sub.Valor);

                    _model.Servicos.Add(_sub);
                }

                model.Add(_model);
            }

            return View(model);
        }

        // GET: Dashboard
        public ActionResult Cancelar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cancelar(DashboardViewModel model)
        {
            ServicoHistoricoObj obj = new ServicoHistoricoObj();
            HistoricoServico dll = new HistoricoServico();

            foreach (var item in model.Servicos)
            {
                obj.DataHora = DateTime.Now;
                obj.Historico = "Solicitação de cancelemento realizada pelo cliente na página do site";
                obj.IdServico = 1;
                obj.IdStatus = 1;
                obj.IdUsuario = ((UsuarioObj)Session["usu"]).ID;

                dll.Salvar(obj);                
            }

            return RedirectToAction("Mensagem");
        }

        // GET: Dashboard
        public ActionResult Acompanhamento(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Acompanhamento()
        {
            return View();
        }

        // GET: Dashboard
        public ActionResult Mensagem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Mensagem(DashboardViewModel model)
        {
            return View();
        }
    }
}