using System;

namespace BurnoutWatcherMonitor.Models
{
    // Sensor que simula um wearable (dispositivo vestível) que fornece dados de postura e atividade.
    public class WearableSensor : Sensor
    {
        // Valor da avaliação de postura (0–100) fornecido pela simulação.
        public double PostureScore { get; set; }

        // Valor da avaliação de atividade (0–100) fornecido pela simulação
        public double ActivityScore { get; set; }

        /// <summary>
        /// Cria uma nova instância de <see cref="WearableSensor"/>.
        /// </summary>
        /// <param name="id">Identificador do sensor.</param>
        /// <param name="location">Localização física ou lógica do sensor.</param>
        public WearableSensor(string id, string location) : base(id, location)
        {
        }

        /// <summary>
        /// Gera uma leitura (<see cref="SensorReading"/>) contendo PostureScore e ActivityScore.
        /// Atualiza <see cref="Sensor.LastReadingTimestamp"/> com o instante atual.
        /// </summary>
        /// <returns>Objeto <see cref="SensorReading"/> com PostureScore e ActivityScore preenchidos.</returns>
        public override SensorReading GetReading()
        {
            LastReadingTimestamp = DateTime.Now;
            return new SensorReading
            {
                EmployeeId = string.Empty,
                Timestamp = LastReadingTimestamp,
                FacialScore = 0,
                PostureScore = PostureScore,
                ActivityScore = ActivityScore
            };
        }
    }
}
