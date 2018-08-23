using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ws.eng.obj;

namespace ws.eng.dao
{
    public class ServicoDao
    {
        ProEngEntities ProEng;

        public ServicoDao()
        {
            ProEng = new ProEngEntities();
        }

        #region Infraestrutura
       
        #region ServicoHistorico
        public ServicoHistoricoObj ConverterObj(ServicoHistorico obj)
        {
            ServicoHistoricoObj objOut = new ServicoHistoricoObj();

            objOut.ID = obj.ID;
            objOut.DataHora = obj.Data;
            objOut.IdServico = obj.IdServico;
            objOut.IdStatus = obj.IdStatus;
            objOut.IdUsuario = obj.IdUsuario;
            objOut.Historico = obj.Historico;

            return objOut;
        }

        public ServicoHistorico ConverterObj(ServicoHistoricoObj obj)
        {
            ServicoHistorico objOut = new ServicoHistorico();

            objOut.ID = obj.ID;
            objOut.Data = obj.DataHora;
            objOut.IdServico = obj.IdServico;
            objOut.IdStatus = obj.IdStatus;
            objOut.IdUsuario = obj.IdUsuario;
            objOut.Historico = obj.Historico;

            return objOut;
        }

        public List<ServicoHistoricoObj> PopularListaObj(List<ServicoHistorico> obj)
        {
            List<ServicoHistoricoObj> objOut = new List<ServicoHistoricoObj>();
            foreach (var item in obj)
            {
                objOut.Add(ConverterObj(item));
            }
            return objOut;
        }
        #endregion

        #region Servico
        public ServicoObj ConverteObj(Servico obj)
        {
            ServicoObj objOut = new ServicoObj();

            objOut.Ativo = obj.Ativo;
            objOut.Descricao = obj.Descricao;
            objOut.ID = obj.ID;
            objOut.IDServicoTipo = obj.IDTipoServico;
            objOut.Nome = obj.Nome;
            objOut.IDPais = obj.IDPais;
            objOut.PossuiCidade = obj.PossuiCidade;

            return objOut;
        }

        public Servico ConverteObj(ServicoObj obj)
        {
            Servico objOut = new Servico();

            objOut.Ativo = obj.Ativo;
            objOut.Descricao = obj.Descricao;
            objOut.ID = obj.ID;
            objOut.IDTipoServico = obj.IDServicoTipo;
            objOut.Nome = obj.Nome;
            objOut.IDPais = obj.IDPais;
            objOut.PossuiCidade = obj.PossuiCidade;

            return objOut;
        }

        public List<ServicoObj> PopularListaObj(List<Servico> obj)
        {
            List<ServicoObj> objOut = new List<ServicoObj>();
            foreach (var item in obj)
            {
                objOut.Add(ConverteObj(item));
            }
            return objOut;
        }
        #endregion

        #region ServicoTipo
        public ServicoTipo ConverteObj(ServicoTipoObj obj)
        {
            ServicoTipo objOut = new ServicoTipo();

            objOut.Descricao = obj.Descricao;
            objOut.ID = obj.ID;
            objOut.Nome = obj.Nome;

            return objOut;
        }

        public ServicoTipoObj ConverteObj(ServicoTipo obj)
        {
            ServicoTipoObj objOut = new ServicoTipoObj();

            objOut.Descricao = obj.Descricao;
            objOut.ID = obj.ID;
            objOut.Nome = obj.Nome;

            return objOut;
        }

        public List<ServicoTipoObj> PopularListaObj(List<ServicoTipo> obj)
        {
            List<ServicoTipoObj> objOut = new List<ServicoTipoObj>();
            foreach (var item in obj)
            {
                objOut.Add(ConverteObj(item));
            }
            return objOut;
        }
        #endregion
        #endregion
        
        public List<ServicoHistoricoObj> ListarHistoricoServico()
        {
            return PopularListaObj(ProEng.ServicoHistoricoes.ToList());
        }

        public List<ServicoObj> ListarServico()
        {
            return PopularListaObj(ProEng.Servicoes.ToList());
        }

        public List<ServicoTipoObj> ListarTipoServico()
        {
            return PopularListaObj(ProEng.ServicoTipoes.ToList());
        }

        public void SalvarHistorico(ServicoHistoricoObj obj)
        {
            ProEng.ServicoHistoricoes.Add(ConverterObj(obj));
            ProEng.SaveChanges();
        }

        public List<ServicoObj> ListarServicoEngenharia(int IdPais, int IdRegiao)
        {
            bool possuiCidade = false;

            if (IdRegiao != 0)
                possuiCidade = true;

            return ListarServico().Where(x => x.IDServicoTipo == 2 && x.IDPais == IdPais && x.Ativo == true && x.PossuiCidade == possuiCidade).ToList();
        }

        public List<ServicoObj> ListarServicoArquitetura(int IdPais, int IdRegiao)
        {
            bool possuiCidade = false;

            if (IdRegiao != 0)
                possuiCidade = true;

            return ListarServico().Where(x => x.IDServicoTipo == 1 && x.IDPais == IdPais && x.Ativo == true && x.PossuiCidade == possuiCidade).ToList();
        }      

    }
}
