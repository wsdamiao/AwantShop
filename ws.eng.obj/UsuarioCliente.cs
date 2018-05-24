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
    }

    public class UsuarioCategoriaObj
    {
        public int ID { get; set; }        
        public string Nome { get; set; }
        public string Descr { get; set; }
        public bool Ativo { get; set; }        
    }

    public class ProjetoObj
    {
        public int ID { get; set; }
        public int ClienteID { get; set; }
        public int logradouroID { get; set; }
        public ClienteObj Cliente{ get; set; }
        public RegiaoProjeto Regiao { get; set; }
        public TipoProjeto Projeto { get; set; }
        public AreaProjeto Area { get; set; }
        public PadraoProjeto PadraoAcabamento { get; set; }
        public decimal? ValorMetroQuadradoAplicado { get; set; }        
        public decimal? AreaPersonalizada { get; set; }
        public List<ProjetoServicoObj> Servicos { get; set; }
        public DateTime DataCad { get; set; }
        public string TextoLivre { get; set; }
    }

    public class ProjetoServicoObj
    {
        public int ID { get; set; }
        public int ProjetoID { get; set; }
        public int ServicoID { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCad { get; set; }
        public int Status { get; set; }
        public string Descricao { get; set; }
        public int UsuarioID { get; set; }
        public UsuarioObj Usuario { get; set; }
        public FormaPgto FormaPagamento { get; set; }
        public ProjetoObj Projeto { get; set; }
        public decimal? T { get; set; }
        public decimal? A { get; set; }
        public decimal? P { get; set; }
        public decimal? d { get; set; }
    }

    
      
}
