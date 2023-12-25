namespace EdblockModel.Symbols;

public class StartEndSymbolModel : BlockSymbolModel
{
    private const int offsetTextField = 25;

    public override int GetTextFieldWidth()
    {
        return Width - offsetTextField;
    }

    public override int GetTextFieldHeight()
    {
        return Height;
    }

    public override int GetTextFieldLeftOffset()
    {
       return offsetTextField / 2;
    }

    public override int GetTextFieldTopOffset()
    {
        return 0;
    }
}