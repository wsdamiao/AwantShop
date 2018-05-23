using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ws.eng.obj
{
    public static class DadosProjeto
    {
        public static string CRIPTOGRAFIA = "CuPuLa";
        public static string SENHAPADRAO = "123456";
        public static int TAMANHOSENHAPADRAO = 8;
        public static string EMAILDOSISTEMA = "sistemas@wallacedamiao.com";
        public static string EMAILDOSISTEMASENHA = "senha@senha@123";
        public static string EMAILDOSISTEMASERVIDOR = "webmail.wallacedamiao.com";
        public static bool EMAILSSL = false;
        public static int EMAILPORTA = 143;
    }
        
    public static class EnumObj
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            if (fi != null)
            {
                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

                if (attributes != null &&
                    attributes.Length > 0)
                    return attributes[0].Description;
                else
                    return value.ToString();
            }

            return value.ToString();

        }
    }

    public enum RegiaoProjeto
    {
        [Description("Portugal")]
        Portugal = 1,
        [Description("Rio de Janeiro")]
        RioDeJaneiro = 2,
        [Description("Restante do Brasil")]
        Brasil = 3
    }

    public enum TipoProjeto
    {
        [Description("Residencial")]
        Residencial = 1,
        [Description("Comercial")]
        Comercial = 2,
        [Description("Industrial")]
        Industrial = 3,
        [Description("Personalizado")]
        Personalizado = 0
    }

    public enum ServicosProjeto
    {
        [Description("Projeto Arquitetônico")]
        ProjetoArquitetonico = 10,

        [Description("Projeto Elétrico")]
        ProjetoEletrico = 11,

        [Description("Projeto Sanitário")]
        ProjetoHidroSanitario = 12,

        [Description("Projeto Executivo")]
        ProjetoExecutivo = 13,

        [Description("Outros")]
        Outro = 0
    }

    public enum AreaProjeto
    {
        [Description("Pequena")]
        Pequeno = 4,
        [Description("Média")]
        Medio = 5,
        [Description("Grande")]
        Grande = 6,
        [Description("Personalizado")]
        Personalizado = 0
    }

    public enum PadraoProjeto
    {
        [Description("Baixo Custo")]
        BaixoCusto = 7,
        [Description("Médio Custo")]
        MedioCusto = 8,
        [Description("Alto Padrão")]
        AltoCusto = 9,
        [Description("Personalizado")]
        Personalizado = 0
    }

    public enum FormaPgto
    {
        [Description("Depósito em Conta Corrente")]
        DepositoBancario = 1,
        [Description("Boleto Bancário")]
        Boleto = 2,
        [Description("Cartão de Crédito")]
        Cartao = 3,
        [Description("Aplicativo")]
        Aplicativo = 4
    }

    public enum CategoriaUsuario
    {
        [Description("Cliente")]
        Cliente = 5,
        [Description("Colaborador")]
        Colaborador = 4,
        [Description("Consultor")]
        Operador = 3,
        [Description("Gestor")]
        Gestor = 2,
        [Description("Administrador")]
        Administrador = 1
    }

    public enum StatusServico
    {
        [Description("Serviço Criado. Em fila de espera")]
        Criado = 1,
        [Description("Serviço lido pelo operador")]
        Lido = 2,
        [Description("Tentando contactar cliente/Aguardando Retorno")]
        Contato = 3,
        [Description("Serviço Iniciado")]
        Iniciado = 4,
        [Description("Enviado para o cliente")]
        EnviadoCliente = 5,
        [Description("Cliente solicita Revisão")]
        SolicitadaRevisão = 6,
        [Description("Serviço Iniciado")]
        RevisãoFinalizada = 7
    }

    public enum StatusLoginAcesso
    {
        Sucesso = 0,
        Bloqueado = 1,
        EnviarSenha = 2,
        Falha = 3
    }    

}

