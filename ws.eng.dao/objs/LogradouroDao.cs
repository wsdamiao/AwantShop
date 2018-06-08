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
            objOut.MoedaID = obj.MoedaID.Value;

            return objOut;
        }

        private Pai ConverterObj(PaisObj obj)
        {
            Pai objOut = new Pai();

            objOut.ID = obj.ID;
            objOut.Nome = obj.Nome;
            objOut.NomeOficial = obj.NomeOficial;
            objOut.Idioma = obj.Idioma;
            objOut.MetroQuadrado = obj.MetroQuadrado;
            objOut.MoedaID = obj.MoedaID;

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

        public PaisObj BuscarPaisPorId(int Id)
        {
            return this.ListarPais().Where(x => x.ID == Id).FirstOrDefault();
        }

        public void SalvarPais(PaisObj obj)
        {
            try
            {
                if (obj.ID == 0)
                {
                    ProEng.Pais.Add(ConverterObj(obj));
                }
                else
                {
                    var aux = ProEng.Pais.Find(obj.ID);

                    aux.MetroQuadrado = obj.MetroQuadrado;
                    aux.Nome = obj.Nome;
                    aux.NomeOficial = obj.NomeOficial;
                    aux.MoedaID = obj.MoedaID;

                    ProEng.Entry(aux).State = System.Data.Entity.EntityState.Modified;
                }
                ProEng.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
