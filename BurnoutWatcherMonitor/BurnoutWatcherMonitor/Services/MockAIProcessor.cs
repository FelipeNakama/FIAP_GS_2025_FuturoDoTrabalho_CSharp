using BurnoutWatcherMonitor.Models;

namespace BurnoutWatcherMonitor.Services
{
    /// <summary>
    /// Implementação simples e determinística de <see cref="IProcessor"/>.
    /// Usa uma fórmula linear ponderada para calcular o stressScore.
    /// </summary>
    public class MockAIProcessor : IProcessor
    {
        /// <summary>
        /// Calcula o stressScore usando a fórmula:
        /// stressScore = 0.5 * facial + 0.3 * posture + 0.2 * activity.
        /// O resultado é limitado ao intervalo 0–100.
        /// </summary>
        /// <param name="reading">Leitura agregada contendo FacialScore, PostureScore e ActivityScore.</param>
        /// <returns>Valor do stressScore normalizado entre 0 e 100.</returns>
        public double CalculateStressScore(SensorReading reading)
        {
            double s = 0.5 * reading.FacialScore + 0.3 * reading.PostureScore + 0.2 * reading.ActivityScore;
            if (s < 0) s = 0;
            if (s > 100) s = 100;
            return s;
        }

        /// <summary>
        /// Avalia o nível categórico a partir do stressScore usando os thresholds fornecidos.
        /// - Retorna "High" se stressScore >= thresholds.High
        /// - Retorna "Moderate" se stressScore >= thresholds.Moderate
        /// - Retorna "Low" caso contrário
        /// </summary>
        /// <param name="stressScore">Valor do stressScore calculado.</param>
        /// <param name="thresholds">Limiares (Moderate, High) do colaborador.</param>
        /// <returns>Nome do nível correspondente ("Low", "Moderate" ou "High").</returns>
        public string EvaluateLevel(double stressScore, Thresholds thresholds)
        {
            if (stressScore >= thresholds.High) return "High";
            if (stressScore >= thresholds.Moderate) return "Moderate";
            return "Low";
        }
    }
}
