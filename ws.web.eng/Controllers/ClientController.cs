using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ws.eng.obj;
using ws.web.eng.Models;
using ws.eng.dll;

namespace ws.web.eng.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            UsuarioClienteDll cli = new UsuarioClienteDll();
            PesquisarClienteViewModel model = new PesquisarClienteViewModel();
            model.Clientes = cli.Listar(10);

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PesquisarClienteViewModel model)
        {
            UsuarioClienteDll cli = new UsuarioClienteDll();

            model.Clientes = cli.ListarPor(model.PesquisaPor, model.ValorPesquisa);

            return View(model);
        }

            // GET: Client/Details/5
            public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
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

        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Client/Edit/5
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

        // GET: Client/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Client/Delete/5
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
    }
}
