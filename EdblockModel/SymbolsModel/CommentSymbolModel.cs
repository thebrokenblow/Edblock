using EdblockModel.AbstractionsModel;

namespace EdblockModel.SymbolsModel;

public class CommentSymbolModel : BlockSymbolModel, IHasTextFieldSymbolModel
{
    public TextFieldSymbolModel TextFieldSymbolModel { get; init; }

    private const double minWidth = 140;
    public override double MinWidth => minWidth;

    private const double minHeight = 20;
    public override double MinHeight => minHeight;

    public CommentSymbolModel()
    {
        TextFieldSymbolModel = new();
    }
}