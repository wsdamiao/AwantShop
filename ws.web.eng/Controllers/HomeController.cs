using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ws.eng.dll;
using ws.eng.obj;
using ws.web.eng.Models;
using System.Net.Mail;

namespace ws.web.eng.Controllers
{
    public class HomeController : Controller
    {
        //private ProjetoModel _projeto;

        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    base.OnActionExecuting(filterContext);

        //    _projeto = (SerializationUtility.Deserialize(Request.Form["_projeto"]) ?? TempData["_projeto"] ?? new ProjetoModel()) as ProjetoModel;
        //    TryUpdateModel(_projeto);
        //}

        //protected override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    base.OnActionExecuted(filterContext);

        //    if (filterContext.Result is RedirectToRouteResult)
        //        TempData["_projeto"] = _projeto;
        //}

        public ActionResult Index()
        {
            ProjetoModel model = new ProjetoModel();
            ViewBag.MsgErro = "";
            //model.Municipios = PopularCidades("RJ");

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ProjetoModel model)
        {
            if (Request.Form["BT_PT"] == "")
            {
                model.Regiao = RegiaoProjeto.Portugal;
                TempData["_projeto"] = model;
                return RedirectToAction("Projeto");
            }
            else if (Request.Form["BT_BR"] == "")
            {
                model.Regiao = RegiaoProjeto.Brasil;
                TempData["_projeto"] = model;
                return RedirectToAction("Projeto");
            }
            else if (Request.Form["BT_RJ"] == "")
            {
                model.Regiao = RegiaoProjeto.RioDeJaneiro;
                TempData["_projeto"] = model;
                return RedirectToAction("Regiao");
            }

           
            return View(model);
        }

        public ActionResult Regiao()
        {
            ProjetoModel model = (ProjetoModel)TempData["_projeto"];

            if (model == null)
                return RedirectToAction("Index");

            ViewBag.MsgErro = "";
            model.Municipios = PopularCidades("RJ");

            return View(model);
        }

        [HttpPost]
        public ActionResult Regiao(ProjetoModel model)
        {
            if (model.CodMunicipio != null)
            {
                model.Regiao = RegiaoProjeto.RioDeJaneiro;
                TempData["_projeto"] = model;
                return RedirectToAction("Projeto");
            }
            else if (Request.Form["BT_VOLTAR"] == "voltar")
            {
               return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Selecione uma cidade para continuar";
            }


            model.Municipios = PopularCidades("RJ");
            return View(model);
        }

        [HttpGet]
        public ActionResult Projeto()
        {
            ProjetoModel model = (ProjetoModel)TempData["_projeto"];

            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }

        [HttpPost]
        public ActionResult Projeto(ProjetoModel model)
        {
            if(model == null)
                return RedirectToAction("Index");

            model.Regiao = (RegiaoProjeto)int.Parse(Request.Form["idRegiao"].ToString());

            if (Request.Form["BT_R"] == "")
            {
                model.Projeto = TipoProjeto.Residencial;
                TempData["_projeto"] = model;
                return RedirectToAction("Area");
            }
            else if (Request.Form["BT_C"] == "")
            {
                model.Projeto = TipoProjeto.Comercial;
                TempData["_projeto"] = model;
                return RedirectToAction("Area");
            }
            else if (Request.Form["BT_I"] == "")
            {
                model.Projeto = TipoProjeto.Industrial;
                TempData["_projeto"] = model;
                return RedirectToAction("Area");
            }
            else if (Request.Form["BT_PER"] == "")
            {
                model.Projeto = TipoProjeto.Personalizado;
                TempData["_projeto"] = model;
                return RedirectToAction("Persoalizado");
            }
            else if (Request.Form["BT_VOLTAR"] == "voltar")
            {
                
                TempData["_projeto"] = model;
                if (model.Regiao == RegiaoProjeto.RioDeJaneiro)
                {
                    return RedirectToAction("Regiao");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        public ActionResult Area()
        {
            ProjetoModel model = (ProjetoModel)TempData["_projeto"];

            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }

        [HttpPost]
        public ActionResult Area(ProjetoModel model)
        {
            if (model == null)
                return RedirectToAction("Index");

            model.Regiao = (RegiaoProjeto)int.Parse(Request.Form["idRegiao"].ToString());
            model.Projeto = (TipoProjeto)int.Parse(Request.Form["idProjeto"].ToString());

            if (Request.Form["BT_P"] == "")
            {
                model.Area = AreaProjeto.Pequeno;
                TempData["_projeto"] = model;
                return RedirectToAction("Padrao");
            }
            else if (Request.Form["BT_M"] == "")
            {
                model.Area = AreaProjeto.Medio;
                TempData["_projeto"] = model;
                return RedirectToAction("Padrao");
            }
            else if (Request.Form["BT_G"] == "")
            {
                model.Area = AreaProjeto.Grande;
                TempData["_projeto"] = model;
                return RedirectToAction("Padrao");
            }
            else if (Request.Form["BT_PER"] == "")
            {
                model.Area = AreaProjeto.Personalizado;
                TempData["_projeto"] = model;
                return RedirectToAction("Persoalizado");
            }
            else if (Request.Form["BT_VOLTAR"] == "voltar")
            {
                TempData["_projeto"] = model;
                return RedirectToAction("Projeto");
            }

            return View(model);
        }

        public ActionResult Padrao()
        {
            ProjetoModel model = (ProjetoModel)TempData["_projeto"];

            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }

        [HttpPost]
        public ActionResult Padrao(ProjetoModel model)
        {
            if (model == null)
                return RedirectToAction("Index");

            model.Regiao = (RegiaoProjeto)int.Parse(Request.Form["idRegiao"].ToString());
            model.Projeto = (TipoProjeto)int.Parse(Request.Form["idProjeto"].ToString());
            model.Area = (AreaProjeto)int.Parse(Request.Form["idArea"].ToString());

            if (Request.Form["BT_P"] == "")
            {
                model.PadraoAcabamento = PadraoProjeto.BaixoCusto;
                TempData["_projeto"] = model;
                return RedirectToAction("Servicos");
            }
            else if (Request.Form["BT_M"] == "")
            {
                model.PadraoAcabamento = PadraoProjeto.MedioCusto;
                TempData["_projeto"] = model;
                return RedirectToAction("Servicos");
            }
            else if (Request.Form["BT_G"] == "")
            {
                model.PadraoAcabamento = PadraoProjeto.AltoCusto;
                TempData["_projeto"] = model;
                return RedirectToAction("Servicos");
            }
            else if (Request.Form["BT_PER"] == "")
            {
                model.PadraoAcabamento = PadraoProjeto.Personalizado;
                TempData["_projeto"] = model;
                return RedirectToAction("Persoalizado");
            }
            else if (Request.Form["BT_VOLTAR"] == "voltar")
            {
                TempData["_projeto"] = model;
                return RedirectToAction("Area");
            }

            return View(model);
        }

        public ActionResult Servicos()
        {
            ProjetoModel model = (ProjetoModel)TempData["_projeto"];

            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }

        [HttpPost]
        public ActionResult Servicos(ProjetoModel model)
        {
            if (model == null)
                return RedirectToAction("Index");

            model.Regiao = (RegiaoProjeto)int.Parse(Request.Form["idRegiao"].ToString());
            model.Projeto = (TipoProjeto)int.Parse(Request.Form["idProjeto"].ToString());
            model.Area = (AreaProjeto)int.Parse(Request.Form["idArea"].ToString());
            model.PadraoAcabamento = (PadraoProjeto)int.Parse(Request.Form["idPadrao"].ToString());

            TempData["_projeto"] = model;

            if (Request.Form["BT_PROX"] == "Continuar")
            {                
                return RedirectToAction("Formulario");
            }
            else if (Request.Form["BT_PER"] == "")
            {
                return RedirectToAction("Persoalizado");
            }
            else if (Request.Form["BT_VOLTAR"] == "voltar")
            {
                return RedirectToAction("Padrao");
            }

            return View(model);
        }

        public ActionResult Formulario()
        {
            ProjetoModel model = (ProjetoModel)TempData["_projeto"];

            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }

        [HttpPost]
        public ActionResult Formulario(ProjetoModel model)
        {
            if (model == null)
                return RedirectToAction("Index");

            model.Regiao = (RegiaoProjeto)int.Parse(Request.Form["idRegiao"].ToString());
            model.Projeto = (TipoProjeto)int.Parse(Request.Form["idProjeto"].ToString());
            model.Area = (AreaProjeto)int.Parse(Request.Form["idArea"].ToString());
            model.PadraoAcabamento = (PadraoProjeto)int.Parse(Request.Form["idPadrao"].ToString());
            model.ProjetoArquitetonico = bool.Parse(Request.Form["PA"].ToString());
            model.ProjetoEletrico = bool.Parse(Request.Form["PE"].ToString());
            model.ProjetoHidraSanitario = bool.Parse(Request.Form["PHS"].ToString());

            TempData["_projeto"] = model;

            ViewBag.Message = "";

            
                if (Request.Form["BT_PROX"] == "Continuar")
                {
                    if (model.CpfCnpj == null)
                        ViewBag.Message += "- Campo CPF/CNPJ deve ser informado<br>";
                    if (model.Nome == null)
                        ViewBag.Message += "- Campo Nome deve ser informado<br>";
                    if (model.Email == null)
                        ViewBag.Message += "- Campo E-mail deve ser informado<br>";
                    if (model.TelContato1 == null)
                        ViewBag.Message += "- Campo Contato Prefêrencial deve ser informado<br>";
                    if (ViewBag.Message == "")
                    {
                        return RedirectToAction("Resumo");
                    }
                }
                else if (Request.Form["BT_VOLTAR"] == "voltar")
                {
                    return RedirectToAction("Servicos");
                }
            

            ViewBag.Message = "Verifique o preenchimento: <br>" + ViewBag.Message;
            return View(model);
        }

        public ActionResult Resumo()
        {
            FinanceiroDll finDll = new FinanceiroDll();
            LogradouroDll logDll = new LogradouroDll();
            UsuarioClienteDll usuCliDll = new UsuarioClienteDll();

            ProjetoModel model = (ProjetoModel)TempData["_projeto"];
                        
            if (model == null)
                return RedirectToAction("Index");

            if (model.CodMunicipio != null)
                model.CidadeID = logDll.BuscarMunicipio(model.CodMunicipio).ID;
            else
                model.CidadeID = 0;


            model.ValorMetroQuadradoAplicado = finDll.BuscarValorMetroQuadrado(model.Regiao);

            if (model.ProjetoArquitetonico)
            {
                model.ValorProjetoArquitetonico = finDll.Calcular(model.Projeto,
                                                                    model.Area,
                                                                    model.Regiao,
                                                                    model.PadraoAcabamento,
                                                                    ServicosProjeto.ProjetoArquitetonico,
                                                                    model.CidadeID);

                model.ValorProjetoTotal += model.ValorProjetoArquitetonico;
            }

            model.ValorProjetoArquitetonicoFormatado =  string.Format("{0}{1:N2}", usuCliDll.UnidadeMonetaria(model.Regiao), model.ValorProjetoArquitetonico);

            if (model.ProjetoEletrico)
            {
                model.ValorProjetoEletrico = finDll.Calcular(model.Projeto,
                                                                    model.Area,
                                                                    model.Regiao,
                                                                    model.PadraoAcabamento,
                                                                    ServicosProjeto.ProjetoEletrico,
                                                                    model.CidadeID);

                model.ValorProjetoTotal += model.ValorProjetoEletrico;
            }

            model.ValorProjetoEletricoFormatado = string.Format("{0}{1:N2}", usuCliDll.UnidadeMonetaria(model.Regiao), model.ValorProjetoEletrico);

            if (model.ProjetoHidraSanitario)
            {
                model.ValorProjetoHidroSanitario = finDll.Calcular(model.Projeto,
                                                                    model.Area,
                                                                    model.Regiao,
                                                                    model.PadraoAcabamento,
                                                                    ServicosProjeto.ProjetoHidroSanitario,
                                                                    model.CidadeID);

                model.ValorProjetoTotal += model.ValorProjetoHidroSanitario;
            }

            model.ValorProjetoHidroSanitarioFormatado = string.Format("{0}{1:N2}", usuCliDll.UnidadeMonetaria(model.Regiao), model.ValorProjetoHidroSanitario);

            if (model.ProjetoExecutivo)
            {
                model.ValorProjetoExecutivo = finDll.Calcular(model.Projeto,
                                                                    model.Area,
                                                                    model.Regiao,
                                                                    model.PadraoAcabamento,
                                                                    ServicosProjeto.ProjetoExecutivo,
                                                                    model.CidadeID);

                model.ValorProjetoTotal += model.ValorProjetoExecutivo;
            }
            
            model.ValorProjetoExecutivoFormatado = string.Format("{0}{1:N2}", usuCliDll.UnidadeMonetaria(model.Regiao), model.ValorProjetoExecutivo);

            model.ValorProjetoTotalFormatado = string.Format("{0}{1:N2}", usuCliDll.UnidadeMonetaria(model.Regiao), model.ValorProjetoTotal);

            return View(model);
        }

        [HttpPost]
        public ActionResult Resumo(ProjetoModel model)
        {
            UsuarioClienteDll cliDll = new UsuarioClienteDll();
            FinanceiroDll finDll = new FinanceiroDll();

            UsuarioObj usu = new UsuarioObj();
            ClienteObj cli = new ClienteObj();
            ProjetoObj pro = new ProjetoObj();
            

            DateTime dataCad = DateTime.Now;

            model.Regiao = (RegiaoProjeto)int.Parse(Request.Form["idRegiao"].ToString());
            model.Projeto = (TipoProjeto)int.Parse(Request.Form["idProjeto"].ToString());
            model.Area = (AreaProjeto)int.Parse(Request.Form["idArea"].ToString());
            model.PadraoAcabamento = (PadraoProjeto)int.Parse(Request.Form["idPadrao"].ToString());
            model.ProjetoArquitetonico = bool.Parse(Request.Form["PA"].ToString());
            model.ProjetoEletrico = bool.Parse(Request.Form["PE"].ToString());
            model.ProjetoHidraSanitario = bool.Parse(Request.Form["PHS"].ToString());

            model.ValorProjetoArquitetonico = decimal.Parse(Request.Form["ValorProjetoArquitetonico"].ToString());
            model.ValorProjetoEletrico = decimal.Parse(Request.Form["ValorProjetoEletrico"].ToString());
            model.ValorProjetoHidroSanitario = decimal.Parse(Request.Form["ValorProjetoHidroSanitario"].ToString());
            model.ValorProjetoExecutivo = decimal.Parse(Request.Form["ValorProjetoExecutivo"].ToString());

            model.CpfCnpj = Request.Form["CpfCnpj"].ToString();
            model.Nome = Request.Form["Nome"].ToString();
            model.TelContato1 = Request.Form["TelContato1"].ToString();
            model.TelContato2 = Request.Form["TelContato2"].ToString();
            model.TelContato3 = Request.Form["TelContato3"].ToString();
            model.TextoLivre = Request.Form["TextoLivre"].ToString();
            model.CidadeID = int.Parse(Request.Form["CidadeID"].ToString());
            model.Email = Request.Form["Email"].ToString();


            if (model == null)
                return RedirectToAction("Index");

            TempData["_projeto"] = model;

            if (Request.Form["BT_PROX"] == "Enviar seu Projeto")
            {
                //popularUsuario
                usu.NomeUsuario = model.CpfCnpj;
                usu.CategoriaID = (int)CategoriaUsuario.Cliente;

                cli.Email = model.Email;
                cli.CPF_CNPJ = model.CpfCnpj;
                cli.Nome = model.Nome;
                cli.TelContato1 = model.TelContato1;
                cli.TelContato2 = model.TelContato2;
                cli.TelContato3 = model.TelContato3;

                
                cliDll.CriarUsuario(usu,true);                
                cliDll.SalvarCliente(cli);

                cli = cliDll.BuscarCliente(cli.CPF_CNPJ);

                pro.Cliente = cli;
                pro.ClienteID = cli.ID;
                pro.Regiao = model.Regiao;
                pro.logradouroID = int.Parse(model.CodMunicipio == null ? "0" : model.CodMunicipio);
                pro.Projeto = model.Projeto;
                pro.Area = model.Area;
                pro.PadraoAcabamento = model.PadraoAcabamento;
                pro.AreaPersonalizada = model.AreaPersonalizada;
                pro.ValorMetroQuadradoAplicado = finDll.BuscarValorMetroQuadrado(model.Regiao);                
                pro.DataCad = dataCad;
                pro.TextoLivre = model.TextoLivre;

                usu = cliDll.BuscarUsuario(cli.CPF_CNPJ);                
                pro.Servicos = new List<ProjetoServicoObj>();

                if (model.ProjetoArquitetonico)
                {
                    ProjetoServicoObj ser = new ProjetoServicoObj();

                    ser.ServicoID = (int)ServicosProjeto.ProjetoArquitetonico;
                    ser.DataCad = dataCad;
                    ser.FormaPagamento = FormaPgto.Boleto;                    
                    ser.Status = (int)StatusServico.Criado;
                    ser.Usuario = usu;
                    ser.UsuarioID = usu.ID;
                    ser.Descricao = "Projeto arquitetônico";                    
                    ser.Valor = model.ValorProjetoArquitetonico;

                    finDll.Calcular(model.Projeto,
                                    model.Area,
                                    model.Regiao,
                                    model.PadraoAcabamento,
                                    ServicosProjeto.ProjetoArquitetonico,
                                    model.CidadeID);

                    ser.T = finDll.T;
                    ser.P = finDll.P;
                    ser.A = finDll.A;
                    ser.d = finDll.D;

                    pro.Servicos.Add(ser);
                }

                if (model.ProjetoExecutivo)
                {
                    ProjetoServicoObj ser = new ProjetoServicoObj();

                    ser.ServicoID = (int)ServicosProjeto.ProjetoExecutivo;
                    ser.DataCad = dataCad;
                    ser.FormaPagamento = FormaPgto.Boleto;
                    ser.Status = (int)StatusServico.Criado;
                    ser.Usuario = usu;
                    ser.UsuarioID = usu.ID;
                    ser.Descricao = "Projeto Executivo";
                    ser.Valor = model.ValorProjetoExecutivo;

                    finDll.Calcular(model.Projeto,
                                    model.Area,
                                    model.Regiao,
                                    model.PadraoAcabamento,
                                    ServicosProjeto.ProjetoExecutivo,
                                    model.CidadeID);

                    ser.T = finDll.T;
                    ser.P = finDll.P;
                    ser.A = finDll.A;
                    ser.d = finDll.D;

                    pro.Servicos.Add(ser);
                }

                if (model.ProjetoEletrico)
                {
                    ProjetoServicoObj ser = new ProjetoServicoObj();

                    ser.ServicoID = (int)ServicosProjeto.ProjetoEletrico;
                    ser.DataCad = dataCad;
                    ser.FormaPagamento = FormaPgto.Boleto;
                    ser.Status = (int)StatusServico.Criado;
                    ser.Usuario = usu;
                    ser.UsuarioID = usu.ID;
                    ser.Descricao = "Projeto Elétrico";
                    ser.Valor = model.ValorProjetoEletrico;

                    finDll.Calcular(model.Projeto,
                                    model.Area,
                                    model.Regiao,
                                    model.PadraoAcabamento,
                                    ServicosProjeto.ProjetoEletrico,
                                    model.CidadeID);


                    ser.T = finDll.T;
                    ser.P = finDll.P;
                    ser.A = finDll.A;
                    ser.d = finDll.D;

                    pro.Servicos.Add(ser);
                }

                if (model.ProjetoHidraSanitario)
                {
                    ProjetoServicoObj ser = new ProjetoServicoObj();

                    ser.ServicoID = (int)ServicosProjeto.ProjetoHidroSanitario;
                    ser.DataCad = dataCad;
                    ser.FormaPagamento = FormaPgto.Boleto;                    
                    ser.Status = (int)StatusServico.Criado;
                    ser.Usuario = usu;
                    ser.UsuarioID = usu.ID;
                    ser.Valor = model.ValorProjetoHidroSanitario;
                    ser.Descricao = "Projeto Hidráulico / Sanitário";

                    finDll.Calcular(model.Projeto,
                                    model.Area,
                                    model.Regiao,
                                    model.PadraoAcabamento,
                                    ServicosProjeto.ProjetoHidroSanitario,
                                    model.CidadeID);

                    ser.T = finDll.T;
                    ser.P = finDll.P;
                    ser.A = finDll.A;
                    ser.d = finDll.D;

                    pro.Servicos.Add(ser);
                }
                
                cliDll.SalvarProjeto(pro);

                return RedirectToAction("Financeiro");
            }
            else if (Request.Form["BT_VOLTAR"] == "voltar")
            {
                return RedirectToAction("Formulario");
            }

            return View(model);
        }

        public ActionResult Financeiro()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Financeiro(ProjetoModel model)
        {
            return View();
        }

        public ActionResult Sobre()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contato()
        {
            ViewBag.Message = TempData["Message"];

            return View();
        }

        private void GravarMensagem(EmailModel model)
        {
            MensagemClienteObj msg = new MensagemClienteObj();

            msg.DtEnvio = DateTime.Now;
            msg.Email = model.Email;
            msg.Mensagem = model.Mensagem;
            msg.Nome = model.Nome;
            msg.Telefone = model.Telefone;

            ContatoDll dll = new ContatoDll();
            dll.Salvar(msg);
        }

        [HttpPost]
        public ActionResult Contato(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                ws.com.v2017.Email mail = new ws.com.v2017.Email();

                mail.CorpoHTML = true;
                mail.EmailServico = DadosProjeto.EMAILDOSISTEMA;
                mail.SenhaEmailServico = DadosProjeto.EMAILDOSISTEMASENHA;
                mail.Servidor = DadosProjeto.EMAILDOSISTEMASERVIDOR;
                mail.Ssl = DadosProjeto.EMAILSSL;
                mail.Porta = DadosProjeto.EMAILPORTA;
                mail.Mensagem = "Mensagem: " + model.Mensagem + "Telefone: " + model.Telefone + "Nome: " + model.Nome;
                mail.Prioridade = MailPriority.High;
                mail.Titulo = "Contato realizado no site por " + model.Nome;

                mail.Destinatarios = new List<com.v2017.Destinatario>();
                mail.Destinatarios.Add(new com.v2017.Destinatario("Contato do Site", DadosProjeto.EMAILDOSISTEMA));

                try
                {
                    GravarMensagem(model);
                    mail.MontarMensagem();
                    mail.Enviar();

                    TempData["Message"] = "Sua mensagem foi enviada para nossos consultores";
                }
                catch(Exception ex)
                {
                    TempData["Message"] = ex.Message;
                }
                
                return RedirectToAction("Contato");
            }
            else
                return View();
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff(string id)
        {            
            Session["usu"] = null;
            return RedirectToAction("index");
        }

        private List<SelectListItem> PopularCidades(string SiglaUF)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            foreach(var item in new LogradouroDll().BuscarMunicipiosPorSiglaUF("RJ"))
            {
                lista.Add(new SelectListItem { Value = item.CodMunicipio, Text = item.NomeMunicipio });
            }

            return lista;

            //return new List<SelectListItem>()
            //    (
            //        new LogradouroDll().BuscarMunicipiosPorSiglaUF("RJ"),
            //        "CodMunicipio",
            //        "NomeMunicipio"
            //    );
        }
    }
}