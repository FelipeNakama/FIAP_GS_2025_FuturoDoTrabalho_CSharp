using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BurnoutWatcherMonitor.Models
{
    // Representa um colaborador monitorado pelo sistema, contendo histórico de leituras,thresholds de avaliação e alertas gerados.
    public class Employee
    {
       
        // Identificador único do colaborador.       
        public string Id { get; set; } = System.Guid.NewGuid().ToString();

        
        // Nome do colaborador
        public string Name { get; set; } = string.Empty;

        
        // Limiares (thresholds) usados para avaliar os níveis de stress deste colaborador
        public Thresholds Thresholds { get; set; } = new Thresholds();

       
        // Histórico de leituras de sensores associadas a este colaborador
        public List<SensorReading> Readings { get; set; } = new List<SensorReading>();

       
        // Lista de alertas gerados para este colaborador.      
        public List<Alert> Alerts { get; set; } = new List<Alert>();

        
        // Indica se há um alerta atualmente marcado como reconhecido de forma geral.     
        public bool IsAlertAcknowledged { get; set; } = false;

       
        ///Contador usado em memória para acompanhar ocorrências consecutivas do nível Moderate.     
        [JsonIgnore]
        public int ConsecutiveModerateCount { get; set; } = 0;
    }
}
