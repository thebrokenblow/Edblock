namespace EdblockModel.Symbols.Abstraction;

public abstract class BlockSymbolModel : SymbolModel
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int MinWidth { get; } = 40;
    public int MinHeight { get; } = 20;
    public abstract void SetWidth(int width);
    public abstract void SetHeight(int height);
    public abstract void SetTextFieldWidth(int width);
    public abstract void SetTextFieldHeight(int height);
}