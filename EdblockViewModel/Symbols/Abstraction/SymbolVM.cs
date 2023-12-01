using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EdblockViewModel.Symbols.Abstraction;

public abstract class SymbolVM : INotifyPropertyChanged
{
    private string? color;
    public string? Color 
    {
        get => color;
        set
        {
            color = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}