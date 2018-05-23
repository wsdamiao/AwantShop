using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ws.eng.obj;

namespace ws.eng.dao
{
    public class FinanceiroDao
    {
        ProEngEntities ProEng;

        public FinanceiroDao()
        {
            ProEng = new ProEngEntities();
        }

        #region Infra
        private ElementoObj ConverterObj(Elemento obj)
        {
            ElementoObj objOut = new ElementoObj();

            objOut.ID = obj.ID;
            objOut.Categoria = obj.Categoria;
            objOut.Descricao = obj.Descricao;
            objOut.Item = obj.Item;

            return objOut;
        }

        private ElementoValorObj ConverterObj(ElementoValor obj)
        {
            ElementoValorObj objOut = new ElementoValorObj();

            objOut.ID = obj.ID;
            objOut.ElementoID = obj.ElementoID;
            objOut.PaisID = obj.PaisID;
            objOut.Valor = obj.Valor;
            objOut.Vigenciafim = obj.VigenciaFim;

            return objOut;
        }

        private List<ElementoObj> PopularListaObj(List<Elemento> obj)
        {
            List<ElementoObj> objOut = new List<ElementoObj>();
            foreach (var item in obj)
            {
                objOut.Add(ConverterObj(item));
            }
            return objOut;
        }

        private List<ElementoValorObj> PopularListaObj(List<ElementoValor> obj)
        {
            List<ElementoValorObj> objOut = new List<ElementoValorObj>();
            foreach (var item in obj)
            {
                objOut.Add(ConverterObj(item));
            }
            return objOut;
        }
        
        public List<ElementoObj> ListarElementos()
        {
            return PopularListaObj(ProEng.Elementoes.ToList());
        }

        public List<ElementoValorObj> ListarElementoValor()
        {
            return PopularListaObj(ProEng.ElementoValors.ToList());
        }

        public List<Distancia> ListarDistancia()
        {
            return ProEng.Distancias.ToList();
        }

        public List<DadosGerai> ListarDadosGerais()
        {
            return ProEng.DadosGerais.ToList();
        }
        #endregion

        public decimal Buscarvalor(int idTipo, int idPais, int idCidade = 0)
        {
            return ListarElementoValor().Where(x => x.ElementoID == idTipo && x.PaisID == idPais && x.Vigenciafim == null).FirstOrDefault().Valor;
        }

        public decimal Buscarvalor(int idCidade)
        {
            var ValorDistancia = ListarDistancia().Where(x => x.LogradouroID == idCidade && x.VigenciaFim == null).FirstOrDefault();

            if (ValorDistancia != null)
                return ValorDistancia.Valor;
            else
                return 1;
        }
    }
}
