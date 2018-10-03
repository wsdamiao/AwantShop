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
        public static string EMAILDOSISTEMASERVIDOR = "localhost ";
        public static bool EMAILSSL = false;
        public static int EMAILPORTA = 25;
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
        Portugal = 2,
        [Description("Rio de Janeiro")]
        RioDeJaneiro = 3,
        [Description("Restante do Brasil")]
        Brasil = 1
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
        [Description("Serviço Criado. Aguardando análise")]
        Criado = 1,
        [Description("Serviço analisado pelo Consultor")]
        AnaliseConsultor = 2,
        [Description("Tentando contactar cliente/Sem sucesso")]
        ContatoSemSucesso = 3,
        [Description("Contato efetuado com o cliente")]
        ContatoComSucesso = 4,
        [Description("Serviço Iniciado / Encaminhado para área técnica")]
        Iniciado = 5,
        [Description("Área Técnica necessita de informaçõess complementares")]
        InformacaoComplementar = 6,
        [Description("Obtida as informaçõess complementares / Encaminhado Área Técnica")]
        RespostaInformacaoComplementar = 7,
        [Description("Serviço Concluído pela Área Técnica")]
        ServicoConcluidoAreaTecnica = 8,
        [Description("Enviado para cliente")]
        EnviadoCliente = 9,
        [Description("Cliente solicita Revisão")]
        ClienteSolicitaRevisao = 10,
        [Description("Revisão concluída")]
        RevisaoConcluida = 11,
        [Description("Revisao finalizada")]
        RevisaoFinalizada = 12,
        [Description("Servico Finalizado")]
        ServicoFinalizado = 13,
        [Description("Servico Cancelado pelo cliente")]
        ServicoCanceladoCliente = 14,
        [Description("Servico em Andamento")]
        ServicoEmAndamento = 15,
        [Description("Servico Parado")]
        ServicoParado = 16,
    }

    public enum StatusLoginAcesso
    {
        Sucesso = 0,
        Bloqueado = 1,
        EnviarSenha = 2,
        Falha = 3
    }    

}

