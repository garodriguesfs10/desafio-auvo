using CsvHelper;
using CsvHelper.Configuration;
using DesafioAuvo.Application.Services.Interfaces;
using DesafioAuvo.Domain.Constants;
using DesafioAuvo.Domain.Entities;
using DesafioAuvo.Domain.Maps;
using System.Collections.Generic;
using System.Globalization;

namespace DesafioAuvo.Application.Services.Implementations
{
    public class CsvService : ICsvService
    {

        CsvConfiguration _csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ";"
        };

        private List<CsvData> _results = new List<CsvData>();
        public async Task<List<Resultado>> ProcesseCsvAsync(string path)
        {
            List<string> avaliableCsvs = ObterArquivosCsvs(path);
            var validCsvs = ObterCsvsValidos(avaliableCsvs);
            var resultado = new List<Resultado>();

            if (validCsvs.Count > 0)
            {
                try
                {
                    resultado = await ProcessarData(validCsvs, path);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return resultado;
        }

        private static List<string> ObterArquivosCsvs(string path)
        {
            try
            {
                if (Directory.Exists(path))
                    return Directory.GetFiles(path, "*.csv").Select(file => Path.GetFileName(file)).ToList();
                throw new Exception(message: "Diretório de arquivo não encontrado");
            }
            catch (Exception)
            {
                throw;
            }

        }

        private static List<InformacoesAdicionaisCsv> ObterCsvsValidos(List<string> csvNames)
        {
            var csvsValidos = new List<InformacoesAdicionaisCsv>();

            foreach (var csvName in csvNames)
            {
                var dep = csvName.Split("-")[0];
                if (!Constants.Departamentos.Select(x => x.ToUpper()).Contains(dep.ToUpper())) continue;

                var mes = csvName.Split("-")[1];
                if (!Constants.Mes.ContainsKey(mes.ToUpper())) continue;

                var anoString = csvName.Split("-")[2].Split(".csv")[0].Trim();
                bool anoEhNumero = int.TryParse(anoString, out int year);
                if (anoString.Length != 4 || !anoEhNumero) continue;

                Constants.Mes.TryGetValue(mes, out var mesVigenciaInt);
                csvsValidos.Add(new InformacoesAdicionaisCsv(dep, mes, year, csvName, mesVigenciaInt));
            }

            return csvsValidos;
        }

        private async Task<List<Resultado>> ProcessarData(List<InformacoesAdicionaisCsv> validCsvs, string path)
        {
            var tasks = new List<Task>();
            foreach (var csv in validCsvs)
            {
                tasks.Add(Task.Run(() =>
                {
                    var employees = GetCsvData(csv, path);
                    lock (_results)
                        _results.AddRange(employees);
                }
                ));
            }

            await Task.WhenAll(tasks);

            var resultado = Resultado.CalculateResult(_results).
                                      OrderBy(x => x.Departamento).
                                      ThenBy(x => x.AnoVigencia).
                                      ThenBy(x => x.MesVigenciaInt).ToList();

            return resultado;
        }

        private List<CsvData> GetCsvData(InformacoesAdicionaisCsv csvInfo, string path)
        {
            if (!path.EndsWith(@"\"))
                path += @"\";

            using (var reader = new StreamReader(path + csvInfo.Filename))
            using (var csv = new CsvReader(reader, _csvConfiguration))
            {
                csv.Context.RegisterClassMap<CsvDataMap>();
                var employees = csv.GetRecords<CsvData>().ToList();
                FillAdditionalData(employees, csvInfo);
                return employees;
            }
        }

        private static List<CsvData> FillAdditionalData(List<CsvData> employees, InformacoesAdicionaisCsv info)
        {
            foreach (var employee in employees)
            {
                employee.Departamento = info.Departamento;
                employee.AnoVigencia = info.AnoVigencia;
                employee.MesVigencia = info.MesVigencia;
                employee.MesVigenciaInt = info.MesVigenciaInt;
            }
            return employees;
        }
    }
}
