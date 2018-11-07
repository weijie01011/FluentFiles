using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FluentFiles.ViewModels
{
    public interface IViewModel : INotifyPropertyChanged { }

    public class ViewModel : IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
