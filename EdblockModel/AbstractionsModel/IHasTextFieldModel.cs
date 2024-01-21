using EdblockModel.SymbolsModel;

namespace EdblockModel.AbstractionsModel;

public interface IHasTextFieldSymbolModel
{
    public TextFieldSymbolModel TextFieldSymbolModel { get; init; }
}