using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ws.eng.dll;
using ws.eng.obj;
using ws.web.eng.Models;
using ws.eng.dll;

namespace ws.web.eng.Controllers
{
    public class ProjetoController : Controller
    {
        // GET: Projeto
        public ActionResult Index()
        {
            ProjetoViewModel model = new ProjetoViewModel();
            ICollection<ProjetoCustomizado> projetosParTela = new List<ProjetoCustomizado>();
            FinanceiroDll finDll = new FinanceiroDll();


            if (Session["usu"] == null)
                return RedirectToAction("index", "Home");

            UsuarioClienteDll usuCliDll = new UsuarioClienteDll();
            UsuarioObj usu = (UsuarioObj)Session["usu"];
            

            if (usu.Cliente == null)
            {
                usu.Cliente = usuCliDll.BuscarCliente(usu.NomeUsuario);
            }

            model.Projetos = usuCliDll.ListarProjeto(usu.Cliente.ID).ToList();            

            foreach (var item in model.Projetos)
            {

                ProjetoCustomizado tela = new ProjetoCustomizado();

                tela.Id = item.ID.ToString().PadLeft(9, char.Parse("0"));
                tela.Regiao = item.Regiao.ToString();
                tela.TipoProj = item.Projeto.ToString();
                tela.DescrProj = string.Format("Área: {0} | Tipo: {0} | Padrão: {0} ", item.Area.ToString(), item.Projeto.ToString(), item.PadraoAcabamento.ToString());
                tela.Valor = finDll.Calcular(item.Projeto,
                                            item.Area,
                                            item.Regiao,
                                            item.PadraoAcabamento,
                                            item.CidadeID,
                                            true).ToString("N2");
                tela.Status = usuCliDll.StatusProjetos(item.Servicos);

                if (item.Servicos.Count > 0)
                {
                    tela.Servicos = new List<ServicosCustomizado>();


                    foreach (var sub in item.Servicos)
                    {
                        ServicosCustomizado subTela = new ServicosCustomizado();

                        subTela.Data = sub.DataCad.ToShortDateString();
                        subTela.Servico = sub.Servico.Nome;
                        subTela.Status = EnumObj.GetEnumDescription((StatusServico)sub.Status);
                        subTela.Valor = string.Format("{0}{1:N2}", usuCliDll.UnidadeMonetaria(item.Regiao), sub.Valor);

                        tela.Servicos.Add(subTela);
                    }
                }

                projetosParTela.Add(tela);
            }

            model.ProjetosTela = projetosParTela.ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ProjetoViewModel model)
        {
            return View();
        }

        // GET: Projeto/Details/5
        public ActionResult Detalhe(int id)
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Detalhe(int id, ProjetoViewModel model)
        {
            return View();
        }

        // GET: Projeto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projeto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Projeto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Projeto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Projeto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Projeto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Acompanhar (long Id)
        {
            if (Session["usu"] == null)
                return RedirectToAction("index", "Home");

            AcompanharViewModel model = new AcompanharViewModel();

            model.Projeto = new ProjetoDll().Buscar(Id);
            model.ProjetoSolicitacao = new ProjetoSolicitacaoDll().BuscarPorprojeto(Id);
            model.SolicitacaoID = model.ProjetoSolicitacao.ID;
            model.Historico = new List<HistoricoViewModel>();

            foreach (var hist in new ProjetoSolicitacaoDll().ListarIteracoesPorSolicitacao(model.ProjetoSolicitacao.ID))
            {
                HistoricoViewModel obj = new HistoricoViewModel();

                obj.Data = hist.Data;
                obj.EstadoID = hist.EstadoID;
                obj.LeituraRealizada = hist.LeituraRealizada;
                obj.SolicitacaoID = hist.SolicitacaoID;
                obj.Texto = hist.Texto;
                obj.UsuarioID = hist.UsuarioID;

                model.Historico.Add(obj);
            }

            foreach (var hist in new ProjetosStatusDll().ListarStatusPorprojeto(Id))
            {
                HistoricoViewModel obj = new HistoricoViewModel();

                obj.Data = hist.DataStatus;
                obj.EstadoID = hist.StatusID;
                obj.LeituraRealizada = true;
                obj.SolicitacaoID = 0;
                obj.Texto = hist.Observacao;
                obj.UsuarioID = hist.UsuarioID;

                model.Historico.Add(obj);
            }

            model.Historico = model.Historico.OrderByDescending(x => x.Data).ToList();

            model.ProjetoID = Id;

            return View(model);
        }

        [HttpPost]
        public ActionResult Acompanhar(long Id, AcompanharViewModel model)
        {
            if (Request.Form["BT_VOLTAR"] == "Voltar")
            {
                return RedirectToAction("Index");
            }

            ProjetoSolicitacaoIteracaoObj obj = new ProjetoSolicitacaoIteracaoObj();

            model.Projeto = new ProjetoDll().Buscar(model.ProjetoID);

            obj.SolicitacaoID = model.SolicitacaoID;
            obj.Data = DateTime.Now;
            obj.EstadoID = 1;            
            obj.LeituraRealizada = false;
            obj.UsuarioID = ((UsuarioObj)Session["usu"]).ID;            
            obj.Texto = model.NovoAcompanhamento;

            new ProjetoSolicitacaoDll().Incluir(obj);
            return RedirectToAction("Acompanhar", new { Id = model.ProjetoID });
        }

        public ActionResult Informar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Informar(int Id)
        {
            return View();
        }
    }
}
