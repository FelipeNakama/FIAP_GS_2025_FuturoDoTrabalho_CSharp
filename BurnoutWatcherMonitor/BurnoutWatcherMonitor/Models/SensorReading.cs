using System;

namespace BurnoutWatcherMonitor.Models
{
    // DTO que representa uma leitura agregada de sensores para um colaborador em um instante.
    public class SensorReading
    {
        
        // Identificador do colaborador a que esta leitura pertence
        public string EmployeeId { get; set; } = string.Empty;

      
        // Data e hora da leitura.
        public DateTime Timestamp { get; set; }


        /// Score derivado da análise facial (0–100)
        public double FacialScore { get; set; }

        
        // Score derivado da postura (0–100)
        public double PostureScore { get; set; }

        
        // Score derivado da atividade (0–100)
        public double ActivityScore { get; set; }
    }
}
