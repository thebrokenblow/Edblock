using EdblockModel.SymbolsModel;
using EdblockViewModel.AbstractionsVM;

namespace EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;

public abstract class SwitchCaseSymbolVM : BlockSymbolVM
{
    protected readonly int _countLines;

    protected SwitchCaseSymbolVM(EdblockVM edblockVM, int countLine) : base(edblockVM)
    {
        _countLines = countLine;

        BlockSymbolModel = CreateBlockSymbolModel();
    }

    public override BlockSymbolModel CreateBlockSymbolModel()
    {
        var switchCaseSymbolModel = new SwitchCaseSymbolModel()
        {
            Id = Id,
            NameSymbol = GetType().Name.ToString(),
            CountLine = _countLines,
        };

        return switchCaseSymbolModel;
    }
}
