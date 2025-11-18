using System.Collections.ObjectModel;
using BurnoutWatcherMonitor.Helpers;
using BurnoutWatcherMonitor.Models;

namespace BurnoutWatcherMonitor.ViewModels
{
    /// <summary>
    /// ViewModel que expõe os dados de um único <see cref="Employee"/> para a UI.
    /// Fornece coleções observáveis com o histórico de leituras e alertas, além de ações relacionadas.
    /// </summary>
    public class EmployeeViewModel : BaseViewModel
    {
        private Employee _employee;

        /// <summary>
        /// Coleção observável com as leituras de sensores do colaborador.
        /// Inicializada a partir de <see cref="Employee.Readings"/>.
        /// </summary>
        public ObservableCollection<SensorReading> Readings { get; set; }

        /// <summary>
        /// Coleção observável com os alertas gerados para o colaborador.
        /// Inicializada a partir de <see cref="Employee.Alerts"/>.
        /// </summary>
        public ObservableCollection<Alert> Alerts { get; set; }

        // Nome do colaborador (somente leitura), exposto para binding na UI.
        public string Name => _employee.Name;

        /// <summary>
        /// Cria um novo <see cref="EmployeeViewModel"/> a partir do modelo <see cref="Employee"/>.
        /// </summary>
        /// <param name="employee">Instância do modelo a ser representado pelo ViewModel.</param>
        public EmployeeViewModel(Employee employee)
        {
            _employee = employee;
            Readings = new ObservableCollection<SensorReading>(employee.Readings);
            Alerts = new ObservableCollection<Alert>(employee.Alerts);
        }

        /// <summary>
        /// Marca um alerta como reconhecido (acknowledged) e notifica bindings sobre mudança.
        /// Modifica o objeto <see cref="Alert"/> diretamente; o ViewModel dispara a notificação
        /// para que a UI possa atualizar a exibição da coleção de alertas.
        /// </summary>
        /// <param name="alert">Alerta a ser marcado como reconhecido.</param>
        public void AcknowledgeAlert(Alert alert)
        {
            alert.Acknowledged = true;
            Raise(nameof(Alerts));
        }
    }
}
