using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BurnoutWatcherMonitor.Helpers
{
    /// <summary>
    /// Base simples para view models que implementa <see cref="INotifyPropertyChanged"/>.
    /// Fornece o método <see cref="Raise"/> para notificar alterações de propriedades.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        // Evento disparado quando uma propriedade pública do ViewModel muda de valor.
        // Observadores (bindings) assinam este evento para atualizar a UI automaticamente.
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Dispara o evento <see cref="PropertyChanged"/> com o nome da propriedade informado.
        /// Use sem parâmetros dentro de propriedades para que o compilador forneça o nome automaticamente.
        /// </summary>
        /// <param name="propName">Nome da propriedade que foi alterada; preenchido automaticamente quando omitido.</param>
        protected void Raise([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
