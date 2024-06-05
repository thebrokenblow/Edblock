using System.Collections.Generic;
using EdblockModel.SymbolsModel;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using EdblockViewModel.Pages;
using EdblockViewModel.Symbols.Abstractions;

namespace EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;

public abstract class SwitchCaseSymbolVM : BlockSymbolVM
{
    public List<ConnectionPointVM> ConnectionPointsSwitchCaseVM { get; init; }
    protected readonly int _countLines;

    protected SwitchCaseSymbolVM(EditorVM edblockVM, int countLine) : base(edblockVM)
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
