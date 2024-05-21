using EdblockModel.EnumsModel;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.AbstractionsVM;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

public class FactoryConnectionPoint
{
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly BlockSymbolVM _blockSymbolVM;
    private readonly LineStateStandardVM _checkBoxLineGostVM;

    public FactoryConnectionPoint(CanvasSymbolsVM canvasSymbolsVM, BlockSymbolVM blockSymbolVM, LineStateStandardVM checkBoxLineGostVM)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        _blockSymbolVM = blockSymbolVM;
        _checkBoxLineGostVM = checkBoxLineGostVM;
    }

    public ConnectionPointVM Create(SideSymbol sideSymbol)
    {
        var connectionPointVM = new ConnectionPointVM(
                _canvasSymbolsVM,
                _blockSymbolVM,
                _checkBoxLineGostVM,
                sideSymbol);

        return connectionPointVM;
    }
}