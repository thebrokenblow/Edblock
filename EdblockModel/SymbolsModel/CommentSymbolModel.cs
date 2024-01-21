using EdblockModel.AbstractionsModel;

namespace EdblockModel.SymbolsModel;

public class CommentSymbolModel : BlockSymbolModel, IHasTextFieldSymbolModel
{
    public TextFieldSymbolModel TextFieldSymbolModel { get; init; }

    public CommentSymbolModel()
    {
        TextFieldSymbolModel = new();
    }
}