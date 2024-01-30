using EdblockModel.AbstractionsModel;

namespace EdblockModel.SymbolsModel;

public class ConditionSymbolModel : BlockSymbolModel, IHasTextFieldSymbolModel
{
    private const double minWidth = 40;
    public override double MinWidth => minWidth;

    private const double minHeight = 20;
    public override double MinHeight => minHeight;
    public TextFieldSymbolModel TextFieldSymbolModel { get; init; }

    public ConditionSymbolModel()
    {
        TextFieldSymbolModel = new();
    }
}