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
            string codValidacao = Gerador.Numero(4);

            criptografia.Key = ws.eng.obj.DadosProjeto.CRIPTOGRAFIA;

            objEncontrado = cliDao.ListarUsuario().Where(x => x.NomeUsuario == obj.NomeUsuario).FirstOrDefault();

            if (objEncontrado == null)
            {
                if (gerarSenhaAutomatica)
                    senha = criptografia.Encrypt(Gerador.alfanumericoAleatorio(DadosProjeto.TAMANHOSENHAPADRAO));

                obj.Senha = senha;
                obj.Ativo = false;
                obj.Token = Guid.NewGuid();
                obj.CodigoValidacao = codValidacao;

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

        public UsuarioObj BuscarUsuarioPorKey(string key)
        {
            Criptografia cript = new Criptografia(CryptProvider.TripleDES);
            cript.Key = ws.eng.obj.DadosProjeto.CRIPTOGRAFIA;

            UsuarioObj obj = cliDao.ListarUsuario().Where(x => x.Token == Guid.Parse(key)).FirstOrDefault();

            obj.Senha = cript.Decrypt(obj.Senha);

            return obj;
        }

        public UsuarioObj ValidarAcesso(string login, string senha, bool reEnvio)
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
                    //mail.RecuperarSenha(usu);
                }

                return new UsuarioObj();

            }
            catch (Exception ex)
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

        public bool ValidarCodigoConfirmacao(string cpf_cnpj, string codigo, string key)
        {
            UsuarioObj usu = new UsuarioObj();

            if (cpf_cnpj != string.Empty)
                usu = BuscarUsuario(cpf_cnpj);
            else
            {
                usu = BuscarUsuarioPorKey(key);
                cpf_cnpj = usu.NomeUsuario;
            }

            if (usu.NomeUsuario == cpf_cnpj)
            {
                if (usu.CodigoValidacao == codigo)
                {
                    cliDao.AtivarUsuario(usu.ID);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public List<ClienteObj> Listar()
        {
            return cliDao.ListarCliente();
        }

        public List<ClienteObj> Listar(int qtd)
        {
            return cliDao.ListarCliente().OrderByDescending(x => x.ID).Take(qtd).ToList();
        }

        public List<ClienteObj> ListarPor(string tipo, string valor)
        {
            List<ClienteObj> resultado = new List<ClienteObj>();

            switch (tipo)
            {
                case "C":
                    resultado = cliDao.ListarCliente().Where(x => x.CPF_CNPJ == valor).OrderBy(x => x.Nome).ToList();
                    break;
                case "N":
                    resultado = cliDao.ListarCliente().Where(x => x.Nome.Contains(valor)).OrderBy(x=> x.Nome).ToList();
                    break;
                default:
                    resultado = new List<ClienteObj>();
                    break;
            }

            return resultado;
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

        public int buscarUltimoIDProjetoPorChaveOuCpf(Guid key, string cpf = "")
        {
            if (cpf == string.Empty)
                cpf = ListarUsuario().Where(x => x.Token == key).FirstOrDefault().NomeUsuario;

            var idCli = BuscarCliente(cpf).ID;

            var proj = ListarProjeto(idCli).LastOrDefault();

            return proj.ID;
        }

        public ProjetoObj buscarUltimoProjetoPorChaveOuCpf(Guid key, string cpf = "")
        {
            if(cpf == string.Empty)
                cpf = ListarUsuario().Where(x => x.Token == key).FirstOrDefault().NomeUsuario;

            var idCli = BuscarCliente(cpf).ID;

            var proj = ListarProjeto(idCli).LastOrDefault();

            return proj;
        }

        public List<ProjetoServicoObj> FiltrarServicosPorTipo(List<ProjetoServicoObj> obj, int tipoServico)
        {
            return new List<ProjetoServicoObj>();
        }

        public void SalvarProjeto(ProjetoObj obj)
        {
            cliDao.SalvarProjeto(obj);
        }

        public ICollection<ProjetoObj> ListarProjeto()
        {
            ICollection<ProjetoObj> pro = cliDao.ListarProjeto();
            
            return pro;
        }

        public ICollection<ProjetoObj> ListarProjeto(int IdCliente)
        {
            return cliDao.ListarProjetoPorCliente(IdCliente);
        }
        
        //public string CalculaValorTotalServicos(List<ProjetoServicoObj> servicos, RegiaoProjeto regiao)
        //{
        //    decimal total = 0;
        //    string moeda = UnidadeMonetaria(regiao);

        //    if (servicos != null)
        //    {
        //        foreach (var item in servicos)
        //        {
        //            total += item.Valor;
        //        }
        //    }

        //    return moeda + string.Format("{0:N2}", total);

        //}

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
            StatusServico menorStatus = StatusServico.RevisãoFinalizada;
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
