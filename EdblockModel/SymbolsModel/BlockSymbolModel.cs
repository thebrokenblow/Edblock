using EdblockModel.SymbolsModel;

namespace EdblockModel.SymbolsModel;

public class BlockSymbolModel
{
    public Guid Id { get; set; }
    public string? NameSymbol { get; set; }
    public double XCoordinate { get; set; }
    public double YCoordinate { get; set; }
    public string? Color { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public TextFieldSymbolModel? TextFieldSymbolModel { get; set; }
}