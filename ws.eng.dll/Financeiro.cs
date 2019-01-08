using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ws.eng.dao;
using ws.eng.obj;

namespace ws.eng.dll
{
    public class FinanceiroDll
    {
        FinanceiroDao finDao;

        private decimal t;
        public decimal T
        {
            get
            {
                return t;
            }
        }

        private decimal a;
        public decimal A
        {
            get
            {
                return a;
            }
        }

        private decimal p;
        public decimal P
        {
            get
            {
                return p;
            }
        }

        private decimal d;
        public decimal D
        {
            get
            {
                return d;
            }
        }

        public FinanceiroDll()
        {
            finDao = new FinanceiroDao();
        }

        public decimal BuscarValorMetroQuadrado(RegiaoProjeto regiao)
        {            
            LogradouroDao logDao = new LogradouroDao();
            PaisObj pais = logDao.ListarPais().Where(x => x.ID == (int)regiao).FirstOrDefault();
            
            return pais.MetroQuadrado;
        }

        public decimal BuscarValor(TipoProjeto tipo, RegiaoProjeto regiao)
        {
            return finDao.Buscarvalor((int)tipo, (int)regiao);
        }

        public decimal BuscarValor(AreaProjeto area, RegiaoProjeto regiao)
        {
            return finDao.Buscarvalor((int)area, (int)regiao);
        }

        public decimal BuscarValor(PadraoProjeto padrao, RegiaoProjeto regiao)
        {
            return finDao.Buscarvalor((int)padrao, (int)regiao);
        }

        public decimal BuscarValor(int idCidade)
        {
            if (idCidade == 0)
                return 0;

            return finDao.Buscarvalor(idCidade);
        }

        public decimal Calcular(TipoProjeto tipo,
                                AreaProjeto area,
                                RegiaoProjeto regiao,
                                PadraoProjeto padrao,                                
                                int idCidade = 0,
                                bool ProjetoArquitetonico = false)
        {
            decimal vlMetroQ, vlPercentual = 0;

            t = this.BuscarValor(tipo, regiao);
            a = this.BuscarValor(area, regiao);
            p = this.BuscarValor(padrao, regiao);
            d = this.BuscarValor(idCidade);

            vlMetroQ = this.BuscarValorMetroQuadrado(regiao);
            
            if(ProjetoArquitetonico)
            {
                vlPercentual = T * A * P;
            }
            else
            {
                vlPercentual = T * A * P * d;
            }            

            return vlMetroQ * vlPercentual;
        }


    }
}