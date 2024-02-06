using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EdblockViewModel.AbstractionsVM;

public abstract class SymbolVM : INotifyPropertyChanged
{
    public abstract string? Color { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}