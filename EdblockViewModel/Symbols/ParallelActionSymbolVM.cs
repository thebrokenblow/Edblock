using EdblockModel.SymbolsModel;
using EdblockModel.AbstractionsModel;
using EdblockViewModel.AbstractionsVM;

namespace EdblockViewModel.Symbols;

public class ParallelActionSymbolVM : BlockSymbolVM
{
    private readonly int _countSymbolsIncoming;
    private readonly int _countSymbolsOutgoing;
    private readonly EdblockVM _edblockVM;

    public ParallelActionSymbolVM(EdblockVM edblockVM, int countSymbolsIncoming, int countSymbolsOutgoing) : base(edblockVM)
    {
        _edblockVM = edblockVM;
        _countSymbolsIncoming = countSymbolsIncoming;
        _countSymbolsOutgoing = countSymbolsOutgoing;

        BlockSymbolModel = CreateBlockSymbolModel();
    }

    public override BlockSymbolModel CreateBlockSymbolModel()
    {
        var parallelActionSymbolModel = new ParallelActionSymbolModel()
        {
            CountSymbolsIncoming = _countSymbolsIncoming,
            CountSymbolsOutgoing = _countSymbolsOutgoing
        };

        return parallelActionSymbolModel;
    }

    public override void SetHeight(double height)
    {
        
    }

    public override void SetWidth(double width)
    {
       
    }
}