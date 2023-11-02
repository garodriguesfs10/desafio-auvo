using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAuvo.Domain.Entities
{
    public class InformacoesAdicionaisCsv
    {
        public InformacoesAdicionaisCsv()
        {
     
        }
        public InformacoesAdicionaisCsv(string? departament, string? currentMonth, int currentYear, string? filename, int mesVigenciaInt)
        {
            Departamento = departament;
            MesVigencia = currentMonth;
            AnoVigencia = currentYear;
            Filename = filename;
            MesVigenciaInt = mesVigenciaInt;
        }

        public string? Departamento { get; set; }
        public string? MesVigencia { get; set; }
        public int AnoVigencia { get; set; }
        public int MesVigenciaInt { get; set; }
        public string? Filename { get; set; }
    }
}
