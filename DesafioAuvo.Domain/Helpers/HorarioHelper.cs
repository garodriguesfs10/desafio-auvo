namespace DesafioAuvo.Domain.Helpers
{

    public static class HorarioHelper
    {
        public static string ConverterTimeSpanParaHorarioString(TimeSpan totalHours, bool horasExtras = false)
        {
            var horas = totalHours.Days * 24 + totalHours.Hours;
            var minutos = totalHours.Minutes;
            var segundos = totalHours.Seconds;
            if (!horasExtras && (horas > 0 || minutos > 0))
                return  $"-{horas}:{minutos}:{segundos}";
            return $"{horas}:{minutos}:{segundos}";
        }

        public static TimeSpan ObterDiferencaDeHorarioStringUnica(string? horario)
        {
            horario = horario.Replace(" ", "");
            var horarioInicial = Convert.ToDateTime(horario.Split('-')[0]);
            var horarioFinal = Convert.ToDateTime(horario.Split('-')[1]);
            TimeSpan diferenca = horarioFinal - horarioInicial;
            return diferenca;
        }
    }
}
