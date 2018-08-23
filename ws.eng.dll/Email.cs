using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ws.eng.obj;
using ws.com.v2017;
using System.IO;
using System.Net.Mail;

namespace ws.eng.dll
{
    public class EmailDll
    {
        Email mail;
        private string caminho;

        public EmailDll()
        {
            mail = new Email();
        }

        public EmailDll(string _caminhoTemplate)
        {
            caminho = _caminhoTemplate;
            mail = new Email();
        }

        public bool RecuperarSenha(UsuarioObj usu)
        {
            return true;
        }

        public void EnviarEmailTemplate(TemplateEmail template,IDictionary<VariavelEmail,string> dados, Destinatario destinatario)
        {
            string corpoEmail = "";

            switch (template)
            {
                case TemplateEmail.ConfirmaCadastro:
                    corpoEmail = CorpoEmailConfirmarCadastro(dados);
                    break;
            }

            mail.CorpoHTML = true;
            mail.EmailServico = "sistemas@wallacedamiao.com";
            mail.SenhaEmailServico = "senha@senha@123";
            mail.Servidor = "localhost";
            mail.Ssl = false;
            mail.Porta = 25;
            mail.Mensagem = corpoEmail;
            mail.Prioridade = MailPriority.High;
            mail.Titulo = "Confirmação de e-mail - Awwant Shop ";

            mail.Destinatarios = new List<Destinatario>();
            mail.Destinatarios.Add(destinatario);

            try
            {
                mail.MontarMensagem();
                mail.Enviar();

                //TempData["Message"] = "(S)Sua mensagem foi enviada. Em breve retornarei seu contato";
            }
            catch (Exception ex)
            {
                //TempData["Message"] = "(E)No momento sua mensgaem não pode ser enviada por este formulário. Por favor, faça-nos uma ligação ou envie uma mensagem diretemente para o e-mail informado acima. Obrigado.";
                //TempData["Message"] = ex.Message + " - " + ex.InnerException + " - " + ex.StackTrace;
            }
        }

        private string CorpoEmailConfirmarCadastro(IDictionary<VariavelEmail, string> dados)
        {
            string corpoEmail = File.ReadAllText(caminho + @"/Template/Email/ConfirmarCadastro.html");
            foreach(var val in dados)
            {
                corpoEmail = corpoEmail.Replace(val.Key.ToString(), val.Value.ToString());
            }

            return corpoEmail;
        }
    }

    public enum TemplateEmail
    {
        RecuperarSenha,
        ConfirmaCadastro,
        SemTemplate
    }

    public enum VariavelEmail
    {
        VAR_CODIGO,
        VAR_LINK
    }
}
