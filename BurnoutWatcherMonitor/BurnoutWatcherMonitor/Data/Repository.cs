using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using BurnoutWatcherMonitor.Models;

namespace BurnoutWatcherMonitor.Data
{
    /// <summary>
    /// Traz operações simples de persistência para a aplicação, lendo e gravando
    /// uma lista de <see cref="Employee"/> em um arquivo JSON local.
    /// </summary>
    public static class Repository
    {
        /// <summary>
        /// Retorna o caminho completo do arquivo JSON usado para persistência dos dados.
        /// O arquivo é localizado em <see cref="AppContext.BaseDirectory"/> e chama-se "employees.json".
        /// </summary>
        /// <returns>Caminho completo para o arquivo de dados.</returns>
        private static string GetPath()
        {
            return Path.Combine(AppContext.BaseDirectory, "employees.json");
        }

        /// <summary>
        /// Carrega a lista de colaboradores do arquivo JSON.
        /// Se o arquivo não existir, cria um conjunto seed de exemplo, grava no disco e o retorna.
        /// Em caso de erro de desserialização, retorna um seed alternativo para não quebrar a UI.
        /// </summary>
        /// <returns>Lista de <see cref="Employee"/> carregada do arquivo ou um seed padrão.</returns>
        public static List<Employee> LoadEmployees()
        {
            var path = GetPath();
            if (!File.Exists(path))
            {
                var seed = new List<Employee>
                {
                    new Employee { Name = "Alice" },
                    new Employee { Name = "Bruno" },
                    new Employee { Name = "Carla" }
                };
                SaveEmployees(seed);
                return seed;
            }

            var json = File.ReadAllText(path);
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var list = JsonSerializer.Deserialize<List<Employee>>(json, options);
                return list ?? new List<Employee>();
            }
            catch
            {
                return new List<Employee>
                {
                    new Employee { Name = "Seed1" },
                    new Employee { Name = "Seed2" }
                };
            }
        }

        /// <summary>
        /// Salva a lista fornecida de colaboradores no arquivo JSON usando formatação indentada.
        /// Substitui o conteúdo existente no arquivo.
        /// </summary>
        /// <param name="employees">Lista de <see cref="Employee"/> a ser persistida.</param>
        public static void SaveEmployees(List<Employee> employees)
        {
            var path = GetPath();
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(employees, options);
            File.WriteAllText(path, json);
        }
    }
}
