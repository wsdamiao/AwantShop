using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ws.eng.dll
{
    public class LogDeErro
    {
        public LogDeErro()
        {

        }

        public LogDeErro(Exception ex)
        {
            RegistrarLog(ex);
        }

        public bool RegistrarLog(Exception ex)
        {
            return true;
        }
    }
}
