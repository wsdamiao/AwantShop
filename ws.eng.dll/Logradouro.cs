using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ws.eng.dao;
using ws.eng.obj;

namespace ws.eng.dll
{
    public class LogradouroDll
    {
        LogradouroDao lgrDao;

        public LogradouroDll()
        {
            lgrDao = new LogradouroDao();
        }

        #region Infra
        public List<LogradouroObj> ListarCidades()
        {
            List<LogradouroObj> obj = new List<LogradouroObj>();
            
            return lgrDao.ListarLogradouro();
        }

        public List<PaisObj> ListarPaises ()
        {
            List<PaisObj> obj = new List<PaisObj>();
            
            return lgrDao.ListarPais();
        }
        #endregion       
        public List<LogradouroObj> BuscarMunicipiosPorCodUF(string CodUF)
        {
            List<LogradouroObj> obj = new List<LogradouroObj>();
           
            return lgrDao.ListarLogradouro();
        }

        public List<LogradouroObj> BuscarMunicipiosPorSiglaUF(string SiglaUF)
        {
            List<LogradouroObj> obj = new List<LogradouroObj>();
           
            return lgrDao.ListarLogradouro();
        }

        public LogradouroObj BuscarMunicipio(string CodMunicipio)
        {
            return lgrDao.ListarLogradouro().Where(x=> x.CodMunicipio == CodMunicipio).FirstOrDefault();
        }

        public LogradouroObj BuscarMunicipioPorId(int Id)
        {
            return lgrDao.ListarLogradouro().Where(x => x.ID == Id).FirstOrDefault();
        }

        public PaisObj BuscarPaisPorId(int Id)
        {
            return lgrDao.BuscarPaisPorId(Id);
        }

        public void Salvar(PaisObj obj)
        {
            try
            {
                lgrDao.SalvarPais(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
