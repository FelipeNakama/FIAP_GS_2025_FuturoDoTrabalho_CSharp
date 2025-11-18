using System;

namespace BurnoutWatcherMonitor.Models
{
  
    // Representa um evento de alerta gerado pelo sistema quando um colaborador atinge um nível de stress que exige atenção.
    public class Alert
    {
        
        // Data e hora em que o alerta foi criado.    
        public DateTime Timestamp { get; set; }

        // Valor do stressScore associado a este alerta.
        public double StressScore { get; set; }

      
        // Nível categórico do alerta (por exemplo "Low", "Moderate", "High").
        public string Level { get; set; } = string.Empty;

      
        // Indica se o alerta já foi reconhecido/tratado por um usuário.        
        public bool Acknowledged { get; set; } = false;
    }
}
