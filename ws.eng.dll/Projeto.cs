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
            return dao.Buscar(ID);
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

    public class ProjetoAcompanharDll
    {
        ProjetoAcompanharDao dao;

        public ProjetoAcompanharDll()
        {
            dao = new ProjetoAcompanharDao();
        }

        public bool Incluir(ProjetoAcompanharObj obj)
        {            
            return dao.Incluir(obj);            
        }

        public bool Alterar(ProjetoAcompanharObj obj)
        {
            return dao.Alterar(obj);
        }

        public bool Excluir(int ID)
        {
            return dao.Excluir(ID);
        }

        public ProjetoAcompanharObj Buscar(int ID)
        {
            return dao.Buscar(ID);
        }

        public List<ProjetoAcompanharObj> ListarPorCliente(int ID)
        {
            return dao.ListarPorCliente(ID);
        }

        public List<ProjetoAcompanharObj> ListarPorUsuario(int ID)
        {
            return dao.ListarPorUsuario(ID);
        }

        public List<ProjetoAcompanharObj> ListarPorEstado(int ID)
        {
            return dao.ListarPorEstado(ID);
        }

        public List<ProjetoAcompanharObj> ListarPorUsuarioEstado(int usuarioID, long estadoID)
        {
            return dao.ListarPorUsuarioEstado(usuarioID, estadoID);
        }

        public List<ProjetoAcompanharObj> ListarPorProjeto(long projetoID, int usuarioID)
        {
            return dao.ListarPorProjeto(projetoID, usuarioID);
        }

        public List<ProjetoAcompanharObj> ListarPorProjeto(long projetoID)
        {
            return dao.ListarPorProjeto(projetoID);
        }

        public List<ProjetoAcompanharObj> ListarPorServico(long servicoID, int usuarioID)
        {
            return dao.ListarPorServico(servicoID, usuarioID);
        }
    }
}
