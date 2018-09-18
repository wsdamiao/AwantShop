using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ws.eng.dao;
using ws.eng.obj;
using Microsoft;

namespace ws.eng.dll
{
    public class ServicoDll
    {
        ServicoDao srvDao;

        public ServicoDll()
        {
            srvDao = new ServicoDao();
        }

        public List<ServicoObj> ListarServicoArquitetura(int IdPais, int IdRegiao)
        {
            return srvDao.ListarServicoArquitetura(IdPais, IdRegiao);
        }

        public List<ServicoObj> ListarServicoEngenharia(int IdPais, int IdRegiao)
        {
            return srvDao.ListarServicoEngenharia(IdPais, IdRegiao);
        }
    }
}
