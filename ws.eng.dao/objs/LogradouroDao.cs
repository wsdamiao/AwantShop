using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ws.eng.obj;

namespace ws.eng.dao
{
    public class LogradouroDao
    {
        ProEngEntities ProEng;

        public LogradouroDao()
        {
            ProEng = new ProEngEntities();            
        }

        public List<LogradouroObj> ListarLogradouro()
        {
            return PopularListaObj(ProEng.Logradouroes.ToList());
        }

        public List<PaisObj> ListarPais()
        {
            return PopularListaObj(ProEng.Pais.ToList());
        }

        private PaisObj ConverterObj(Pai obj)
        {
            PaisObj objOut = new PaisObj();

            objOut.ID = obj.ID;
            objOut.Nome= obj.Nome;
            objOut.NomeOficial = obj.NomeOficial;
            objOut.Idioma = obj.Idioma;
            objOut.MetroQuadrado = obj.MetroQuadrado.Value;

            return objOut;
        }

        private LogradouroObj ConverterObj(Logradouro obj)
        {
            LogradouroObj objOut = new LogradouroObj();

            objOut.ID = obj.ID;
            objOut.CodMunicipio = obj.CodMunicipio;
            objOut.NomeMunicipio = obj.NomeMunicipio;

            return objOut;
        }

        private List<LogradouroObj> PopularListaObj(List<Logradouro> obj)
        {
            List<LogradouroObj> objOut = new List<LogradouroObj>();
            foreach(var item in obj)
            {
                objOut.Add(ConverterObj(item));
            }
            return objOut;
        }

        private List<PaisObj> PopularListaObj(List<Pai> obj)
        {
            List<PaisObj> objOut = new List<PaisObj>();
            foreach (var item in obj)
            {
                objOut.Add(ConverterObj(item));
            }
            return objOut;
        }

    }
}
