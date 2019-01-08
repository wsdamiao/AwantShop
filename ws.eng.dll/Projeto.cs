using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ws.eng.obj;
using ws.eng.dao;

namespace ws.eng.dll
{
    public class ProjetoDll
    {
        ProjetoDao dao;

        public ProjetoDll()
        {
            dao = new ProjetoDao();
        }

        public bool Incluir(ProjetoObj obj)
        {
            return true;
        }

        public bool Editar(ProjetoObj obj)
        {
            return true;
        }

        public bool Excluir(int ID)
        {
            return true;
        }

        public ProjetoObj Buscar(long ID)
        {
            ProjetoObj obj = dao.Buscar(ID);
            obj.Cliente = new UsuarioClienteDao().ListarCliente().Where(x => x.ID == obj.ClienteID).FirstOrDefault();
            return obj;
        }

        public List<ProjetoObj> ListarPorCliente(int ID)
        {
            return new List<ProjetoObj>();
        }

        public List<ProjetoObj> ListarPorUsuario(int ID)
        {
            return new List<ProjetoObj>();
        }

        public List<ProjetoObj> ListarPorEstado(int ID)
        {
            return new List<ProjetoObj>();
        }

        public List<ProjetoObj> ListarPorUsuarioEstado(int ID, int EstadoID)
        {
            return new List<ProjetoObj>();
        }
    }

    public class ProjetoSolicitacaoDll
    {
        ProjetoSolicitacaoDao dao;

        public ProjetoSolicitacaoDll()
        {
            dao = new ProjetoSolicitacaoDao();
        }

        public bool Incluir(ProjetoSolicitacaoObj obj)
        {            
            return dao.Incluir(obj);            
        }

        public bool Incluir(ProjetoSolicitacaoIteracaoObj obj)
        {
            return dao.Incluir(obj);
        }

        public bool Alterar(ProjetoSolicitacaoObj obj)
        {
            return dao.Alterar(obj);
        }

        public bool Excluir(int ID)
        {
            return dao.Excluir(ID);
        }

        public ProjetoSolicitacaoObj Buscar(int ID)
        {
            return dao.Buscar(ID);
        }

        public ProjetoSolicitacaoObj BuscarPorprojeto(long ProjetoID)
        {
            return dao.ListarPorProjeto(ProjetoID).FirstOrDefault();
        }

        public List<ProjetoSolicitacaoObj> ListarPorCliente(int ID)
        {
            return dao.ListarPorCliente(ID);
        }

        public List<ProjetoSolicitacaoObj> ListarPorUsuario(int ID)
        {
            return dao.ListarPorUsuario(ID);
        }

        public List<ProjetoSolicitacaoObj> ListarPorEstado(int ID)
        {
            return dao.ListarPorEstado(ID);
        }

        public List<ProjetoSolicitacaoObj> ListarPorUsuarioEstado(int usuarioID, long estadoID)
        {
            return dao.ListarPorUsuarioEstado(usuarioID, estadoID);
        }

        public List<ProjetoSolicitacaoObj> ListarPorProjeto(long projetoID, int usuarioID)
        {
            return dao.ListarPorProjeto(projetoID, usuarioID);
        }

        public List<ProjetoSolicitacaoObj> ListarPorProjeto(long projetoID)
        {
            List<ProjetoSolicitacaoObj> obj = dao.ListarPorProjeto(projetoID);

            //foreach(var item in obj)
            //    item.Usuario = new UsuarioClienteDll().ListarUsuario().Where(x => x.ID == item.UsuarioID).FirstOrDefault();

            return obj;
        }

        public List<ProjetoSolicitacaoObj> ListarPorServico(long servicoID, int usuarioID)
        {
            return dao.ListarPorServico(servicoID, usuarioID);
        }

        public List<ProjetoSolicitacaoIteracaoObj> ListarIteracoesPorSolicitacao(long solicitacaoID)
        {
            return dao.ListarIteracoesPorSolicitacao(solicitacaoID);
        }
        

    }

    public class ProjetosStatusDll
    {
        ProjetoStatusDao dao;

        public ProjetosStatusDll()
        {
            dao = new ProjetoStatusDao();
        }

        #region ProjetoStatus

        public bool Incluir(ProjetoStatusObj obj)
        {
            return dao.Incluir(obj);
        }

        public bool Alterar(ProjetoStatusObj obj)
        {           
            return dao.Alterar(obj);
        }

        public bool ExcluirStatus(long ID)
        {
            return ExcluirStatus(ID);
        }

        public ProjetoStatusObj BuscarStatus(int ID)
        {
            return dao.BuscarStatus(ID);
        }

        public List<ProjetoStatusObj> ListarStatusPorprojeto(long ProjetoID)
        {
            return dao.ListarStatusPorprojeto(ProjetoID);
        }

        public ProjetoStatusObj UltimoStatus(int ProjetoID)
        {
            return dao.UltimoStatus(ProjetoID);
        }

        #endregion
    }

    public class IteracaoStatusDll
    {
        IteracaoStatusDao dao;

        public IteracaoStatusDll()
        {
            dao = new IteracaoStatusDao();
        }

        public IteracaoStatusObj Buscar(int ID)
        {
            return dao.Buscar(ID);
        }

        public List<IteracaoStatusObj> Listar()
        {
            return dao.Listar();
        }

    }
}
