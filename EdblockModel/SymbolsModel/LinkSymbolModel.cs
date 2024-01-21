using EdblockModel.AbstractionsModel;

namespace EdblockModel.SymbolsModel;

public class LinkSymbolModel : BlockSymbolModel, IHasSize, IHasTextFieldSymbolModel
{
    public double Width { get; set; }
    public double Height { get; set; }

    private const double minWidth = 40;
    public double MinWidth => minWidth;

    private const double minHeight = 40;
    public double MinHeight => minHeight;

    public TextFieldSymbolModel TextFieldSymbolModel { get; init; }

    public LinkSymbolModel()
    {
        TextFieldSymbolModel = new();
    }
}