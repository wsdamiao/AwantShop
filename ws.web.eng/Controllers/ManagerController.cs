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
            
            model = usu.ListarUsuarioAdministrativo();
            return View(model);
        }

        public ActionResult User(int Id)
        {
            //if (Session["usu"] == null)
            //    return RedirectToAction("index", "Home");

            UsuarioModel model = new UsuarioModel();            

            List<SelectListItem> lista = new List<SelectListItem>();
            foreach (var item in new UsuarioClienteDll().ListarCategoria())
            {
                lista.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.Nome });
            }

            model.Categorias = lista;

            if(Id != 0)
            {
                UsuarioClienteDll usuDll = new UsuarioClienteDll();
                UsuarioObj usu = new UsuarioObj();

                usu = usuDll.BuscarUsuario(Id);

                if(usu != null)
                {
                    model.CategoriaID = usu.CategoriaID;
                    model.NomeCompleto = usu.NomeCompleto;
                    model.NomeUsuario = usu.NomeUsuario;
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult User(int id,UsuarioObj model)
        {
            //if (Session["usu"] == null)
            //    return RedirectToAction("index", "Home");

            UsuarioObj usu = new UsuarioObj();
            UsuarioClienteDll usuDll = new UsuarioClienteDll();

            try
            {
                if (usu.ID != 0)
                    usu = usuDll.BuscarUsuario(model.ID);

                usu.NomeUsuario = model.NomeUsuario;
                usu.NomeCompleto = model.NomeCompleto;
                usu.CategoriaID = model.CategoriaID;

                if (usu.ID == 0)
                {
                    usuDll.CriarUsuario(usu, true);
                    TempData["Message"] = "Usuário cadastrado com sucesso";
                }
                else
                {
                    usuDll.AlterarUsuario(usu,false);
                    TempData["Message"] = "Usuário alterado com sucesso";
                }
            }
            catch(Exception ex)
            {
                //salvarLog
                TempData["Message"] = "O usuário não pode ser criado no momento. O erro encontrado já foi enviado para o suporte";
            }
            
            return RedirectToAction("UserList");
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



    }
}