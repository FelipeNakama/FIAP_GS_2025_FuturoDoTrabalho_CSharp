namespace BurnoutWatcherMonitor.Models
{
    /// Limiares (thresholds) utilizados para classificar o nível de stress
    public class Thresholds
    {
        // Valor a partir do qual o score é considerado Moderate
        public double Moderate { get; set; } = 50.0;

        // Valor a partir do qual o score é considerado High.
        public double High { get; set; } = 75.0;
    }
}
