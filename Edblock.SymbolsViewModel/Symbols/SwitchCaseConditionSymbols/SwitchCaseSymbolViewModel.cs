using EdblockModel.SymbolsModel;
using Edblock.SymbolsViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using Edblock.SymbolsViewModel.Core;
using Edblock.PagesViewModel.Pages;

namespace Edblock.SymbolsViewModel.Symbols.SwitchCaseConditionSymbols;

public abstract class SwitchCaseSymbolViewModel : BlockSymbolViewModel
{
    public List<ConnectionPointVM> ConnectionPointsSwitchCaseVM { get; init; }
    protected readonly int _countLines;

    protected SwitchCaseSymbolViewModel(EditorViewModel editorViewModel, int countLine) : base(editorViewModel)
    {
        _countLines = countLine;

        ConnectionPointsSwitchCaseVM = new(countLine);

        var switchCaseSymbolModel = (SwitchCaseSymbolModel)BlockSymbolModel;
        switchCaseSymbolModel.CountLine = _countLines;
    }

    public override BlockSymbolModel CreateBlockSymbolModel()
    {
        var nameSymbol = GetType().Name.ToString();

        var switchCaseSymbolModel = new SwitchCaseSymbolModel()
        {
            Id = Id,
            NameSymbol = nameSymbol
        };

        return switchCaseSymbolModel;
    }
}
