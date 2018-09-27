using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ws.eng.obj;
using System.ComponentModel.DataAnnotations;


namespace ws.web.eng.Models
{
    public enum Paginas
    {
        Area,
        Formulario,
        Index,
        Municipio,
        Padrao,
        Projeto,
        Regiao,
        Resumo,
        Servicos,
        Personalizado,
        Login,
        Confirmarcadastro,
        Encerramento
    }

    [Serializable]
    public class SimuladorDeProjetoViewModel
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        [Display(Name = "Região")]
        public RegiaoProjeto Regiao { get; set; }

        [Display(Name = "Tipo de Projeto")]
        public TipoProjeto Projeto { get; set; }

        [Display(Name = "Área")]
        public AreaProjeto Area { get; set; }

        [Display(Name = "Tipo de Acabamento")]
        public PadraoProjeto PadraoAcabamento { get; set; }

        [Display(Name = "Área Personalizada")]
        public decimal AreaPersonalizada { get; set; }

        public List<ServicoAux> ServicosEngenharia { get; set; }
        public List<ServicoAux> ServicosArquitetura { get; set; }
                
        public decimal ValorProjetoTotal { get; set; }               
        public string ValorProjetoTotalFormatado { get; set; }  
        
        [Required]
        [Display(Name = "Endereço completo do empreendimento")]
        public string EnderecoCompletoEmpreendimento { get; set; }
        
        
        [Required]
        [Display(Name = "País")]
        public string ClientePais { get; set; }

        [Required]
        [Display(Name = "Estado (Sigla)")]
        public string ClienteEstado { get; set; }

        [Required]
        [Display(Name = "Cidade")]
        public string ClienteCidade { get; set; }

        public FormaPgto FormaPagamento { get; set; }
        public string CodMunicipio { get; set; }
        public int CidadeID { get; set; }

        [Display(Name = "Valor do Metro Quadrado")]
        public decimal ValorMetroQuadradoAplicado { get; set; }

        public IEnumerable<SelectListItem> Municipios { get; set; }        

        [Required]
        [Key]
        [Display(Name = "CNPJ/CPF (Sem pontos)(*)")]
        public string CpfCnpj { get; set; }
        [Required]
        [Display(Name = "Nome(*)")]
        public string Nome { get; set; }
        [Required]
        [Display(Name = "Telefone de contato Preferencial(*)")]
        public string TelContato1 { get; set; }
        [Display(Name = "Outro telefone de contato")]
        public string TelContato2 { get; set; }
        [Display(Name = "Outro telefone de contato")]
        public string TelContato3 { get; set; }
        [Required]
        [Display(Name = "E-Mail(*)")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Descreva em até 250 caracteres o que você deseja em seu projeto")]
        public string TextoLivre { get; set; }
    }

    [Serializable]
    public class ServicoAux
    {
        public int ServicoID { get; set; }
        public int ProjetoServicoID { get; set; }
        public string Codigo { get; set; }
        public bool Selecionado { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public string ValorFormatado { get; set; }
    }

    [Serializable]
    public class EmailModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe seu nome")]
        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Informe seu e-mail")]
        [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Informe seu telefone de contato")]
        [RegularExpression(@"^(?:(?:\+|00)?(55)\s?)?(?:\(?([1-9][0-9])\)?\s?)?(?:((?:9\d|[2-9])\d{3})\-?(\d{4}))$", ErrorMessage = "Informe um telefone válido")]
        public string Telefone { get; set; }

        [Display(Name = "Mensagem")]
        [Required(ErrorMessage = "Descreva a sua necessidade")]
        public string Mensagem { get; set; }
    }

    [Serializable]
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Display(Name = "Usuário (de 4 a 8 caracteres)")]
        [StringLength(8, MinimumLength = 4)]
        [Required(ErrorMessage = "Informe o nome do usuário")]
        public string NomeUsuario { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string NomeCompleto { get; set; }
                
        public IEnumerable<SelectListItem> Categorias { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Informe uma categoria")]
        public int CategoriaID { get; set; }
    }

    [Serializable]
    public class UsuarioModelSenha
    {
        public int Id { get; set; }

        [Display(Name = "Usuário (de 4 a 8 caracteres)")]        
        public string NomeUsuario { get; set; }
                
        [Display(Name = "Nome")]
        public string NomeCompleto { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 4)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 4)]
        [Display(Name = "Repetir Senha")]
        public string RepetirSenha { get; set; }
        
    }

    [Serializable]
    public class DistanciaRodoviariaViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "Informe o munícipio")]
        public int LogradouroID { get; set; }

        [Display(Name = "Cidade")]        
        public string NomeCidade { get; set; }

        public IEnumerable<SelectListItem> Logradouros { get; set; }

        [Display(Name = "Valor Percentual")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Informe o valor percentual")]
        public decimal Valor { get; set; }

        [Display(Name = "Distância Rodoviária")]
        [Required(ErrorMessage = "Informe a distância rodoviária do marco zero")]
        public decimal DistanciaRodoviaria { get; set; }

        [Display(Name = "Valor Base")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal ValorBase { get; set; }
    }

    [Serializable]
    public class PaisViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o pais")]
        public string NomePais { get; set; }

        [Display(Name = "Nome Oficial")]
        [Required(ErrorMessage = "Informe o nome oficial do pais")]
        public string NomeOficial { get; set; }        

        [Display(Name = "Valor padrão do metro quadrado")]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Required(ErrorMessage = "Informe o valor do metro quadrado")]
        public decimal Valor { get; set; }

        [Display(Name = "Moeda")]
        public int MoedaID { get; set; }

        public IEnumerable<SelectListItem> Moedas { get; set; }

        public string NomeMoeda { get; set; }
    }

    public class ConfirmarCadastroModel
    {
        public string CodigoConfirmacao { get; set; }
        public string Cpf_Cnpj { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public UsuarioObj Usu { get; set; }
    }
}