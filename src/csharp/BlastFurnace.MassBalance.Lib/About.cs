using System.Text;

namespace BlastFurnace.MassBalance.Lib
{
    /// <summary>
    /// Contains description and other metadata of BlasFurnace Mass Balance library/application
    /// </summary>
    public static class About
    {
        private static readonly string paragraph1 = "Este programa realiza um cálculo de carga simplificado para um alto forno," +
                                                "feito com base nos exercícios feitos em sala de aula e, também, com base nos" +
                                                "conhecimentos do grupo, tanto na montagem da linha de pensamento como na" +
                                                "montagem do programa usando a linguagem Pascal.";

        private static readonly string paragraph2 = "\n" + "O programa considera o carregamento de até 3 tipos de minérios" +
                                                "(estes podem ser os minérios, pelotas e sínteres).";

        private static readonly string paragraph3 = "\n" + "A partir de uma proporção em peso fixa, a carga de minérios é calculada." +
                                                "Calcula-se também o teor médio de ferro contido nela.";

        private static readonly string paragraph4 = "\n" + "A carga de coque é feita de forma similar: estipula-se a proporção dos" +
                                                "diferentes tipos de coque(no máximo 3) e calcula-se a massa de cada um deles" +
                                                "para atender ao processo.";

        private static readonly string paragraph5 = "\n" + "Pode-se calcular a quantidade de PCI máxima a se injetar, e a partir dela," +
                                                "estipular um valor a ser usado.";

        private static readonly string paragraph6 = "\n" + "O sopro pode ser calculado, considerando-se a necessidade de oxigênio e" +
                                                "o seu teor no sopro.";

        private static readonly string paragraph7 = "\n" + "Os cálculos são feitos com base nas considerações de que todo o ferro" +
                                                "carregado é reduzido e se incorpora ao gusa líquido e a reação de redução da" +
                                                "wustita a ferro se processam integralmente a 900 °C, de forma que a" +
                                                "reação de redução seja:";

        private static readonly string paragraph8 = "\n" + "FeO +3,12CO = Fe + 2,12 CO + CO2.";

        private static readonly string paragraph9 = "\n" + "Após o uso do programa, pode-se armazenar os dados em um arquivo texto" +
                                                "(Calculo_de_carga.doc)";

        private static string description = string.Empty;

        private static string GetDescription()
        {
            var sb = new StringBuilder();

            sb.AppendLine(paragraph1);
            sb.AppendLine(paragraph2);  
            sb.AppendLine(paragraph3);
            sb.AppendLine(paragraph4);
            sb.AppendLine(paragraph5);
            sb.AppendLine(paragraph6);
            sb.AppendLine(paragraph7);
            sb.AppendLine(paragraph8);
            sb.AppendLine(paragraph9);

            return sb.ToString();   
        }

        /// <summary>
        /// A textual description of the purpose of Blast Furnace mass balance library/application
        /// </summary>
        public static string Description 
        { 
            get 
            { 
                description = GetDescription();
                return description; 
            } 
        }
    }
}
