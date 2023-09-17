using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EdblockViewModel.Symbols.Abstraction;

public abstract class Symbol : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
