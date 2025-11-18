using BurnoutWatcherMonitor.Models;

namespace BurnoutWatcherMonitor.Services
{
    // Interface que define o contrato do processador responsável por transformar leituras de sensores em um score de stress e por avaliar o nível correspondente.
    public interface IProcessor
    {
        /// <summary>
        /// Calcula o stressScore a partir de uma leitura agregada de sensores.
        /// </summary>
        /// <param name="reading">Leitura agregada contendo FacialScore, PostureScore e ActivityScore.</param>
        /// <returns>Valor do stressScore no intervalo esperado (0–100).</returns>
        double CalculateStressScore(SensorReading reading);

        /// <summary>
        /// Avalia o nível categórico ("Low", "Moderate", "High") com base no stressScore
        /// e nos thresholds fornecidos para o colaborador.
        /// </summary>
        /// <param name="stressScore">Valor do stressScore calculado.</param>
        /// <param name="thresholds">Limiares (Moderate, High) do colaborador.</param>
        /// <returns>Nome do nível correspondente.</returns>
        string EvaluateLevel(double stressScore, Thresholds thresholds);
    }
}
