using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ws.eng.obj;

namespace ws.eng.dao
{
    public class ServicoHistoricoDao
    {
        ProEngEntities ProEng;

        public ServicoHistoricoDao()
        {
            ProEng = new ProEngEntities();
        }

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


        public List<ServicoHistoricoObj> Listar()
        {
            return PopularListaObj(ProEng.ServicoHistoricoes.ToList());
        }

        public void Salvar(ServicoHistoricoObj obj)
        {
            ProEng.ServicoHistoricoes.Add(ConverterObj(obj));
            ProEng.SaveChanges();
        }

    }
}
