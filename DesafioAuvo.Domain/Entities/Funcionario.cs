using DesafioAuvo.Domain.Helpers;
using System.Text.Json.Serialization;

namespace DesafioAuvo.Domain.Entities
{
    public class Funcionario
    {
        // podia ser feito no appsettings também        
        private static TimeSpan JORNADA_DE_TRABALHO = new TimeSpan(8, 0, 0);
        public Funcionario()
        {

        }
        public Funcionario(int codigo, string? nome)
        {
            Codigo = codigo;
            Nome = nome;
        }
        public int Codigo { get; set; }
        public string? Nome { get; set; }
        public decimal TotalReceber { get; set; }
        public decimal TotalDescontos { get; set; }
        [JsonIgnore]
        public decimal TotalExtras { get; set; }
        public string? HorasExtras { get; set; }
        public string? HorasDebito { get; set; }
        public string? DiasFalta { get; set; }
        public int DiasTrabalhados { get; set; }
        public int DiasExtras { get; set; }

        public static List<Funcionario> CalcularValores(List<CsvData> funcs)
        {
            var funcionarios = funcs.GroupBy(x => x.Codigo).Select(grp => grp.ToList()).ToList();

            var calculoFuncionarios = new List<Funcionario>();
            foreach (var funcionario in funcionarios)
            {
                var funcionarioCalculado = CalcularUnicoFuncionario(funcionario);
                calculoFuncionarios.Add(funcionarioCalculado);
            }

            return calculoFuncionarios;
        }

        private static Funcionario CalcularUnicoFuncionario(List<CsvData> dadosFuncionario)
        {
            var dadoFuncionario = dadosFuncionario.FirstOrDefault();
            var funcionario = new Funcionario(dadoFuncionario.Codigo, dadoFuncionario.Nome);
            var valorHoraString = dadoFuncionario.ValorHora.Split("R$")[1].Trim();
            var valorHoraBase = Decimal.Parse(valorHoraString);
            List<string> diasExtras;
            TimeSpan horasTrabalhadas, horasExtras, horasDebito;
            decimal totalReceber, totalExtras, totalDescontos;
            CriarVariaveisDeCalculo(out diasExtras, out horasTrabalhadas, out horasExtras, out horasDebito, out totalReceber, out totalExtras, out totalDescontos);

            foreach (var dado in dadosFuncionario)
            {
                var horaDeAlmoco = HorarioHelper.ObterDiferencaDeHorarioStringUnica(dado.HorarioAlmoco);
                var valorHoraDia = ObterValorHoraDoDia(dado.ValorHora);
                var horasTrabalhadasDia = dado.HorarioSaida - dado.HorarioEntrada - horaDeAlmoco;
                totalReceber += (decimal)horasTrabalhadasDia.TotalHours * valorHoraDia;

                if (horasTrabalhadasDia > JORNADA_DE_TRABALHO)
                {
                    var horasExtrasDia = horasTrabalhadasDia - JORNADA_DE_TRABALHO;
                    horasExtras += horasExtrasDia;
                    totalExtras += (decimal)horasExtrasDia.TotalHours * valorHoraDia;
                    if (!diasExtras.Contains(dado.DiaDoRegistro.ToShortDateString()))
                        diasExtras.Add(dado.DiaDoRegistro.ToShortDateString());
                }
                if (horasTrabalhadasDia < JORNADA_DE_TRABALHO)
                {
                    var horasDebitoDia = JORNADA_DE_TRABALHO - horasTrabalhadasDia;
                    horasDebito += horasDebitoDia;
                    totalDescontos += (decimal)horasDebitoDia.TotalHours * valorHoraDia;
                }

            }

            funcionario.DiasTrabalhados = dadosFuncionario.Select(o => o.DiaDoRegistro).Distinct().Count();
            var diasFaltantes = DateTime.DaysInMonth(dadoFuncionario.AnoVigencia, dadoFuncionario.DiaDoRegistro.Month) - funcionario.DiasTrabalhados;
            funcionario.DiasExtras = diasExtras.Count;
            funcionario.DiasFalta = diasFaltantes > 0 ? $"-{diasFaltantes}" : diasFaltantes.ToString();
            var totalHorasEmDebitoDiasFaltantes = new TimeSpan(diasFaltantes * JORNADA_DE_TRABALHO.Hours, 0, 0);
            funcionario.TotalReceber = totalReceber;
            funcionario.TotalExtras = totalExtras;
            funcionario.TotalDescontos = totalDescontos + (diasFaltantes * JORNADA_DE_TRABALHO.Hours) * valorHoraBase;
            funcionario.HorasExtras = HorarioHelper.ConverterTimeSpanParaHorarioString(horasExtras, true);
            funcionario.HorasDebito = HorarioHelper.ConverterTimeSpanParaHorarioString(horasDebito.Add(totalHorasEmDebitoDiasFaltantes));

            return funcionario;
        }

        private static void CriarVariaveisDeCalculo(out List<string> diasExtras, out TimeSpan horasTrabalhadas, out TimeSpan horasExtras, out TimeSpan horasDebito, out decimal totalReceber, out decimal totalExtras, out decimal totalDescontos)
        {
            diasExtras = new List<string>();
            horasTrabalhadas = new TimeSpan();
            horasExtras = new TimeSpan();
            horasDebito = new TimeSpan();
            totalReceber = new decimal();
            totalExtras = new decimal();
            totalDescontos = new decimal();
        }

        private static Decimal ObterValorHoraDoDia(string? valorHora)
        {
            var valorHoraString = valorHora.Split("R$")[1].Trim();
            var valor = Decimal.Parse(valorHoraString);
            return valor;
        }
    }
}
