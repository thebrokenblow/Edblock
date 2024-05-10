using EdblockModel.EnumsModel;
using Edblock.SymbolsViewModel.Core;
using Edblock.PagesViewModel.Components;
using Edblock.PagesViewModel.Components.PopupBox;

namespace Edblock.SymbolsViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

public class FactoryConnectionPoint(CanvasSymbolsViewModel canvasSymbolsViewModel, BlockSymbolViewModel blockSymbolViewModel, LineGostViewModel lineGostViewModel)
{
    public ConnectionPointVM Create(SideSymbol sideSymbol)
    {
        var connectionPointVM = new ConnectionPointVM(
                canvasSymbolsViewModel,
                blockSymbolViewModel,
                lineGostViewModel,
                sideSymbol);

        return connectionPointVM;
    }
}