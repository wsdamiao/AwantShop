using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ws.eng.dll;
using ws.eng.obj;
using ws.web.eng.Models;
using System.Net.Mail;
using ws.com.v2017;

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
            Session["pagAtual"] = Paginas.Index;

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
                Session["_projeto"] = model;
                return RedirectToAction("Projeto");
            }
            else if (Request.Form["BT_BR"] == "")
            {
                model.Regiao = RegiaoProjeto.Brasil;
                Session["_projeto"] = model;
                return RedirectToAction("Projeto");
            }
            else if (Request.Form["BT_RJ"] == "")
            {
                model.Regiao = RegiaoProjeto.RioDeJaneiro;
                Session["_projeto"] = model;
                return RedirectToAction("Regiao");
            }


            return View(model);
        }

        public ActionResult Regiao()
        {
            Session["pagAtual"] = Paginas.Regiao;

            ProjetoModel model = (ProjetoModel)Session["_projeto"];

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
                Session["_projeto"] = model;
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

        public ActionResult Projeto()
        {
            Session["pagAtual"] = Paginas.Projeto;

            ProjetoModel model = (ProjetoModel)Session["_projeto"];

            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }

        [HttpPost]
        public ActionResult Projeto(ProjetoModel model)
        {
            if (model == null)
                return RedirectToAction("Index");

            model.Regiao = (RegiaoProjeto)int.Parse(Request.Form["idRegiao"].ToString());

            if (Request.Form["BT_R"] == "")
            {
                model.Projeto = TipoProjeto.Residencial;
                Session["_projeto"] = model;
                return RedirectToAction("Padrao");
            }
            else if (Request.Form["BT_C"] == "")
            {
                model.Projeto = TipoProjeto.Comercial;
                Session["_projeto"] = model;
                return RedirectToAction("Padrao");
            }
            else if (Request.Form["BT_I"] == "")
            {
                model.Projeto = TipoProjeto.Industrial;
                Session["_projeto"] = model;
                return RedirectToAction("Padrao");
            }
            else if (Request.Form["BT_PER"] == "Projeto Personalizado")
            {
                model.Projeto = TipoProjeto.Personalizado;
                Session["_projeto"] = model;
                return RedirectToAction("Personalizado");
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
            Session["pagAtual"] = Paginas.Area;

            ProjetoModel model = (ProjetoModel)Session["_projeto"];

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
            model.PadraoAcabamento = (PadraoProjeto)int.Parse(Request.Form["idPadrao"].ToString());

            if (Request.Form["BT_P"] == "")
            {
                model.Area = AreaProjeto.Pequeno;
                Session["_projeto"] = model;
                return RedirectToAction("Servicos");
            }
            else if (Request.Form["BT_M"] == "")
            {
                model.Area = AreaProjeto.Medio;
                Session["_projeto"] = model;
                return RedirectToAction("Servicos");
            }
            else if (Request.Form["BT_G"] == "")
            {
                model.Area = AreaProjeto.Grande;
                Session["_projeto"] = model;
                return RedirectToAction("Servicos");
            }
            else if (Request.Form["BT_PER"] == "Projeto Personalizado")
            {
                model.Area = AreaProjeto.Personalizado;
                Session["_projeto"] = model;
                return RedirectToAction("Personalizado");
            }
            else if (Request.Form["BT_VOLTAR"] == "voltar")
            {
                Session["_projeto"] = model;
                return RedirectToAction("Padrao");
            }

            return View(model);
        }

        public ActionResult Padrao()
        {
            Session["pagAtual"] = Paginas.Padrao;

            ProjetoModel model = (ProjetoModel)Session["_projeto"];

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
                Session["_projeto"] = model;
                return RedirectToAction("Area");
            }
            else if (Request.Form["BT_M"] == "")
            {
                model.PadraoAcabamento = PadraoProjeto.MedioCusto;
                Session["_projeto"] = model;
                return RedirectToAction("Area");
            }
            else if (Request.Form["BT_G"] == "")
            {
                model.PadraoAcabamento = PadraoProjeto.AltoCusto;
                Session["_projeto"] = model;
                return RedirectToAction("Area");
            }
            else if (Request.Form["BT_PER"] == "Projeto Personalizado")
            {
                model.PadraoAcabamento = PadraoProjeto.Personalizado;
                Session["_projeto"] = model;
                return RedirectToAction("Personalizado");
            }
            else if (Request.Form["BT_VOLTAR"] == "voltar")
            {
                Session["_projeto"] = model;
                return RedirectToAction("Projeto");
            }

            return View(model);
        }

        public ActionResult Servicos()
        {
            Session["pagAtual"] = Paginas.Servicos;

            ProjetoModel model = (ProjetoModel)Session["_projeto"];

            if (model == null)
                return RedirectToAction("Index");

            if (model.ServicosArquitetura == null)
                model.ServicosArquitetura = MontarServicos(model.Regiao, "A");

            if (model.ServicosEngenharia == null)
                model.ServicosEngenharia = MontarServicos(model.Regiao, "E");

            return View(model);
        }

        [HttpPost]
        public ActionResult Servicos(ProjetoModel model)
        {

            if (model == null)
                return RedirectToAction("Index");

            model.Id = (int)int.Parse(Request.Form["Id"].ToString());
            model.Regiao = (RegiaoProjeto)int.Parse(Request.Form["idRegiao"].ToString());
            model.Projeto = (TipoProjeto)int.Parse(Request.Form["idProjeto"].ToString());
            model.Area = (AreaProjeto)int.Parse(Request.Form["idArea"].ToString());
            model.PadraoAcabamento = (PadraoProjeto)int.Parse(Request.Form["idPadrao"].ToString());
            model.EnderecoCompletoEmpreendimento = Request.Form["EnderecoCompletoEmpreendimento"].ToString();
            model.TextoLivre = Request.Form["TextoLivre"].ToString();

            model.ServicosArquitetura = MontarServicos(model.Regiao, "A");
            model.ServicosEngenharia = MontarServicos(model.Regiao, "E");

            Session["_projeto"] = model;
            if (Request.Form["BT_CON"] == "Continuar")
            {
                return RedirectToAction("Resumo");
            }
            if (Request.Form["BT_PROX"] == "Continuar e fazer um novo cadastro")
            {
                return RedirectToAction("Formulario");
            }
            if (Request.Form["BT_LOG"] == "Continuar e fazer seu login")
            {
                Session["_orcamento"] = true;
                return RedirectToAction("Login", "Account");
            }
            else if (Request.Form["BT_PER"] == "Projeto Personalizado")
            {
                return RedirectToAction("Personalizado");
            }
            else if (Request.Form["BT_VOLTAR"] == "voltar")
            {
                return RedirectToAction("Area");
            }

            return View(model);
        }        

        public ActionResult ConfirmarCadastro()
        {
            ProjetoModel projeto = (ProjetoModel)Session["_projeto"];
            ConfirmarCadastroModel model = new ConfirmarCadastroModel();
            UsuarioObj usu = new UsuarioObj();
            UsuarioClienteDll usuCliDll = new UsuarioClienteDll();

            var cod = Request["cod"];
            var key = Request["key"];

            if (cod != null || key != null)
            {
                
                bool validar = usuCliDll.ValidarCodigoConfirmacao(string.Empty, cod, key);

                if (validar)
                {                
                  
                    usu = usuCliDll.BuscarUsuarioPorKey(key);

                    Session["usu"] = usu;                    

                    PopularProjectModel(usuCliDll.buscarUltimoProjetoPorChaveOuCpf(Guid.Parse(key)));

                    return RedirectToAction("Resumo");
                }
                else
                {
                    return RedirectToAction("Mensagem");
                }
            }
            else
            {
                //return RedirectToAction("Index");
                if (projeto == null)
                    ViewBag.DigitarCpf = true;
                else
                {
                    ViewBag.DigitarCpf = false;
                    try
                    {
                        SalvarProjeto(projeto);
                    }
                    catch (Exception ex)
                    {
                        //Enviar e-mail de erro
                        ViewBag.Message = "Infelizmente não foi possível registrar suas escolhas devido a um problema interno. Para refazer seu projeto clique no botão abaixo";
                        return RedirectToAction("Error");
                    }

                    //disparar e-mail para cliente aqui       
                    EmailDll email = new EmailDll(Server.MapPath(@"../Content")); 
                    Destinatario destinatario = new Destinatario(projeto.Nome, projeto.Email);

                    usu = usuCliDll.BuscarUsuario(projeto.CpfCnpj);

                    IDictionary<VariavelEmail, string> dados = new Dictionary<VariavelEmail, string>();
                    dados.Add(VariavelEmail.VAR_CODIGO, usu.CodigoValidacao);
                    dados.Add(VariavelEmail.VAR_LINK, @"http://http://www.wallacedamiao.com/avshop/Home/confirmarcadastro?cod=" + usu.CodigoValidacao + "&key=" + usu.Token);

                    email.EnviarEmailTemplate(TemplateEmail.ConfirmaCadastro, dados, destinatario);

                    model.Nome = projeto.Nome;
                    model.Email = projeto.Email;
                    model.Cpf_Cnpj = projeto.CpfCnpj;
                }                
                
                
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult ConfirmarCadastro(ConfirmarCadastroModel model)
        {
            UsuarioClienteDll usuCliDll = new UsuarioClienteDll();

            bool validar = usuCliDll.ValidarCodigoConfirmacao(model.Cpf_Cnpj, model.CodigoConfirmacao, string.Empty);

            if (validar)
            {
                if (Session["_projeto"] == null)
                {
                    PopularProjectModel(usuCliDll.buscarUltimoProjetoPorChaveOuCpf(new Guid(), model.Cpf_Cnpj));
                }
                else
                {
                    ProjetoModel pro = (ProjetoModel)Session["_projeto"];
                    pro.Id = usuCliDll.buscarUltimoIDProjetoPorChaveOuCpf(new Guid(),model.Cpf_Cnpj);
                    Session["_projeto"] = pro;
                }

                return RedirectToAction("Resumo");
            }
            else
            {
                ViewBag.Message = "O cõdigo informado é inválido";
                ViewBag.DigitarCpf = true;
                return View(model);
            }
        }

        public ActionResult Formulario()
        {
            Session["pagAtual"] = Paginas.Formulario;

            ProjetoModel model = (ProjetoModel)Session["_projeto"];

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
            model.ServicosArquitetura = MontarServicos(model.Regiao, "A");
            model.ServicosEngenharia = MontarServicos(model.Regiao, "E");

            Session["_projeto"] = model;
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
                    if(Session["usu"] == null)
                        return RedirectToAction("ConfirmarCadastro");
                    else
                        return RedirectToAction("resumo");
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
            Session["pagAtual"] = Paginas.Resumo;

            FinanceiroDll finDll = new FinanceiroDll();
            LogradouroDll logDll = new LogradouroDll();
            UsuarioClienteDll usuCliDll = new UsuarioClienteDll();

            ProjetoModel model = new ProjetoModel();

            model = (ProjetoModel)Session["_projeto"];

            if (model == null)
                return RedirectToAction("Index");

            if (model.CodMunicipio != null)
                model.CidadeID = logDll.BuscarMunicipio(model.CodMunicipio).ID;
            else
                model.CidadeID = 0;


            model.ValorMetroQuadradoAplicado = finDll.BuscarValorMetroQuadrado(model.Regiao);

            foreach (var item in model.ServicosArquitetura)
            {
                if (item.Selecionado)
                {
                    //passa valor true para o parametro de projeto arquitetonico
                    item.Valor = finDll.Calcular(model.Projeto,
                                                    model.Area,
                                                    model.Regiao,
                                                    model.PadraoAcabamento,
                                                    model.CidadeID,
                                                    true);

                    item.ValorFormatado = string.Format("{0}{1:N2}", usuCliDll.UnidadeMonetaria(model.Regiao), item.Valor);

                    model.ValorProjetoTotal += item.Valor;
                }
            }

            foreach (var item in model.ServicosEngenharia)
            {
                if (item.Selecionado)
                {
                    //passa valor true para o parametro de projeto de engenharia
                    item.Valor = finDll.Calcular(model.Projeto,
                                                    model.Area,
                                                    model.Regiao,
                                                    model.PadraoAcabamento,
                                                    model.CidadeID,
                                                    false);
                    item.ValorFormatado = string.Format("{0}{1:N2}", usuCliDll.UnidadeMonetaria(model.Regiao), item.Valor);

                    model.ValorProjetoTotal += item.Valor;
                }
            }

            model.ValorProjetoTotalFormatado = string.Format("{0}{1:N2}", usuCliDll.UnidadeMonetaria(model.Regiao), model.ValorProjetoTotal);

            if (Session["_orcamento"] != null)
            {
                UsuarioObj usu = (UsuarioObj)Session["usu"];
                ClienteObj cli = usuCliDll.BuscarCliente(usu.Cliente.ID);

                model.Nome = cli.Nome;
                model.CpfCnpj = cli.CPF_CNPJ;
                model.Email = cli.Email;
                model.ClienteCidade = cli.Cidade;
                model.ClienteEstado = cli.Estado;
                model.ClientePais = cli.Pais;
                model.TelContato1 = cli.TelContato1;
                model.TelContato2 = cli.TelContato2;
                model.TelContato3 = cli.TelContato3;
            }

            Session["_projeto"] = model;

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

            model.Id = (int)int.Parse(Request.Form["Id"].ToString());
            model.Regiao = (RegiaoProjeto)int.Parse(Request.Form["idRegiao"].ToString());
            model.Projeto = (TipoProjeto)int.Parse(Request.Form["idProjeto"].ToString());
            model.Area = (AreaProjeto)int.Parse(Request.Form["idArea"].ToString());
            model.PadraoAcabamento = (PadraoProjeto)int.Parse(Request.Form["idPadrao"].ToString());

            model.CpfCnpj = Request.Form["CpfCnpj"].ToString();
            model.Nome = Request.Form["Nome"].ToString();
            model.TelContato1 = Request.Form["TelContato1"].ToString();
            model.TelContato2 = Request.Form["TelContato2"].ToString();
            model.TelContato3 = Request.Form["TelContato3"].ToString();
            model.TextoLivre = Request.Form["TextoLivre"].ToString();
            model.CidadeID = int.Parse(Request.Form["CidadeID"].ToString());
            model.Email = Request.Form["Email"].ToString();
            model.ClienteCidade = Request.Form["ClienteCidade"].ToString();
            model.ClienteEstado = Request.Form["ClienteEstado"].ToString();
            model.ClientePais = Request.Form["ClientePais"].ToString();
            model.EnderecoCompletoEmpreendimento = Request.Form["EnderecoCompletoEmpreendimento"].ToString();
            model.ServicosArquitetura = MontarServicos(model.Regiao, "A");
            model.ServicosEngenharia = MontarServicos(model.Regiao, "E");


            if (model == null)
                return RedirectToAction("Index");

            if (Request.Form["BT_PROX"] == "Enviar seu Projeto")
            {
                SalvarProjeto(model);
                return RedirectToAction("Financeiro");
            }
            else if (Request.Form["BT_VOLTAR"] == "voltar")
            {
                if (Session["usu"] == null)
                    return RedirectToAction("Formulario");
                else
                    return RedirectToAction("Servicos");
            }

            return View(model);
        }

        public ActionResult Personalizado()
        {
            Session["pagAtual"] = Paginas.Personalizado;

            ProjetoModel model = (ProjetoModel)TempData["_projeto"];

            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }

        [HttpPost]
        public ActionResult Personalizado(ProjetoModel model)
        {
            if (model == null)
                return RedirectToAction("Index");


            if (Request.Form["BT_PROX"] == "Continuar")
            {
                TempData["Novo"] = true;
                return RedirectToAction("Login", "Account");
            }
            else if (Request.Form["BT_VOLTAR"] == "voltar")
            {
                return RedirectToAction("Index");
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
                catch (Exception ex)
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

        #region Métodos Auxiliares

        private List<SelectListItem> PopularCidades(string SiglaUF)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            foreach (var item in new LogradouroDll().BuscarMunicipiosPorSiglaUF("RJ"))
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

        public List<ServicoAux> MontarServicos(RegiaoProjeto regiao, string tipo, List<ProjetoServicoObj> servicos = null)
        {
            ServicoDll srvDll = new ServicoDll();
            List<ServicoObj> listaIn = new List<ServicoObj>();
            List<ServicoAux> listaOut = new List<ServicoAux>();

            int idPais = 0;
            int idRegiao = 0;

            if ((int)regiao != 3)
            {
                idPais = (int)regiao;
            }
            else
            {
                idPais = 1;
            }

            if (tipo == "A")
                listaIn = srvDll.ListarServicoArquitetura(idPais, idRegiao);
            else
                listaIn = srvDll.ListarServicoEngenharia(idPais, idRegiao);

            foreach (var item in listaIn)
            {
                ServicoAux obj = new ServicoAux();

                obj.ServicoID = item.ID;
                obj.ProjetoServicoID = 0;
                obj.Nome = item.Nome;
                if (tipo == "A")
                    obj.Codigo = "ARQID" + item.ID.ToString();
                else
                    obj.Codigo = "ENGID" + item.ID.ToString();

                listaOut.Add(obj);
            }

            if (tipo == "A")
            {
                if (Request.Form["ARQID"] != null)
                {
                    foreach (var item in listaOut)
                    {
                        if (Request.Form["ARQID"] == item.ServicoID.ToString())
                            item.Selecionado = true;
                    }
                }
                else
                {
                    if (servicos != null)
                    {
                        foreach (var item in listaOut)
                        {                            
                            item.Selecionado = (from s in servicos where s.ServicoID == item.ServicoID && s.Servico.Tipo.ID == 1 select s).Count() > 0;
                        }
                    }

                }


            }
            else
            {
                foreach (var item in listaOut)
                {
                    if (Request.Form[item.Codigo.ToString()] != null)
                    {
                        if (Request.Form[item.Codigo.ToString()].ToString() == "true,false")
                            item.Selecionado = true;
                    }
                }

                if (servicos != null)
                {
                    if (servicos != null)
                    {
                        foreach (var item in listaOut)
                        {                            
                            item.Selecionado = (from s in servicos where s.ServicoID == item.ServicoID && s.Servico.Tipo.ID == 2 select s).Count() > 0;
                        }
                    }

                }
            }

            return listaOut;
        }

        private void SalvarProjeto(ProjetoModel model)
        {
            UsuarioClienteDll cliDll = new UsuarioClienteDll();
            FinanceiroDll finDll = new FinanceiroDll();

            UsuarioObj usu = new UsuarioObj();
            ClienteObj cli = new ClienteObj();
            ProjetoObj pro = new ProjetoObj();


            DateTime dataCad = DateTime.Now;


            //popularUsuario
            usu.NomeUsuario = model.CpfCnpj;
            usu.CategoriaID = (int)CategoriaUsuario.Cliente;
            usu.NomeCompleto = model.Nome;

            cliDll.CriarUsuario(usu, true);

            cli.Email = model.Email;
            cli.CPF_CNPJ = model.CpfCnpj;
            cli.Nome = model.Nome;
            cli.TelContato1 = model.TelContato1;
            cli.TelContato2 = model.TelContato2;
            cli.TelContato3 = model.TelContato3;
            cli.Pais = model.ClientePais;
            cli.Estado = model.ClienteEstado;
            cli.Cidade = model.ClienteCidade;

            cliDll.SalvarCliente(cli);

            cli = cliDll.BuscarCliente(cli.CPF_CNPJ);

            pro.ID = model.Id;
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
            pro.EnderecoCompletoEmpreendimento = model.EnderecoCompletoEmpreendimento;

            usu = cliDll.BuscarUsuario(cli.CPF_CNPJ);

            pro.Servicos = new List<ProjetoServicoObj>();

            foreach (var item in model.ServicosArquitetura)
            {
                if (item.Selecionado)
                {
                    ProjetoServicoObj ser = new ProjetoServicoObj();

                    ser.ID = item.ProjetoServicoID;
                    ser.ServicoID = (int)ServicosProjeto.ProjetoArquitetonico;
                    ser.DataCad = dataCad;
                    ser.FormaPagamento = FormaPgto.Boleto;
                    ser.Status = (int)StatusServico.Criado;
                    ser.Usuario = usu;
                    ser.UsuarioID = usu.ID;
                    ser.Descricao = item.Nome;
                    ser.Valor = item.Valor;

                    finDll.Calcular(model.Projeto,
                                    model.Area,
                                    model.Regiao,
                                    model.PadraoAcabamento,
                                    model.CidadeID,
                                    true);

                    ser.T = finDll.T;
                    ser.P = finDll.P;
                    ser.A = finDll.A;
                    ser.d = finDll.D;

                    pro.Servicos.Add(ser);
                }

            }

            foreach (var item in model.ServicosEngenharia)
            {
                if (item.Selecionado)
                {
                    ProjetoServicoObj ser = new ProjetoServicoObj();

                    ser.ID = item.ProjetoServicoID;
                    ser.ServicoID = item.ServicoID;
                    ser.DataCad = dataCad;
                    ser.FormaPagamento = FormaPgto.Boleto;
                    ser.Status = (int)StatusServico.Criado;
                    ser.Usuario = usu;
                    ser.UsuarioID = usu.ID;
                    ser.Descricao = item.Nome;
                    ser.Valor = item.Valor;

                    finDll.Calcular(model.Projeto,
                                    model.Area,
                                    model.Regiao,
                                    model.PadraoAcabamento,
                                    model.CidadeID,
                                    false);

                    ser.T = finDll.T;
                    ser.P = finDll.P;
                    ser.A = finDll.A;
                    ser.d = finDll.D;

                    pro.Servicos.Add(ser);
                }

            }

            cliDll.SalvarProjeto(pro);
        }

        private ProjetoModel PopularProjectModel(ProjetoObj obj)
        {
            ProjetoModel proj = new ProjetoModel();

            proj.Id = obj.ID;
            proj.Area = obj.Area;
            proj.PadraoAcabamento = obj.PadraoAcabamento;
            proj.Projeto = obj.Projeto;
            proj.Regiao = obj.Regiao;

            proj.ServicosArquitetura = MontarServicos(proj.Regiao, "A", obj.Servicos);
            proj.ServicosEngenharia = MontarServicos(proj.Regiao, "E", obj.Servicos);

            proj.AreaPersonalizada = obj.AreaPersonalizada.Value;
            proj.CidadeID = obj.ClienteID;
            proj.ClienteCidade = obj.Cliente.Cidade;
            proj.ClienteEstado = obj.Cliente.Estado;
            proj.ClientePais = obj.Cliente.Pais;
            proj.CpfCnpj = obj.Cliente.CPF_CNPJ;
            proj.Email = obj.Cliente.Email;
            proj.EnderecoCompletoEmpreendimento = obj.EnderecoCompletoEmpreendimento;
            proj.Nome = obj.Cliente.Nome;
            proj.TelContato1 = obj.Cliente.TelContato1;
            proj.TelContato2 = obj.Cliente.TelContato2;
            proj.TelContato3 = obj.Cliente.TelContato3;
            proj.TextoLivre = obj.TextoLivre;

            Session["_projeto"] = proj;

            return proj;
        }

        #endregion
    }
}