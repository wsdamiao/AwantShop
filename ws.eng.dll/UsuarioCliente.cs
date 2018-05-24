using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ws.eng.dao;
using ws.eng.obj;
using Microsoft;
using ws.com.v2017;

namespace ws.eng.dll
{
    public class UsuarioClienteDll
    {
        UsuarioClienteDao cliDao;

        public UsuarioClienteDll()
        {
            cliDao = new UsuarioClienteDao();
        }

        
        #region Usuario

        public void CriarUsuario(UsuarioObj obj, bool gerarSenhaAutomatica)
        {
            UsuarioObj objEncontrado;
            Criptografia criptografia = new Criptografia(CryptProvider.TripleDES);

            string senha = obj.Senha;

            criptografia.Key = ws.eng.obj.DadosProjeto.CRIPTOGRAFIA;

            objEncontrado = cliDao.ListarUsuario().Where(x => x.NomeUsuario == obj.NomeUsuario).FirstOrDefault();

            if (objEncontrado == null)
            {
                if (gerarSenhaAutomatica)
                    senha = criptografia.Encrypt(Gerador.alfanumericoAleatorio(DadosProjeto.TAMANHOSENHAPADRAO));
                
                obj.Senha = senha;
                obj.Ativo = false;
                obj.Token = Guid.NewGuid();

                cliDao.SalvarUsuario(obj);
            }

        }

        public void AlterarUsuario(UsuarioObj obj, bool alterarSenha, bool gerarSenhaAutomatica = false)
        {
            if (alterarSenha)
            {
                Criptografia cript = new Criptografia(CryptProvider.TripleDES);
                cript.Key = ws.eng.obj.DadosProjeto.CRIPTOGRAFIA;

                if (gerarSenhaAutomatica)
                    obj.Senha = cript.Encrypt(Gerador.alfanumericoAleatorio(DadosProjeto.TAMANHOSENHAPADRAO));
                else
                    obj.Senha = cript.Encrypt(obj.Senha);
            }

            cliDao.SalvarUsuario(obj);
        }

        public UsuarioObj BuscarUsuario(int ID)
        {
            Criptografia cript = new Criptografia(CryptProvider.TripleDES);
            cript.Key = ws.eng.obj.DadosProjeto.CRIPTOGRAFIA;

            UsuarioObj obj = cliDao.ListarUsuario().Where(x => x.ID == ID).FirstOrDefault();

            obj.Senha = cript.Decrypt(obj.Senha);

            return obj;
        }

        public List<UsuarioObj> ListarUsuario()
        {
            return cliDao.ListarUsuario();
        }

        public UsuarioObj BuscarUsuario(string cpfCnpj)
        {
            Criptografia cript = new Criptografia(CryptProvider.TripleDES);
            cript.Key = ws.eng.obj.DadosProjeto.CRIPTOGRAFIA;

            UsuarioObj obj = cliDao.ListarUsuario().Where(x => x.NomeUsuario == cpfCnpj).FirstOrDefault();

            obj.Senha = cript.Decrypt(obj.Senha);

            return obj;
            
        }

        public UsuarioObj  ValidarAcesso(string login, string senha, bool reEnvio)
        {
            UsuarioObj usu;
            
            try
            {
                usu = BuscarUsuario(login);
                usu.Cliente = BuscarCliente(usu.NomeUsuario);

                if (usu.Senha == senha && !reEnvio)
                    return usu;
                else if (reEnvio)
                {
                    Email mail = new Email();
                    mail.RecuperarSenha(usu);
                }
                                                  
                return new UsuarioObj();

            }
            catch( Exception ex)
            {
                new LogDeErro(ex);           
            }

            return new UsuarioObj();
        }

        public List<UsuarioObj> ListarUsuarioAdministrativo()
        {            
            try
            {
                return ListarUsuario().Where(x => x.CategoriaID != (int)CategoriaUsuario.Cliente).ToList();
            }
            catch (Exception ex)
            {
                new LogDeErro(ex);
            }

            return new List<UsuarioObj>();
        }

        public List<UsuarioObj> ListarUsuarioCliente()
        {
            try
            {
                return ListarUsuario().Where(x => x.CategoriaID == (int)CategoriaUsuario.Cliente).ToList();
            }
            catch (Exception ex)
            {
                new LogDeErro(ex);
            }

            return new List<UsuarioObj>();
        }

        #endregion

        #region Cliente

        public ClienteObj BuscarCliente(int ID)
        {
            return cliDao.ListarCliente().Where(x => x.ID == ID).FirstOrDefault();
        }

        public ClienteObj BuscarCliente(string cpfCnpj)
        {
            return cliDao.ListarCliente().Where(x => x.CPF_CNPJ == cpfCnpj).FirstOrDefault();
        }

        public ClienteObj BuscarClientePorEmail(string email)
        {
            return cliDao.ListarCliente().Where(x => x.Email == email).FirstOrDefault();
        }

        #endregion

        #region Projeto

        public void SalvarCliente(ClienteObj obj)
        {
            UsuarioObj usu;
            ClienteObj objEncontrado;

            objEncontrado = cliDao.ListarCliente().Where(x => x.CPF_CNPJ == obj.CPF_CNPJ).FirstOrDefault();

            if (objEncontrado == null)
            {
                usu = cliDao.ListarUsuario().Where(x => x.NomeUsuario == obj.CPF_CNPJ).FirstOrDefault();
                obj.UsuarioID = usu.ID;

                cliDao.SalvarCliente(obj);
            }
        }

        public bool BuscarProjetoSimilar(ProjetoObj obj)
        {
            ProjetoObj objEncontrado;

            objEncontrado = cliDao.ListarProjeto().Where(x => x.Area == obj.Area &&
            x.PadraoAcabamento == obj.PadraoAcabamento &&
            x.Projeto == obj.Projeto &&
            x.logradouroID == obj.logradouroID &&
            x.AreaPersonalizada == obj.AreaPersonalizada).FirstOrDefault();

            if (objEncontrado == null)
                return false;
            else
                return true;
        }

        public void SalvarProjeto(ProjetoObj obj)
        {
            cliDao.SalvarProjeto(obj);
        }

        public ICollection<ProjetoObj> ListarProjeto()
        {
            ICollection<ProjetoObj> pro = cliDao.ListarProjeto(); 

            foreach(var item in pro)
            {
                item.Cliente = cliDao.ListarCliente().Where(x => x.ID == item.ClienteID).FirstOrDefault();
                item.Servicos = cliDao.ListarProjetoServico().Where(x => x.ProjetoID == item.ID).ToList();
            }

            return pro;
        }

        public ICollection<ProjetoObj> ListarProjeto(int IdCliente)
        {
            return ListarProjeto().Where(x => x.ClienteID == IdCliente).ToList();
        }

        public string CalculaValorTotalServicos(List<ProjetoServicoObj> servicos, RegiaoProjeto regiao)
        {
            decimal total = 0;
            string moeda = UnidadeMonetaria(regiao);
                        
            if (servicos != null)
            {
                foreach (var item in servicos)
                {
                    total += item.Valor;
                }
            }

            return moeda + string.Format("{0:N2}", total);
            
        }

        public string UnidadeMonetaria(RegiaoProjeto regiao)
        {
            string moeda = "";

            switch (regiao)
            {
                case RegiaoProjeto.Brasil:
                    moeda = "R$ ";
                    break;
                case RegiaoProjeto.RioDeJaneiro:
                    moeda = "R$ ";
                    break;
                case RegiaoProjeto.Portugal:
                    moeda = "EU$ ";
                    break;
            }

            return moeda;
        }

        public string StatusProjetos(List<ProjetoServicoObj> servicos)
        {
            StatusServico menorStatus =  StatusServico.RevisãoFinalizada;
            if (servicos != null)
            {
                foreach (var item in servicos)
                {
                    if (item.Status < (int)menorStatus)
                        menorStatus = (StatusServico)item.Status;
                }

                return menorStatus.ToString();
            }

            return "Sem Status";
        }



        #endregion

        #region Categoria
        public List<UsuarioCategoriaObj> ListarCategoria()
        {
            return cliDao.ListarUsuarioCategoria();
        }
        #endregion
    }
}
