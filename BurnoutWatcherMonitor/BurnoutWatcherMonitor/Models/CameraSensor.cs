using System;

namespace BurnoutWatcherMonitor.Models
{
    /// <summary>
    /// Sensor que simula uma câmera capaz de estimar um escore facial (facial expression).
    /// Herda de <see cref="Sensor"/> e produz leituras com <see cref="FacialScore"/>.
    /// </summary>
    public class CameraSensor : Sensor
    {
       
        // Valor da avaliação facial (0–100) definido externamente para a simulação.   
        public double FacialScore { get; set; }

        /// <summary>
        /// Cria uma nova instância de <see cref="CameraSensor"/>.
        /// </summary>
        /// <param name="id">Identificador do sensor.</param>
        /// <param name="location">Localização física ou lógica do sensor.</param>
        public CameraSensor(string id, string location) : base(id, location)
        {
        }

        /// <summary>
        /// Gera uma leitura (<see cref="SensorReading"/>) contendo apenas o <see cref="FacialScore"/>.
        /// Atualiza <see cref="Sensor.LastReadingTimestamp"/> com o instante atual.
        /// </summary>
        /// <returns>Objeto <see cref="SensorReading"/> com FacialScore preenchido.</returns>
        public override SensorReading GetReading()
        {
            LastReadingTimestamp = DateTime.Now;
            return new SensorReading
            {
                EmployeeId = string.Empty,
                Timestamp = LastReadingTimestamp,
                FacialScore = FacialScore,
                PostureScore = 0,
                ActivityScore = 0
            };
        }
    }
}
