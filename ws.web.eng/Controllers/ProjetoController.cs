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

            model.Historico = new ProjetoAcompanharDll().ListarPorProjeto(Id);
            model.Projeto = new ProjetoDll().Buscar(Id);
            model.NovoAcompanhamento = new ProjetoAcompanharObj();

            return View(model);
        }

        [HttpPost]
        public ActionResult Acompanhar(int Id, ProjetoObj model)
        {
            return View();
        }
    }
}
