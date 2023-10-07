using EdblockModel.Symbols.Abstraction;

namespace EdblockModel.Symbols;

public class ActionSymbolModel : BlockSymbolModel
{
    public ActionSymbolModel()
    {
        HexColor = "#FF52C0AA";
    }

    public override void SetWidth(int width)
    {
        Width = width;
    }

    public override void SetHeight(int height)
    {
        Height = height;
    }

    public override void SetTextFieldWidth(int width)
    {
    }

    public override void SetTextFieldHeight(int height)
    {
    }
}