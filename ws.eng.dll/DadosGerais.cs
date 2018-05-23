using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ws.eng.dao;

namespace ws.eng.obj
{
    public static class DadosGeraisDll
    {        

        public static decimal VALOR_METRO_QUADRADO()
        {
            return Convert.ToDecimal(BuscarValorDdadosGerais("VALOR_METRO_QUADRADO"));
        }

        private static string BuscarValorDdadosGerais(string item)
        {
            FinanceiroDao finDao = new FinanceiroDao();
            return finDao.ListarDadosGerais().Where(x => x.Nome == item).FirstOrDefault().Valor;
        }
    }
}
