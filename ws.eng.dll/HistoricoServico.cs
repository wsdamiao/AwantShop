using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ws.eng.obj;
using ws.eng.dao;

namespace ws.eng.dll
{
    public class HistoricoServico
    {
        private ServicoDao dao;

        public HistoricoServico()
        {
            dao = new ServicoDao();
        }

        public List<ServicoHistoricoObj>  Listar()
        {
            return dao.ListarHistoricoServico();
        }

        public List<ServicoHistoricoObj> ListarPorId(long ID)
        {
            return dao.ListarHistoricoServico().Where(x=> x.ID == ID).ToList();
        }

        public List<ServicoHistoricoObj> ListarPorIdUsuario(long ID)
        {
            return dao.ListarHistoricoServico().Where(x => x.IdUsuario == ID).ToList();
        }

        public List<ServicoHistoricoObj> ListarPorIdSerico(long ID)
        {
            return dao.ListarHistoricoServico().Where(x => x.IdServico == ID).ToList();
        }

        public List<ServicoHistoricoObj> ListarPorIdStatus(long ID)
        {
            return dao.ListarHistoricoServico().Where(x => x.IdStatus == ID).ToList();
        }

        public List<ServicoHistoricoObj> ListarPorIntervalor(DateTime dtInicial, DateTime dtFinal)
        {
            List<ServicoHistoricoObj> result = new List<ServicoHistoricoObj>();
            result = dao.ListarHistoricoServico().Where(x => x.DataHora >= dtInicial && x.DataHora <= dtFinal ).ToList();

            return result;
        }

        public List<ServicoHistoricoObj> FiltarIntervalorPorUsuario (List<ServicoHistoricoObj> result, long ID)
        {
            return result.Where(x => x.IdUsuario == ID).ToList();
        }

        public List<ServicoHistoricoObj> FiltarIntervalorPorIdStatus(List<ServicoHistoricoObj> result, long ID)
        {
            return result.Where(x => x.IdStatus == ID).ToList();
        }

        public List<ServicoHistoricoObj> FiltarIntervalorPorIdServico(List<ServicoHistoricoObj> result, long ID)
        {
            return result.Where(x => x.IdServico == ID).ToList();
        }

        public void Salvar(ServicoHistoricoObj obj)
        {
            dao.SalvarHistorico(obj);
        }
    }
}
