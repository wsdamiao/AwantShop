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
                return RedirectToAction("Login", "Account");
            
            ICollection<DashboardViewModel> model = new List<DashboardViewModel>();            

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