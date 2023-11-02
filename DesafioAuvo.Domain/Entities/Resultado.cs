using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DesafioAuvo.Domain.Entities
{
    public class Resultado
    {
        public Resultado(string? departamento, string? mesVigencia, int anoVigencia, int? mesVigenciaInt)
        {
            Departamento = departamento;
            MesVigencia = mesVigencia;
            AnoVigencia = anoVigencia;
            MesVigenciaInt = mesVigenciaInt;
        }

        public Resultado()
        {

        }

        public string? Departamento { get; set; }
        public string? MesVigencia { get; set; }
        [JsonIgnore]
        public int? MesVigenciaInt { get; set; }
        public int AnoVigencia { get; set; }
        public decimal? TotalPagar { get; set; } = 0.00m;
        public decimal? TotalDescontos { get; set; } = 0.00m;
        public decimal? TotalExtras { get; set; } = 0.00m;
        public List<Funcionario>? Funcionarios { get; set; }

        public static List<Resultado> CalculateResult(List<CsvData> dadosCsv)
        {
            var departaments = dadosCsv.GroupBy(x => new { x.Departamento, x.MesVigencia }).Select(grp => grp.ToList()).ToList();
            var finalResult = new List<Resultado>();
            foreach (var departament in departaments)
            {
                var result = BuildResultDepartament(departament);
                finalResult.Add(result);
            }


            return finalResult;
        }

        private static Resultado BuildResultDepartament(List<CsvData> departamentoRecebido)
        {
            var departamentInfo = departamentoRecebido.FirstOrDefault();
            var departamento = new Resultado(departamentInfo.Departamento, departamentInfo.MesVigencia, departamentInfo.AnoVigencia, departamentInfo.MesVigenciaInt);
            departamento.Funcionarios = Funcionario.CalcularValores(departamentoRecebido);

            departamento.TotalPagar = departamento.Funcionarios.Sum(x => x.TotalReceber);
            departamento.TotalExtras = departamento.Funcionarios.Sum(x => x.TotalExtras);
            departamento.TotalDescontos = departamento.Funcionarios.Sum(x => x.TotalDescontos);

            return departamento;
        }
    }
}
