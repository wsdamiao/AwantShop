using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ws.eng.obj;
using ws.eng.dll;
using ws.web.eng.Models;
using ws.web.eng.Filter;
using System.Net.Mail;
using ws.com.v2017;

namespace ws.web.eng.Controllers
{
    [AutorizationUser]
    public class ManagerController : Controller
    {
        UsuarioClienteDll usu = new UsuarioClienteDll();
        // GET: Manager
        public ActionResult Index()
        {
            //if (Session["usu"] == null)
            //    return RedirectToAction("index", "Home");

            return View();
        }

        public ActionResult UserList()
        {
            //if (Session["usu"] == null)
            //    return RedirectToAction("index", "Home");

            ViewBag.Message = TempData["Message"];

            List<UsuarioObj> model = new List<UsuarioObj>();
            
            model = usu.ListarUsuarioAdministrativo().OrderBy(x=>x.NomeCompleto).ToList();
            return View(model);
        }

        private List<SelectListItem> Popularcategorias()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            foreach (var item in new UsuarioClienteDll().ListarCategoria())
            {
                lista.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.Nome });
            }

            return lista;
        }

        private List<SelectListItem> PopularLogradouro()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            foreach (var item in new LogradouroDll().ListarCidades().OrderBy(x => x.NomeMunicipio))
            {
                lista.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.NomeMunicipio });
            }

            return lista;
        }

        private List<SelectListItem> PopularMoedas()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            lista.Add(new SelectListItem { Value = "1", Text = "Real (R$)" });
            lista.Add(new SelectListItem { Value = "2", Text = "Euro (EU$)" });
            lista.Add(new SelectListItem { Value = "3", Text = "Dollar (US$)" });

            return lista;
        }

        public ActionResult User(int Id)
        {
            //if (Session["usu"] == null)
            //    return RedirectToAction("index", "Home");

            UsuarioModel model = new UsuarioModel();            
                                    
            model.Categorias = Popularcategorias();

            if(Id != 0)
            {
                UsuarioClienteDll usuDll = new UsuarioClienteDll();
                UsuarioObj usu = new UsuarioObj();

                usu = usuDll.BuscarUsuario(Id);

                if(usu != null)
                {
                    model.Id = usu.ID;
                    model.CategoriaID = usu.CategoriaID;
                    model.NomeCompleto = usu.NomeCompleto;
                    model.NomeUsuario = usu.NomeUsuario;
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult User(int id, UsuarioModel model)
        {
            //if (Session["usu"] == null)
            //    return RedirectToAction("index", "Home");

            model.Categorias = Popularcategorias();
                       

            if (ModelState.IsValid)
            {
                UsuarioObj usu = new UsuarioObj();
                UsuarioClienteDll usuDll = new UsuarioClienteDll();                

                    try
                {
                    if (model.Id != 0)
                        usu = usuDll.BuscarUsuario(model.Id);

                    usu.NomeUsuario = model.NomeUsuario;
                    usu.NomeCompleto = model.NomeCompleto;
                    usu.CategoriaID = model.CategoriaID;
                    
                    if (usu.ID == 0)
                    {
                        usu.Senha = DadosProjeto.SENHAPADRAO;

                        usuDll.CriarUsuario(usu, false);
                        TempData["Message"] = "Usuário cadastrado com sucesso";
                    }
                    else
                    {
                        usuDll.AlterarUsuario(usu, false);
                        TempData["Message"] = "Usuário alterado com sucesso";
                    }
                }
                catch (Exception ex)
                {
                    //salvarLog(ex);
                    TempData["Message"] = "O usuário não pode ser criado no momento. O erro encontrado já foi enviado para o suporte";
                }

                return RedirectToAction("UserList");
            }

            return View(model);
        }
                
        public ActionResult UserPassword(int Id)
        {
            UsuarioObj usu = new UsuarioObj();
            UsuarioClienteDll usuDll = new UsuarioClienteDll();            
           
            usu = usuDll.BuscarUsuario(Id);
                        
            usu.Senha = DadosProjeto.SENHAPADRAO;
            usu.Ativo = false;

            usuDll.AlterarUsuario(usu,true);

            TempData["Message"] = "Usuário resetado com sucesso";

            return RedirectToAction("UserList");
        }

        public ActionResult DistanciaList()
        {
            DistanciaRodoviariaDll disDll = new DistanciaRodoviariaDll();
            LogradouroDll logrDll = new LogradouroDll();

            List<DistanciaRodoviariaViewModel> lista = new List<DistanciaRodoviariaViewModel>();
            
            foreach (var item in disDll.Listar())
            {
                DistanciaRodoviariaViewModel obj = new DistanciaRodoviariaViewModel();

                if(item.Distancia != null)
                    obj.DistanciaRodoviaria = item.Distancia.Value;

                obj.ID = item.ID;
                obj.LogradouroID = item.LogradouroID;
                obj.NomeCidade = logrDll.BuscarMunicipioPorId(item.LogradouroID).NomeMunicipio;                               
                obj.Valor = item.ValorPercentual;

                lista.Add(obj);

            }

            ViewBag.Message = TempData["Message"];

            return View(lista.OrderBy(x =>x.NomeCidade).ToList());
        }

        [HttpPost]
        public ActionResult DistanciaList(DistanciaRodoviariaViewModel model)
        {
            return View();
        }

        public ActionResult DistanciaRodoviaria(int Id)
        {
            DistanciaRodoviariaViewModel model = new DistanciaRodoviariaViewModel();
            FinanceiroDll finDll = new FinanceiroDll();

            model.Logradouros = PopularLogradouro();         
            model.ValorBase =  finDll.BuscarValorMetroQuadrado(RegiaoProjeto.Brasil);

            if(Id > 0)
            {
                DistanciaRodoviariaDll dll = new DistanciaRodoviariaDll();
                DistanciaRodoviariaObj obj = new DistanciaRodoviariaObj();

                obj = dll.Buscar(Id);

                model.LogradouroID = obj.LogradouroID;
                model.Valor = obj.ValorPercentual;
                if (obj.Distancia != null)
                    model.DistanciaRodoviaria = obj.Distancia.Value;

            }

            return View(model);
        }

        [HttpPost]
        public ActionResult DistanciaRodoviaria(int Id,DistanciaRodoviariaViewModel model)
        {
            DistanciaRodoviariaDll disDll = new DistanciaRodoviariaDll();
            DistanciaRodoviariaObj obj = new DistanciaRodoviariaObj();

            obj.Distancia = model.DistanciaRodoviaria;
            obj.LogradouroID = model.LogradouroID;
            obj.ValorPercentual = model.Valor;
            obj.ID = model.ID;

            try
            {
                disDll.Salvar(obj);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            
            return RedirectToAction("DistanciaList");
        }

        public ActionResult PaisList()
        {            
            LogradouroDll logrDll = new LogradouroDll();

            List<PaisViewModel> lista = new List<PaisViewModel>();

            foreach (var item in logrDll.ListarPaises())
            {
                PaisViewModel obj = new PaisViewModel();

                obj.Valor = item.MetroQuadrado;
                obj.ID = item.ID;
                obj.NomeOficial = item.NomeOficial;
                obj.NomePais = item.Nome;
                obj.NomeMoeda = PopularMoedas()[item.MoedaID - 1].Text;
                
                lista.Add(obj);

            }

            ViewBag.Message = TempData["Message"];

            return View(lista.OrderBy(x => x.NomePais).ToList());
        }

        [HttpPost]
        public ActionResult PaisList(PaisViewModel model)
        {
            return View(model);
        }
        
        public ActionResult Pais(int Id)
        {
            PaisViewModel model = new PaisViewModel();

            model.Moedas = PopularMoedas();

            if (Id > 0)
            {
                LogradouroDll dll = new LogradouroDll();
                PaisObj obj = new PaisObj();

                obj = dll.BuscarPaisPorId(Id);

                model.NomeOficial = obj.NomeOficial;
                model.NomePais = obj.Nome;
                model.Valor = obj.MetroQuadrado;
                model.MoedaID = obj.MoedaID;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Pais(int Id, PaisViewModel model)
        {
            LogradouroDll dll = new LogradouroDll();
            PaisObj obj = new PaisObj();

            obj.Nome = model.NomePais;
            obj.NomeOficial = model.NomeOficial;
            obj.MetroQuadrado = model.Valor;
            obj.MoedaID = model.MoedaID;
            obj.ID = model.ID;

            try
            {
                dll.Salvar(obj);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("PaisList");
        }       

    }
}