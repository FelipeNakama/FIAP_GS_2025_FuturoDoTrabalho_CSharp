using System;

namespace BurnoutWatcherMonitor.Models
{
    // Classe base abstrata para sensores que produzem leituras. Fornece propriedades comuns como identificador, localização e timestamp da última leitura.
    public abstract class Sensor
    {
        // Identificador do sensor.
        public string Id { get; set; } = string.Empty;

        // Localização física ou lógica do sensor.
        public string Location { get; set; } = string.Empty;

        // Data e hora da última leitura gerada por este sensor.
        public DateTime LastReadingTimestamp { get; set; }

        /// <summary>
        /// Construtor que inicializa identificador e localização do sensor.
        /// Define <see cref="LastReadingTimestamp"/> como <see cref="DateTime.MinValue"/>.
        /// </summary>
        /// <param name="id">Identificador do sensor.</param>
        /// <param name="location">Localização do sensor.</param>
        public Sensor(string id, string location)
        {
            Id = id;
            Location = location;
            LastReadingTimestamp = DateTime.MinValue;
        }

        /// <summary>
        /// Produz uma leitura do sensor representada por um <see cref="SensorReading"/>.
        /// Implementações concretas devem preencher os campos relevantes do DTO.
        /// </summary>
        /// <returns>Uma instância de <see cref="SensorReading"/> representando a leitura atual.</returns>
        public abstract SensorReading GetReading();
    }
}
