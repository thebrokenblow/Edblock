using EdblockModel.AbstractionsModel;

namespace EdblockModel.SymbolsModel;

public class ParallelActionSymbolModel : BlockSymbolModel
{
    public int CountSymbolsIncoming { get; set; }
    public int CountSymbolsOutgoing { get; set; }

    private const double minWidth = 20;
    public override double MinWidth => minWidth;

    private const double minHeight = 20;
    public override double MinHeight => minHeight;
}