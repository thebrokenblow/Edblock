using EdblockModel.EnumsModel;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

public class FactoryConnectionPoint(
    ICanvasSymbolsComponentVM canvasSymbolsVM, 
    ILineStateStandardComponentVM lineStateStandardComponentVM,
    BlockSymbolVM blockSymbolVM)
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