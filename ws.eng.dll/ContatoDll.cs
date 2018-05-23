using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ws.eng.dao;
using ws.eng.obj;

namespace ws.eng.dll
{
    public class ContatoDll
    {
        ContatoDao dao;

        public ContatoDll()
        {
            dao = new ContatoDao();
        }

        public List<MensagemClienteObj> Listar()
        {
            return dao.Listar();
        }

        public MensagemClienteObj Buscar (int Id)
        {
            return dao.Listar().Where(x => x.ID == Id).FirstOrDefault();
        }

        public List<MensagemClienteObj> ListarPorUsuario(int Id)
        {
            return dao.Listar().Where(x => x.UsuarioID == Id).ToList();
        }

        public void Salvar(MensagemClienteObj obj)
        {
            dao.Salvar(obj);
        }
    }

}
