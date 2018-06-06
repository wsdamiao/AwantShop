using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ws.eng.obj;

namespace ws.eng.dao
{
    public class DistanciaRodoviariaDao
    {
        ProEngEntities ProEng;

        public DistanciaRodoviariaDao()
        {
            ProEng = new ProEngEntities();
        }

        #region Infra
        private DistanciaRodoviariaObj ConverterObj(Distancia obj)
        {
            DistanciaRodoviariaObj objOut = new DistanciaRodoviariaObj();

            objOut.ID = obj.ID;
            objOut.Distancia  = obj.DistRodoviaria;
            objOut.LogradouroID = obj.LogradouroID;
            objOut.ValorPercentual = obj.Valor;
            
            return objOut;
        }

        private Distancia ConverterObj(DistanciaRodoviariaObj obj)
        {
            Distancia objOut = new Distancia();

            objOut.ID = obj.ID;
            objOut.DistRodoviaria = obj.Distancia;
            objOut.LogradouroID = obj.LogradouroID;
            objOut.Valor = obj.ValorPercentual;

            return objOut;
        }

        private List<DistanciaRodoviariaObj> PopularListaObj(List<Distancia> obj)
        {
            List<DistanciaRodoviariaObj> objOut = new List<DistanciaRodoviariaObj>();
            foreach (var item in obj)
            {
                objOut.Add(ConverterObj(item));
            }
            return objOut;
        }

        public List<DistanciaRodoviariaObj> Listar()
        {
            return PopularListaObj(ProEng.Distancias.ToList());
        }

        public void Salvar(DistanciaRodoviariaObj obj)
        {
            try
            {
                if (obj.ID == 0)
                {
                    ProEng.Distancias.Add(ConverterObj(obj));
                }
                else
                {
                    var aux = ProEng.Distancias.Find(obj.ID);

                    aux.DistRodoviaria = obj.Distancia;                    
                    aux.LogradouroID = obj.LogradouroID;
                    aux.Valor = obj.ValorPercentual;                    

                    ProEng.Entry(aux).State = System.Data.Entity.EntityState.Modified;
                }
                ProEng.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DistanciaRodoviariaObj Buscar(int ID)
        {            
            try
            {
                return Listar().Where(x => x.ID == ID).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public DistanciaRodoviariaObj BuscarPorLogradouro(int LogradouroID)
        {
            try
            {
                return Listar().Where(x => x.LogradouroID == LogradouroID).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
