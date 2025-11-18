using System;
using System.Windows.Input;

namespace BurnoutWatcherMonitor.Helpers
{
    /// <summary>
    /// Implementação simples de <see cref="ICommand"/> para uso em bindings de comandos na UI.
    /// Permite delegar a execução e a condição de execução para lambdas ou métodos externos.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        /// <summary>
        /// Cria uma nova instância de <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="execute">Ação a ser executada quando o comando for disparado.</param>
        /// <param name="canExecute">Função que determina se o comando pode executar; se null, o comando sempre pode executar.</param>
        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Retorna se o comando pode ser executado com o parâmetro fornecido.
        /// </summary>
        /// <param name="parameter">Parâmetro passado pelo binding/ invocador; pode ser null.</param>
        /// <returns>True se pode executar; caso contrário false.</returns>
        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

        /// <summary>
        /// Executa a ação associada ao comando.
        /// </summary>
        /// <param name="parameter">Parâmetro passado pelo binding/ invocador; pode ser null.</param>
        public void Execute(object? parameter) => _execute(parameter);

        /// <summary>
        /// Evento que notifica mudanças na condição de execução do comando.
        /// Encadeia-se ao <see cref="CommandManager.RequerySuggested"/> para suporte automático de reavaliação.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
