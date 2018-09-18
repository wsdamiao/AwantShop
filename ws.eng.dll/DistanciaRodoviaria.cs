using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ws.eng.dao;
using ws.eng.obj;

namespace ws.eng.dll
{
    public class DistanciaRodoviariaDll
    {
        DistanciaRodoviariaDao dao;

        public DistanciaRodoviariaDll()
        {
            dao = new DistanciaRodoviariaDao();
        }

        public List<DistanciaRodoviariaObj> Listar()
        {
            try
            {
                return dao.Listar();

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public DistanciaRodoviariaObj Buscar(int ID)
        {
            try
            {
                return dao.Buscar(ID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DistanciaRodoviariaObj BuscarPorLogradouro(int logradouroID)
        {
            try
            {
                return dao.BuscarPorLogradouro(logradouroID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Salvar(DistanciaRodoviariaObj obj)
        {
            try
            {
                dao.Salvar(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
