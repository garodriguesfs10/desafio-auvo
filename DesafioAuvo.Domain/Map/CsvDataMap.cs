using CsvHelper.Configuration;
using DesafioAuvo.Domain.Entities;

namespace DesafioAuvo.Domain.Maps
{
    public class CsvDataMap : ClassMap<CsvData>
    {
        public CsvDataMap()
        {
            Map(e => e.Codigo).Name("Código");
            Map(e => e.Nome).Name("Nome");
            Map(e => e.ValorHora).Name("Valor hora");
            Map(e => e.DiaDoRegistro).Name("Data");
            Map(e => e.HorarioEntrada).Name("Entrada");
            Map(e => e.HorarioSaida).Name("Saída");
            Map(e => e.HorarioAlmoco).Name("Almoço");
        }
    }
}
