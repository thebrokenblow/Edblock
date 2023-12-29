namespace EdblockModel.SymbolsModel;

public class SubroutineSymbolModel : BlockSymbolModel
{
    private const int offsetTextField = 20;

    public override double GetTextFieldWidth()
    {
        return Width - offsetTextField * 2;
    }

    public override double GetTextFieldHeight()
    {
        return Height;
    }

    public override double GetTextFieldLeftOffset()
    {
        return offsetTextField;
    }

    public override double GetTextFieldTopOffset()
    {
        return 0;
    }
}