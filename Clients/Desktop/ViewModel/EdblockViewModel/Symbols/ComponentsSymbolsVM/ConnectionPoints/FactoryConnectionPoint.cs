using EdblockModel.EnumsModel;
using EdblockViewModel.Abstractions;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

public class FactoryConnectionPoint(ICanvasSymbolsVM canvasSymbolsVM, BlockSymbolVM blockSymbolVM, ILineStateStandardComponentVM lineStateStandardComponentVM)
{
    public ConnectionPointVM Create(SideSymbol sideSymbol)
    {
        var connectionPointVM = new ConnectionPointVM(
                canvasSymbolsVM,
                lineStateStandardComponentVM,
                blockSymbolVM,
                sideSymbol);

        return connectionPointVM;
    }
}