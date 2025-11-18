using System.Collections.ObjectModel;
using BurnoutWatcherMonitor.Models;

namespace BurnoutWatcherMonitor.ViewModels
{
    // ViewModel do dashboard que agrupa a coleção de colaboradores exibida na janela principal.
    public class DashboardViewModel
    {
        // Coleção observável de colaboradores usada para binding com a lista principal.
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();

        // Construtor padrão. A coleção pode ser populada externamente (ex.: ao carregar do repositório).
        public DashboardViewModel()
        {
        }
    }
}
