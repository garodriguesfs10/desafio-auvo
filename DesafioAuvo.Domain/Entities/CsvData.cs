namespace DesafioAuvo.Domain.Entities
{
    public class CsvData : InformacoesAdicionaisCsv
    {
        public CsvData()
        {
                
        }

        public CsvData(string? departament, string? currentMonth, int currentYear, string? filename)
        {
            Departamento = departament;
            MesVigencia = currentMonth;
            AnoVigencia = currentYear;
            Filename = filename;
        }
        public int Codigo { get; set; }
        public string? Nome { get; set; }
        public string? ValorHora { get; set; }
        public DateTime DiaDoRegistro { get; set; }
        public TimeSpan HorarioEntrada { get; set; }
        public TimeSpan HorarioSaida { get; set; }
        public string? HorarioAlmoco { get; set; }

        
    }
}
