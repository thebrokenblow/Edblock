using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EdblockViewModel.Symbols.Abstraction;

public abstract class Symbol : INotifyPropertyChanged
{
    public string? HexColor { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
