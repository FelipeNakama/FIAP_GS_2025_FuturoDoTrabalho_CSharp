using System.Windows;
using BurnoutWatcherMonitor.ViewModels;
using BurnoutWatcherMonitor.Models;

namespace BurnoutWatcherMonitor.Views
{
    // Janela de detalhe de um colaborador. Exibe histórico de leituras e alertas e fornece ação para marcar alertas como tratados.
    public partial class EmployeeDetailWindow : Window
    {
        /// <summary>
        /// Retorna o ViewModel associado à janela, criando um <see cref="EmployeeViewModel"/>
        /// padrão se o DataContext não estiver definido.
        /// </summary>
        public EmployeeViewModel ViewModel => DataContext as EmployeeViewModel ?? new EmployeeViewModel(new Employee());

        public EmployeeDetailWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Marca o primeiro alerta disponível como reconhecido (acknowledged).
        /// Exibe uma MessageBox informando se a operação foi realizada ou se não havia alertas.
        /// </summary>
        /// <param name="sender">Remetente do evento Click.</param>
        /// <param name="e">Argumentos do evento RoutedEventArgs.</param>
        private void BtnAcknowledge_Click(object sender, RoutedEventArgs e)
        {
            var alert = ViewModel.Alerts != null && ViewModel.Alerts.Count > 0 ? ViewModel.Alerts[0] : null;
            if (alert != null)
            {
                ViewModel.AcknowledgeAlert(alert);
                MessageBox.Show("Alert acknowledged.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No alerts to acknowledge.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Fecha a janela de detalhe.
        /// </summary>
        /// <param name="sender">Remetente do evento Click.</param>
        /// <param name="e">Argumentos do evento RoutedEventArgs.</param>
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
