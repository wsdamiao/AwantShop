using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ws.eng.obj;

namespace ws.eng.dao
{
    public class ContatoDao
    {
        ProEngEntities ProEng;

        public ContatoDao()
        {
            ProEng = new ProEngEntities();
        }

        #region Infra
        private MensagemClienteObj ConverterObj(MensagemCliente obj)
        {
            MensagemClienteObj objOut = new MensagemClienteObj();

            objOut.ID = obj.ID;            
            objOut.Mensagem = obj.Mensagem;
            objOut.Nome = obj.Nome;
            objOut.Telefone = obj.Telefone;
            objOut.Email = obj.Email;

            if (obj.DtLeitura != null)
                objOut.DtLeitura = obj.DtLeitura.Value;

            if (obj.IdUsuario != null)
                objOut.UsuarioID = obj.IdUsuario.Value;

            objOut.DtEnvio = obj.DtEnvio;
            return objOut;
        }

        private MensagemCliente ConverterObj(MensagemClienteObj obj)
        {
            MensagemCliente objOut = new MensagemCliente();

            objOut.ID = obj.ID;
            objOut.Mensagem = obj.Mensagem;
            objOut.Nome = obj.Nome;
            objOut.Telefone = obj.Telefone;
            objOut.Email = obj.Email;

            if (obj.DtLeitura != null)
                objOut.DtLeitura = obj.DtLeitura.Value;

            if (obj.UsuarioID != null)
                objOut.IdUsuario = obj.UsuarioID.Value;

            objOut.DtEnvio = obj.DtEnvio;
            return objOut;
        }

        private List<MensagemClienteObj> PopularListaObj(List<MensagemCliente> obj)
        {
            List<MensagemClienteObj> objOut = new List<MensagemClienteObj>();
            foreach (var item in obj)
            {
                objOut.Add(ConverterObj(item));
            }
            return objOut;
        }

        public List<MensagemClienteObj> Listar()
        {
            return PopularListaObj(ProEng.MensagemClientes.ToList());
        }

        public void Salvar(MensagemClienteObj obj)
        {
            try
            {
                ProEng.MensagemClientes.Add(ConverterObj(obj));
                ProEng.SaveChanges();

            }catch(Exception ex)
            {
                throw ex;
            }
            
        }
        #endregion
    }
}
