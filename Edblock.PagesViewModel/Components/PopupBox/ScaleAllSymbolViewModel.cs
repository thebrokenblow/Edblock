using Edblock.PagesViewModel.Core;

namespace Edblock.PagesViewModel.Components.PopupBox;

public class ScaleAllSymbolViewModel : BaseViewModel
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
}