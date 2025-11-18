using System.Collections.Generic;
using System.Windows;
using BurnoutWatcherMonitor.Data;
using BurnoutWatcherMonitor.Models;
using BurnoutWatcherMonitor.ViewModels;

namespace BurnoutWatcherMonitor
{
    // Entrada da aplicação WPF. Trata eventos globais do ciclo de vida da aplicação.
    public partial class App : Application
    {
        /// <summary>
        /// Chamado quando a aplicação está sendo finalizada.
        /// Salva a lista de colaboradores usando o <see cref="Repository"/> se o DataContext
        /// da janela principal for um <see cref="DashboardViewModel"/>.
        /// </summary>
        /// <param name="e">Argumentos do evento de saída da aplicação.</param>
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            if (Current?.MainWindow != null)
            {
                var mw = Current.MainWindow;
                if (mw.DataContext is DashboardViewModel dvm)
                {
                    Repository.SaveEmployees(new List<Employee>(dvm.Employees));
                }
            }
        }
    }
}
