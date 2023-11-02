using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAuvo.Domain.Constants
{
    public static class Constants
    {
        private static readonly ReadOnlyCollection<string> _departamentos = new ReadOnlyCollection<string>(new[]
        {
            "RH",
            "Compras",
            "Departamento de Operações Especiais",
            "TI"
        });
        public static ReadOnlyCollection<string> Departamentos
        {
            get { return _departamentos; }
        }

        private static readonly Dictionary<string, int> _mes = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase)
        {
            {"Janeiro", 1},
            {"Fevereiro", 2},
            {"Março", 3},
            {"Abril", 4},
            {"Maio", 5},
            {"Junho", 6},
            {"Julho", 7},
            {"Agosto", 8},
            {"Setembro",9},
            {"Outubro", 10},
            {"Novembro",11},
            {"Dezembro",12 }
        };

        public static Dictionary<string, int> Mes
        {
            get { return _mes; }
        }
    }
}
