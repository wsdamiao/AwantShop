using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ws.eng.obj;
using System.ComponentModel.DataAnnotations;


namespace ws.web.eng.Models
{
    [Serializable]
    public class ProjetoModel
    {
        [Display(Name = "Região")]
        public RegiaoProjeto Regiao { get; set; }

        [Display(Name = "Tipo de Projeto")]
        public TipoProjeto Projeto { get; set; }

        [Display(Name = "Área")]
        public AreaProjeto Area { get; set; }

        [Display(Name = "Tipo de Acabamento")]
        public PadraoProjeto PadraoAcabamento { get; set; }  
        
        public decimal AreaPersonalizada { get; set; }

        public decimal ValorProjetoArquitetonico { get; set; }
        public decimal ValorProjetoEletrico { get; set; }
        public decimal ValorProjetoHidroSanitario { get; set; }
        public decimal ValorProjetoExecutivo { get; set; }
        public decimal ValorProjetoOutro { get; set; }
        public decimal ValorProjetoTotal { get; set; }

        public string ValorProjetoArquitetonicoFormatado { get; set; }
        public string ValorProjetoEletricoFormatado { get; set; }
        public string ValorProjetoHidroSanitarioFormatado { get; set; }
        public string ValorProjetoExecutivoFormatado { get; set; }
        public string ValorProjetoOutroFormatado { get; set; }
        public string ValorProjetoTotalFormatado { get; set; }

        public FormaPgto FormaPagamento { get; set; }
        public string CodMunicipio { get; set; }
        public int CidadeID { get; set; }

        [Display(Name = "Valor do Metro Quadrado")]
        public decimal ValorMetroQuadradoAplicado { get; set; }

        public IEnumerable<SelectListItem> Municipios { get; set; }

        [Display(Name = "Projeto Arquitetônico")]
        public bool ProjetoArquitetonico { get; set; }

        [Display(Name = "Projeto Hidraulico/Sanitário")]
        public bool ProjetoHidraSanitario { get; set; }

        [Display(Name = "Projeto Eletrico")]
        public bool ProjetoEletrico { get; set; }

        [Display(Name = "Projeto Executivo")]
        public bool ProjetoExecutivo { get; set; }

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
        [Display(Name = "Descrição Complementar")]
        public string TextoLivre { get; set; }
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
}