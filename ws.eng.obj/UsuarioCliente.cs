using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ws.eng.obj
{
    public class ClienteObj
    {
        public int ID { get; set; }
        public int UsuarioID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string TelContato1 { get; set; }
        public string TelContato2 { get; set; }
        public string TelContato3 { get; set; }
        public string CPF_CNPJ { get; set; }
        public UsuarioObj Usuario { get; set; }
        public Endereco Endereco { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }        
    }

    public class Endereco
    {
        public string Rua { get; set; }
        public string CEP { get; set; }
        public LogradouroObj Logradouro { get; set; }
    }

    public class UsuarioObj
    {
        public int ID { get; set; }
        public int CategoriaID { get; set; }        
        public string NomeUsuario { get; set; }
        public string NomeCompleto { get; set; }                
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        public Guid Token { get; set; }
        public ClienteObj Cliente { get; set; }
        public UsuarioCategoriaObj Categoria { get; set; }        
        public string CodigoValidacao { get; set; }
    }

    public class UsuarioCategoriaObj
    {
        public int ID { get; set; }        
        public string Nome { get; set; }
        public string Descr { get; set; }
        public bool Ativo { get; set; }        
    }

        
      
}
