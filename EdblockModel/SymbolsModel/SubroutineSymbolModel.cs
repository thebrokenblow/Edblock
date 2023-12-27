namespace EdblockModel.SymbolsModel;

public class SubroutineSymbolModel : BlockSymbolModel
{
    private const int offsetTextField = 20;

    public override int GetTextFieldWidth()
    {
        return Width - offsetTextField * 2;
    }

    public override int GetTextFieldHeight()
    {
        return Height;
    }

    public override int GetTextFieldLeftOffset()
    {
        return offsetTextField;
    }

    public override int GetTextFieldTopOffset()
    {
        return 0;
    }
}