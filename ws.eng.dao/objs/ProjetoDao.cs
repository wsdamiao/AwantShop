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
            objOut.LogradouroID = obj.LogradouroID;
            objOut.PadraoAcabamento = (PadraoProjeto)obj.PadraoID;
            objOut.Projeto = (TipoProjeto)obj.ProjetoID;
            objOut.Regiao= (RegiaoProjeto)obj.PaisID;
            //objOut.Servicos = obj.UsuarioID;
            objOut.TextoLivre = obj.TextoLivre;
            objOut.VlMetroQuadradoBase = obj.VlMetroQuadradoBase;
            objOut.ClienteID = obj.ClienteID; 

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
            objOut.LogradouroID = obj.LogradouroID;
            objOut.PadraoID = (int)obj.PadraoAcabamento;
            objOut.ProjetoID = (int)obj.Projeto;
            objOut.PaisID = (int)obj.Regiao;
            //objOut.Servicos = obj.UsuarioID;
            objOut.TextoLivre = obj.TextoLivre;
            objOut.VlMetroQuadradoBase = obj.VlMetroQuadradoBase;

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

    public class ProjetoSolicitacaoDao
    {
        ProEngEntities ProEng;

        public ProjetoSolicitacaoDao()
        {
            ProEng = new ProEngEntities();
        }

        #region Infra
        private ProjetoSolicitacaoObj ConverterObj(ProjetoSolicitacao obj)
        {
            ProjetoSolicitacaoObj objOut = new ProjetoSolicitacaoObj();

            objOut.ID = obj.ID;
            objOut.ClienteID = obj.ClienteID;
            objOut.Data = obj.Data;
            objOut.EstadoID = obj.EstadoID;
            objOut.ProjetoID = obj.ProjetoID;
            objOut.ServicoID = obj.ServicoID;
            objOut.Titulo = obj.Titulo;
            objOut.UsuarioID = obj.UsuarioID;


            return objOut;
        }

        private ProjetoSolicitacao ConverterObj(ProjetoSolicitacaoObj obj)
        {
            ProjetoSolicitacao objOut = new ProjetoSolicitacao();

            objOut.ID = obj.ID;
            objOut.ClienteID = obj.ClienteID;
            objOut.Data = obj.Data;
            objOut.EstadoID = obj.EstadoID;
            objOut.ProjetoID = obj.ProjetoID;
            objOut.ServicoID = obj.ServicoID;
            objOut.Titulo = obj.Titulo;
            objOut.UsuarioID = obj.UsuarioID;

            return objOut;
        }

        private List<ProjetoSolicitacao> ConverterObj(List<ProjetoSolicitacaoObj> obj)
        {
            List<ProjetoSolicitacao> objSaida = new List<ProjetoSolicitacao>();

            foreach (var item in obj)
                objSaida.Add(ConverterObj(item));

            return objSaida;
        }

        private List<ProjetoSolicitacaoObj> ConverterObj(List<ProjetoSolicitacao> obj)
        {
            List<ProjetoSolicitacaoObj> objSaida = new List<ProjetoSolicitacaoObj>();

            foreach (var item in obj)
                objSaida.Add(ConverterObj(item));

            return objSaida;
        }

        private ProjetoSolicitacaoIteracaoObj ConverterObj(ProjetoSolicitacaoIteracao obj)
        {
            ProjetoSolicitacaoIteracaoObj objOut = new ProjetoSolicitacaoIteracaoObj();

            objOut.SolicitacaoID = obj.SolicitacaoID;
            objOut.Data = obj.Data;
            objOut.Data = obj.Data;
            objOut.UsuarioID = obj.UsuarioID;
            objOut.EstadoID = obj.EstadoID;
            objOut.Texto = obj.Texto;
            objOut.LeituraRealizada = obj.leituraRealizada;
            objOut.UsuarioID = obj.UsuarioID;


            return objOut;
        }

        private ProjetoSolicitacaoIteracao ConverterObj(ProjetoSolicitacaoIteracaoObj obj)
        {
            ProjetoSolicitacaoIteracao objOut = new ProjetoSolicitacaoIteracao();

            objOut.SolicitacaoID = obj.SolicitacaoID;
            objOut.Data = obj.Data;
            objOut.Data = obj.Data;
            objOut.UsuarioID = obj.UsuarioID;
            objOut.EstadoID = obj.EstadoID;
            objOut.Texto = obj.Texto;
            objOut.leituraRealizada = obj.LeituraRealizada;
            objOut.UsuarioID = obj.UsuarioID;

            return objOut;
        }

        private List<ProjetoSolicitacaoIteracao> ConverterObj(List<ProjetoSolicitacaoIteracaoObj> obj)
        {
            List<ProjetoSolicitacaoIteracao> objSaida = new List<ProjetoSolicitacaoIteracao>();

            foreach (var item in obj)
                objSaida.Add(ConverterObj(item));

            return objSaida;
        }

        private List<ProjetoSolicitacaoIteracaoObj> ConverterObj(List<ProjetoSolicitacaoIteracao> obj)
        {
            List<ProjetoSolicitacaoIteracaoObj> objSaida = new List<ProjetoSolicitacaoIteracaoObj>();

            foreach (var item in obj)
                objSaida.Add(ConverterObj(item));

            return objSaida;
        }
        
        #endregion

        #region ProjetoSolicitacao
        public bool Incluir(ProjetoSolicitacaoObj obj)
        {
            ProjetoSolicitacao objDestino = ConverterObj(obj);

            ProEng.ProjetoSolicitacaos.Add(objDestino);
            return (ProEng.SaveChanges() > 0);
            
        }

        public bool Excluir(long ID)
        {
            ProjetoSolicitacao objDestino = ProEng.ProjetoSolicitacaos.Find(ID);

            ProEng.Entry(objDestino).State = System.Data.Entity.EntityState.Deleted;
            return (ProEng.SaveChanges() > 0);
        }

        public bool Alterar(ProjetoSolicitacaoObj obj)
        {
            ProjetoSolicitacao objDestino = ConverterObj(obj);

            ProEng.Entry(objDestino).State = System.Data.Entity.EntityState.Modified;
            return (ProEng.SaveChanges() > 0);
        }

        public ProjetoSolicitacaoObj Buscar(int ID)
        {
            return ConverterObj(ProEng.ProjetoSolicitacaos.Find(ID));
        }
        
        public List<ProjetoSolicitacaoObj> ListarPorCliente(int ID)
        {
            var lista = (from c in ProEng.ProjetoSolicitacaos where c.ClienteID == ID select c).ToList();
            return ConverterObj(lista);
        }

        public List<ProjetoSolicitacaoObj> ListarPorUsuario(int ID)
        {
            var lista = (from c in ProEng.ProjetoSolicitacaos where c.UsuarioID == ID select c).ToList();
            return ConverterObj(lista);
        }

        public List<ProjetoSolicitacaoObj> ListarPorEstado(int ID)
        {
            var lista = (from c in ProEng.ProjetoSolicitacaos where c.EstadoID == ID select c).ToList();
            return ConverterObj(lista);
        }

        public List<ProjetoSolicitacaoObj> ListarPorUsuarioEstado(int usuarioID, long estadoID)
        {
            var lista = (from c in ProEng.ProjetoSolicitacaos where c.EstadoID == estadoID && c.UsuarioID == usuarioID select c).ToList();
            return ConverterObj(lista);
        }

        public List<ProjetoSolicitacaoObj> ListarPorProjeto(long projetoID, int usuarioID)
        {
            var lista = (from c in ProEng.ProjetoSolicitacaos where c.ProjetoID == projetoID && c.UsuarioID == usuarioID select c).ToList();
            return ConverterObj(lista);
        }

        public List<ProjetoSolicitacaoObj> ListarPorProjeto(long projetoID)
        {
            var lista = (from c in ProEng.ProjetoSolicitacaos where c.ProjetoID == projetoID select c).ToList();
            return ConverterObj(lista);
        }

        public List<ProjetoSolicitacaoObj> ListarPorServico(long servicoID, int usuarioID)
        {
            var lista = (from c in ProEng.ProjetoSolicitacaos where c.ServicoID == servicoID && c.UsuarioID == usuarioID select c).ToList();
            return ConverterObj(lista);
        }
        #endregion

        #region ProjetoSolicitacaoIteracao

        public bool Incluir(ProjetoSolicitacaoIteracaoObj obj)
        {
            ProjetoSolicitacaoIteracao objDestino = ConverterObj(obj);

            ProEng.ProjetoSolicitacaoIteracaos.Add(objDestino);
            return (ProEng.SaveChanges() > 0);

        }

        public ProjetoSolicitacaoIteracaoObj Buscar(long ID)
        {
            return ConverterObj(ProEng.ProjetoSolicitacaoIteracaos.Find(ID));
        }

        public List<ProjetoSolicitacaoIteracaoObj> ListarIteracoesPorSolicitacao(long solicitacaoID)
        {
            var lista = (from c in ProEng.ProjetoSolicitacaoIteracaos where c.SolicitacaoID == solicitacaoID select c).ToList();
            return ConverterObj(lista);
        }
        #endregion

        

    }

    public class ProjetoStatusDao
    {
        ProEngEntities ProEng;

        public ProjetoStatusDao()
        {
            ProEng = new ProEngEntities();
        }

        #region Infra
        private List<ProjetoStatu> ConverterObj(List<ProjetoStatusObj> obj)
        {
            List<ProjetoStatu> objSaida = new List<ProjetoStatu>();

            foreach (var item in obj)
                objSaida.Add(ConverterObj(item));

            return objSaida;
        }

        private List<ProjetoStatusObj> ConverterObj(List<ProjetoStatu> obj)
        {
            List<ProjetoStatusObj> objSaida = new List<ProjetoStatusObj>();

            foreach (var item in obj)
                objSaida.Add(ConverterObj(item));

            return objSaida;
        }

        private ProjetoStatusObj ConverterObj(ProjetoStatu obj)
        {
            ProjetoStatusObj objOut = new ProjetoStatusObj();

            objOut.Observacao = obj.Observacao;
            objOut.DataStatus = obj.DataStatus;
            objOut.StatusID = obj.StatusID;
            objOut.UsuarioID = obj.UsuarioID;
            objOut.ProjetoID = obj.ProjetoID;

            return objOut;
        }

        private ProjetoStatu ConverterObj(ProjetoStatusObj obj)
        {
            ProjetoStatu objOut = new ProjetoStatu();

            objOut.Observacao = obj.Observacao;
            objOut.DataStatus = obj.DataStatus;
            objOut.StatusID = obj.StatusID;
            objOut.UsuarioID = obj.UsuarioID;
            objOut.ProjetoID = obj.ProjetoID;

            return objOut;
        }
        #endregion

        #region ProjetoStatus

        public bool Incluir(ProjetoStatusObj obj)
        {
            ProjetoStatu objDestino = ConverterObj(obj);

            ProEng.ProjetoStatus.Add(objDestino);
            return (ProEng.SaveChanges() > 0);

        }

        public bool Alterar(ProjetoStatusObj obj)
        {
            ProjetoStatu objDestino = ConverterObj(obj);

            ProEng.Entry(objDestino).State = System.Data.Entity.EntityState.Modified;
            return (ProEng.SaveChanges() > 0);
        }

        public bool ExcluirStatus(long ID)
        {
            ProjetoStatu objDestino = ProEng.ProjetoStatus.Find(ID);

            ProEng.Entry(objDestino).State = System.Data.Entity.EntityState.Deleted;
            return (ProEng.SaveChanges() > 0);
        }

        public ProjetoStatusObj BuscarStatus(int ID)
        {
            return ConverterObj(ProEng.ProjetoStatus.Find(ID));
        }

        public List<ProjetoStatusObj> ListarStatusPorprojeto(long ProjetoID)
        {
            return ConverterObj(ProEng.ProjetoStatus.Where(x => x.ProjetoID == ProjetoID).ToList());
        }

        public ProjetoStatusObj UltimoStatus(int ProjetoID)
        {
            return ConverterObj(ProEng.ProjetoStatus.Where(x => x.ProjetoID == ProjetoID).OrderByDescending(x => x.DataStatus).ToList().FirstOrDefault());
        }

        #endregion
    }

    public class IteracaoStatusDao
    {
        ProEngEntities ProEng;

        public IteracaoStatusDao()
        {
            ProEng = new ProEngEntities();
        }

        #region Infra
        private List<IteracaoStatu> ConverterObj(List<IteracaoStatusObj> obj)
        {
            List<IteracaoStatu> objSaida = new List<IteracaoStatu>();

            foreach (var item in obj)
                objSaida.Add(ConverterObj(item));

            return objSaida;
        }

        private List<IteracaoStatusObj> ConverterObj(List<IteracaoStatu> obj)
        {
            List<IteracaoStatusObj> objSaida = new List<IteracaoStatusObj>();

            foreach (var item in obj)
                objSaida.Add(ConverterObj(item));

            return objSaida;
        }

        private IteracaoStatusObj ConverterObj(IteracaoStatu obj)
        {
            IteracaoStatusObj objOut = new IteracaoStatusObj();

            objOut.ID = obj.ID;
            objOut.Nome = obj.Nome;            

            return objOut;
        }

        private IteracaoStatu ConverterObj(IteracaoStatusObj obj)
        {
            IteracaoStatu objOut = new IteracaoStatu();

            objOut.ID = obj.ID;
            objOut.Nome = obj.Nome;
            

            return objOut;
        }
        #endregion

        #region ProjetoStatus

        //public bool Incluir(ProjetoStatusObj obj)
        //{
        //    ProjetoStatu objDestino = ConverterObj(obj);

        //    ProEng.ProjetoStatus.Add(objDestino);
        //    return (ProEng.SaveChanges() > 0);

        //}

        //public bool Alterar(ProjetoStatusObj obj)
        //{
        //    ProjetoStatu objDestino = ConverterObj(obj);

        //    ProEng.Entry(objDestino).State = System.Data.Entity.EntityState.Modified;
        //    return (ProEng.SaveChanges() > 0);
        //}

        //public bool ExcluirStatus(long ID)
        //{
        //    ProjetoStatu objDestino = ProEng.ProjetoStatus.Find(ID);

        //    ProEng.Entry(objDestino).State = System.Data.Entity.EntityState.Deleted;
        //    return (ProEng.SaveChanges() > 0);
        //}

        public IteracaoStatusObj Buscar(int ID)
        {
            return ConverterObj(ProEng.IteracaoStatus.Find(ID));
        }

        public List<IteracaoStatusObj> Listar()
        {
            return ConverterObj(ProEng.IteracaoStatus.ToList());
        }        

        #endregion
    }
}
