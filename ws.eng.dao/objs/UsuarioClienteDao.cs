using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ws.eng.obj;
using ws.com.v2017;

namespace ws.eng.dao
{
    public class UsuarioClienteDao
    {
        ProEngEntities ProEng;

        public UsuarioClienteDao()
        {
            ProEng = new ProEngEntities();
        }

        #region Infra
        private UsuarioObj ConverterObj(Usuario obj)
        {
            UsuarioObj objOut = new UsuarioObj();
            
            objOut.ID = obj.ID;
            objOut.Ativo = obj.Ativo;
            objOut.CategoriaID = obj.CategoriaID;
            objOut.NomeUsuario = obj.NomeUsuario;
            objOut.NomeCompleto = obj.NomeCompleto;
            objOut.Senha = obj.Senha;
            objOut.Token = obj.Token;
            objOut.Categoria = this.ListarUsuarioCategoria().Where(x => x.ID == obj.CategoriaID).FirstOrDefault();

            return objOut;
        }

        private Usuario ConverterObj(UsuarioObj obj)
        {
            Usuario objOut = new Usuario();

            objOut.ID = obj.ID;
            objOut.Ativo = obj.Ativo;
            objOut.CategoriaID = obj.CategoriaID;
            objOut.NomeUsuario = obj.NomeUsuario;
            objOut.NomeCompleto = obj.NomeCompleto;
            objOut.Senha = obj.Senha;
            objOut.Token = obj.Token;

            return objOut;
        }

        private ClienteObj ConverterObj(Cliente obj)
        {
            ClienteObj objOut = new ClienteObj();

            objOut.ID = obj.ID;
            objOut.Nome = obj.Nome;
            objOut.CPF_CNPJ = obj.CpfCnpj;
            objOut.Email = obj.Email;
            objOut.TelContato1 = obj.TelContato1;
            objOut.TelContato2 = obj.TelContato2;
            objOut.TelContato3 = obj.TelContato3;
            objOut.UsuarioID = obj.UsuarioID.Value;
            objOut.Usuario = ListarUsuario().Where(x=> x.ID == objOut.UsuarioID).FirstOrDefault();

            //objOut.Endereco = new Endereco();
            //objOut.Endereco.Rua = obj.Logradouro;
            //objOut.Endereco.CEP = obj.CodPostal;

            return objOut;
        }

        private Cliente ConverterObj(ClienteObj obj)
        {
            Cliente objOut = new Cliente();

            objOut.ID = obj.ID;
            objOut.Nome = obj.Nome;
            objOut.CpfCnpj = obj.CPF_CNPJ;
            objOut.Email = obj.Email;
            objOut.TelContato1 = obj.TelContato1;
            objOut.TelContato2 = obj.TelContato2;
            objOut.TelContato3 = obj.TelContato3;
            objOut.UsuarioID = obj.UsuarioID;

            //objOut.Endereco = new Endereco();
            //objOut.Endereco.Rua = obj.Logradouro;
            //objOut.Endereco.CEP = obj.CodPostal;

            return objOut;
        }

        private ProjetoObj ConverterObj(Projeto obj)
        {
            ProjetoObj objOut = new ProjetoObj();

            objOut.ID = obj.ID;
            objOut.Area = (AreaProjeto)obj.AreaID;
            objOut.ClienteID = obj.ClienteID;
            objOut.Cliente = ConverterObj(obj.Cliente);
            objOut.logradouroID = obj.LogradouroID;
            objOut.PadraoAcabamento = (PadraoProjeto)obj.PadraoID;
            objOut.Projeto = (TipoProjeto)obj.ProjetoID;
            objOut.Regiao = (RegiaoProjeto)obj.PaisID;
            objOut.AreaPersonalizada = obj.AreaPersonalizada;
            objOut.ValorMetroQuadradoAplicado = obj.VlMetroQuadradoBase;            
            objOut.DataCad = obj.DataCad.Value;
            objOut.TextoLivre = obj.TextoLivre;

            return objOut;
        }

        private Projeto ConverterObj(ProjetoObj obj)
        {
            Projeto objOut = new Projeto();

            objOut.ID = obj.ID;
            objOut.AreaID = (int)obj.Area;
            objOut.ClienteID = obj.ClienteID;
            //objOut.Cliente = ConverterObj(obj.Cliente);
            objOut.LogradouroID = obj.logradouroID;
            objOut.PadraoID = (int)obj.PadraoAcabamento;
            objOut.ProjetoID = (int)obj.Projeto;
            objOut.PaisID = (int)obj.Regiao;
            objOut.AreaPersonalizada = obj.AreaPersonalizada;
            objOut.VlMetroQuadradoBase = obj.ValorMetroQuadradoAplicado;            
            objOut.DataCad = obj.DataCad;
            objOut.TextoLivre = obj.TextoLivre;

            return objOut;
        }

        private ProjetoServicoObj ConverterObj(ProjetoServico obj)
        {
            ProjetoServicoObj objOut = new ProjetoServicoObj();

            objOut.ID = obj.ID;
            objOut.DataCad = obj.Data;
            objOut.Descricao = obj.Descricao;
            objOut.FormaPagamento = (FormaPgto)obj.FormaPgto.Value;
            objOut.Projeto = ConverterObj(obj.Projeto);
            objOut.ProjetoID = obj.ProjetoID;
            objOut.ServicoID = obj.ServicoID;
            objOut.Status = obj.Status;
            objOut.UsuarioID = obj.UsuarioID;
            objOut.Valor = obj.Valor;
            objOut.T = obj.T;
            objOut.A = obj.A;
            objOut.P = obj.P;
            objOut.d = obj.d;

            return objOut;
        }

        private ProjetoServico ConverterObj(ProjetoServicoObj obj)
        {
            ProjetoServico objOut = new ProjetoServico();

            objOut.ID = obj.ID;
            objOut.Data = obj.DataCad;
            objOut.Descricao = obj.Descricao;
            objOut.FormaPgto = (int)obj.FormaPagamento;            
            objOut.ProjetoID = obj.ProjetoID;
            objOut.ServicoID = obj.ServicoID;
            objOut.Status = obj.Status;
            objOut.UsuarioID = obj.UsuarioID;
            objOut.Valor = obj.Valor;

            return objOut;
        }

        private UsuarioCategoria ConverterObj(UsuarioCategoriaObj obj)
        {
            UsuarioCategoria objOut = new UsuarioCategoria();

            objOut.ID = obj.ID;
            objOut.Ativo = obj.Ativo;
            objOut.Descr = obj.Descr;
            objOut.Nome = obj.Nome;
            
            return objOut;
        }

        private UsuarioCategoriaObj ConverterObj(UsuarioCategoria obj)
        {
            UsuarioCategoriaObj objOut = new UsuarioCategoriaObj();

            objOut.ID = obj.ID;
            objOut.Ativo = obj.Ativo;
            objOut.Descr = obj.Descr;
            objOut.Nome = obj.Nome;

            return objOut;
        }

        private List<UsuarioObj> PopularListaObj(List<Usuario> obj)
        {
            List<UsuarioObj> objOut = new List<UsuarioObj>();
            foreach (var item in obj)
            {
                objOut.Add(ConverterObj(item));
            }
            return objOut;
        }

        private List<ClienteObj> PopularListaObj(List<Cliente> obj)
        {
            List<ClienteObj> objOut = new List<ClienteObj>();
            foreach (var item in obj)
            {
                objOut.Add(ConverterObj(item));
            }
            return objOut;
        }

        private List<ProjetoServicoObj> PopularListaObj(List<ProjetoServico> obj)
        {
            List<ProjetoServicoObj> objOut = new List<ProjetoServicoObj>();
            foreach (var item in obj)
            {
                objOut.Add(ConverterObj(item));
            }
            return objOut;
        }

        private List<ProjetoObj> PopularListaObj(List<Projeto> obj)
        {
            List<ProjetoObj> objOut = new List<ProjetoObj>();
            foreach (var item in obj)
            {
                objOut.Add(ConverterObj(item));
            }
            return objOut;
        }

        private List<UsuarioCategoriaObj> PopularListaObj(List<UsuarioCategoria> obj)
        {
            List<UsuarioCategoriaObj> objOut = new List<UsuarioCategoriaObj>();
            foreach (var item in obj)
            {
                objOut.Add(ConverterObj(item));
            }
            return objOut;
        }
        #endregion

        public List<UsuarioObj> ListarUsuario()
        {
            return PopularListaObj(ProEng.Usuarios.ToList());
        }

        public List<ClienteObj> ListarCliente()
        {
            return PopularListaObj(ProEng.Clientes.ToList());
        }

        public List<ProjetoObj> ListarProjeto()
        {
            return PopularListaObj(ProEng.Projetoes.ToList());
        }

        public List<ProjetoServicoObj> ListarProjetoServico()
        {
            return PopularListaObj(ProEng.ProjetoServicoes.ToList());
        }

        public List<UsuarioCategoriaObj> ListarUsuarioCategoria()
        {
            return PopularListaObj(ProEng.UsuarioCategorias.ToList());
        }

        public void SalvarCliente(ClienteObj obj)
        {
            ProEng.Clientes.Add(ConverterObj(obj));
            ProEng.SaveChanges();
        }

        public void SalvarProjetoServico(ProjetoServicoObj obj)
        {
            if (obj.ID == 0)
                ProEng.ProjetoServicoes.Add(ConverterObj(obj));
            else
                ProEng.ProjetoServicoes.Attach(ConverterObj(obj));

            ProEng.SaveChanges();
        }

        public void SalvarUsuario(UsuarioObj obj)
        {
            if (obj.ID == 0)
            {
                ProEng.Usuarios.Add(ConverterObj(obj));                
            }
            else
            {
                var aux = ProEng.Usuarios.Find(obj.ID);

                aux.NomeCompleto = obj.NomeCompleto;
                aux.NomeUsuario = obj.NomeUsuario;
                aux.Senha = obj.Senha;
                aux.Ativo = obj.Ativo;
                aux.CategoriaID = obj.CategoriaID;
                
                ProEng.Entry(aux).State = System.Data.Entity.EntityState.Modified;
                
            }

            ProEng.SaveChanges();
        }

        public void SalvarProjeto(ProjetoObj obj)
        {
            Projeto objIn = ConverterObj(obj);

            using (var dbContextTransaction = ProEng.Database.BeginTransaction())
            {
                try
                {
                    if (obj.ID == 0)
                    {
                        ProEng.Projetoes.Add(objIn);
                        ProEng.SaveChanges();
                        
                        foreach (var item in obj.Servicos)
                        {
                            item.ProjetoID = objIn.ID;                            
                            ProEng.ProjetoServicoes.Add(ConverterObj(item));
                            ProEng.SaveChanges();
                        }
                    }
                    else
                    {
                        ProEng.Entry(obj).State = System.Data.Entity.EntityState.Modified;

                        foreach (var item in obj.Servicos)
                        {
                            ProEng.Entry(item).State = System.Data.Entity.EntityState.Modified;
                        }
                    }
                   
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }
        }

        public void ExcluirCliente(ClienteObj obj)
        {
            ProEng.Entry(obj).State = System.Data.Entity.EntityState.Deleted;
            //ProEng.Clientes.Remove(ConverterObj(obj));
            ProEng.SaveChanges();
        }

        public void ExcluirProjetoServico(ProjetoServicoObj obj)
        {
            ProEng.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            //.ProjetoServicoes.Remove(ConverterObj(obj));
            ProEng.SaveChanges();
        }

        public void ExcluirUsuario(UsuarioObj obj)
        {
            ProEng.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            //ProEng.Usuarios.Remove(ConverterObj(obj));
            ProEng.SaveChanges();
        }

        public void ExcluirProjeto(ProjetoObj obj)
        {
            ProEng.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            //ProEng.Projetoes.Remove(ConverterObj(obj));
            ProEng.SaveChanges();
        }
    }
}
