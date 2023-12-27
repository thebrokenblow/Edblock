namespace EdblockModel.SymbolsModel;

public class CycleForSymbolModel : BlockSymbolModel
{
    public const int offsetTextField = 10;

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