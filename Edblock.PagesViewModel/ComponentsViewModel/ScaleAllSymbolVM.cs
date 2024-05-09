using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Edblock.PagesViewModel.ComponentsViewModel;

public class ScaleAllSymbolVM : INotifyPropertyChanged
{
    private bool isScaleAllSymbolVM;
    public bool IsScaleAllSymbolVM
    {
        get => isScaleAllSymbolVM;
        set
        {
            isScaleAllSymbolVM = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}