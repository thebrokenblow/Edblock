namespace EdblockModel.Symbols;

public class ActionSymbolModel : BlockSymbolModel
{
    public ActionSymbolModel() : base()
    {
    }

    public override void SetWidth(int width)
    {
        Width = width;
    }

    public override void SetHeight(int height)
    {
        Height = height;
    }

    public override int GetTextFieldWidth(int width)
    {
        return Width;
    }

    public override int GetTextFieldHeight(int height)
    {
        return Height;
    }
}