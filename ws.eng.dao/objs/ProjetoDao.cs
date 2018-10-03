using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ws.eng.obj;

namespace ws.eng.dao
{
    public class ProjetoDao
    {
        ProEngEntities ProEng;

        public ProjetoDao()
        {
            ProEng = new ProEngEntities();
        }

        #region Infra
        private ProjetoObj ConverterObj(Projeto obj)
        {
            ProjetoObj objOut = new ProjetoObj();

            objOut.Area = (AreaProjeto)obj.AreaID;
            objOut.AreaPersonalizada= obj.AreaPersonalizada;
            //objOut.CidadeID = obj.cid;
            objOut.DataCad = obj.DataCad.Value;
            objOut.EnderecoCompletoEmpreendimento = obj.EnderecoObra;
            objOut.ID = obj.ID;
            objOut.logradouroID = obj.LogradouroID;
            objOut.PadraoAcabamento = (PadraoProjeto)obj.PadraoID;
            objOut.Projeto = (TipoProjeto)obj.ProjetoID;
            objOut.Regiao= (RegiaoProjeto)obj.PaisID;
            //objOut.Servicos = obj.UsuarioID;
            objOut.TextoLivre = obj.TextoLivre;
            objOut.ValorMetroQuadradoAplicado = obj.VlMetroQuadradoBase;

            return objOut;
        }

        private Projeto ConverterObj(ProjetoObj obj)
        {
            Projeto objOut = new Projeto();

            objOut.AreaID = (int)obj.Area;
            objOut.AreaPersonalizada = obj.AreaPersonalizada;
            //objOut.CidadeID = obj.cid;
            objOut.DataCad = obj.DataCad;
            objOut.EnderecoObra = obj.EnderecoCompletoEmpreendimento;
            objOut.ID = obj.ID;
            objOut.LogradouroID = obj.logradouroID;
            objOut.PadraoID = (int)obj.PadraoAcabamento;
            objOut.ProjetoID = (int)obj.Projeto;
            objOut.PaisID = (int)obj.Regiao;
            //objOut.Servicos = obj.UsuarioID;
            objOut.TextoLivre = obj.TextoLivre;
            objOut.VlMetroQuadradoBase = obj.ValorMetroQuadradoAplicado;

            return objOut;
        }
        #endregion

        public bool Incluir(ProjetoObj obj)
        {
            Projeto objDestino = ConverterObj(obj);

            ProEng.Projetoes.Add(objDestino);
            return (ProEng.SaveChanges() > 0);

        }

        public bool Excluir(long ID)
        {
            Projeto objDestino = ProEng.Projetoes.Find(ID);

            ProEng.Entry(objDestino).State = System.Data.Entity.EntityState.Deleted;
            return (ProEng.SaveChanges() > 0);
        }

        public bool Alterar(ProjetoObj obj)
        {
            Projeto objDestino = ConverterObj(obj);

            ProEng.Entry(objDestino).State = System.Data.Entity.EntityState.Modified;
            return (ProEng.SaveChanges() > 0);
        }

        public ProjetoObj Buscar(long ID)
        {
            return ConverterObj(ProEng.Projetoes.Find(ID));
        }
    }

    public class ProjetoAcompanharDao
    {
        ProEngEntities ProEng;

        public ProjetoAcompanharDao()
        {
            ProEng = new ProEngEntities();
        }

        #region Infra
        private ProjetoAcompanharObj ConverterObj(ProjetoAcompanhar obj)
        {
            ProjetoAcompanharObj objOut = new ProjetoAcompanharObj();

            objOut.ID = obj.ID;
            objOut.ClienteID = obj.ClienteID;
            objOut.Data = obj.Data;
            objOut.EstadoID = obj.EstadoID;
            objOut.ProjetoID = obj.ProjetoID;
            objOut.ServicoID = obj.ServicoID;
            objOut.Texto = obj.Texto;
            objOut.UsuarioID = obj.UsuarioID;


            return objOut;
        }

        private ProjetoAcompanhar ConverterObj(ProjetoAcompanharObj obj)
        {
            ProjetoAcompanhar objOut = new ProjetoAcompanhar();

            objOut.ID = obj.ID;
            objOut.ClienteID = obj.ClienteID;
            objOut.Data = obj.Data;
            objOut.EstadoID = obj.EstadoID;
            objOut.ProjetoID = obj.ProjetoID;
            objOut.ServicoID = obj.ServicoID;
            objOut.Texto = obj.Texto;
            objOut.UsuarioID = obj.UsuarioID;

            return objOut;
        }

        private List<ProjetoAcompanhar> ConverterObj(List<ProjetoAcompanharObj> obj)
        {
            List<ProjetoAcompanhar> objSaida = new List<ProjetoAcompanhar>();

            foreach (var item in obj)
                objSaida.Add(ConverterObj(item));

            return objSaida;
        }

        private List<ProjetoAcompanharObj> ConverterObj(List<ProjetoAcompanhar> obj)
        {
            List<ProjetoAcompanharObj> objSaida = new List<ProjetoAcompanharObj>();

            foreach (var item in obj)
                objSaida.Add(ConverterObj(item));

            return objSaida;
        }
        #endregion

        public bool Incluir(ProjetoAcompanharObj obj)
        {
            ProjetoAcompanhar objDestino = ConverterObj(obj);

            ProEng.ProjetoAcompanhars.Add(objDestino);
            return (ProEng.SaveChanges() > 0);
            
        }

        public bool Excluir(long ID)
        {
            ProjetoAcompanhar objDestino = ProEng.ProjetoAcompanhars.Find(ID);

            ProEng.Entry(objDestino).State = System.Data.Entity.EntityState.Deleted;
            return (ProEng.SaveChanges() > 0);
        }

        public bool Alterar(ProjetoAcompanharObj obj)
        {
            ProjetoAcompanhar objDestino = ConverterObj(obj);

            ProEng.Entry(objDestino).State = System.Data.Entity.EntityState.Modified;
            return (ProEng.SaveChanges() > 0);
        }

        public ProjetoAcompanharObj Buscar(int ID)
        {
            return ConverterObj(ProEng.ProjetoAcompanhars.Find(ID));
        }

        public List<ProjetoAcompanharObj> ListarPorCliente(int ID)
        {
            var lista = (from c in ProEng.ProjetoAcompanhars where c.ClienteID == ID select c).ToList();
            return ConverterObj(lista);
        }

        public List<ProjetoAcompanharObj> ListarPorUsuario(int ID)
        {
            var lista = (from c in ProEng.ProjetoAcompanhars where c.UsuarioID == ID select c).ToList();
            return ConverterObj(lista);
        }

        public List<ProjetoAcompanharObj> ListarPorEstado(int ID)
        {
            var lista = (from c in ProEng.ProjetoAcompanhars where c.EstadoID == ID select c).ToList();
            return ConverterObj(lista);
        }

        public List<ProjetoAcompanharObj> ListarPorUsuarioEstado(int usuarioID, long estadoID)
        {
            var lista = (from c in ProEng.ProjetoAcompanhars where c.EstadoID == estadoID && c.UsuarioID == usuarioID select c).ToList();
            return ConverterObj(lista);
        }

        public List<ProjetoAcompanharObj> ListarPorProjeto(long projetoID, int usuarioID)
        {
            var lista = (from c in ProEng.ProjetoAcompanhars where c.ProjetoID == projetoID && c.UsuarioID == usuarioID select c).ToList();
            return ConverterObj(lista);
        }

        public List<ProjetoAcompanharObj> ListarPorProjeto(long projetoID)
        {
            var lista = (from c in ProEng.ProjetoAcompanhars where c.ProjetoID == projetoID select c).ToList();
            return ConverterObj(lista);
        }

        public List<ProjetoAcompanharObj> ListarPorServico(long servicoID, int usuarioID)
        {
            var lista = (from c in ProEng.ProjetoAcompanhars where c.ServicoID == servicoID && c.UsuarioID == usuarioID select c).ToList();
            return ConverterObj(lista);
        }
    }
}
