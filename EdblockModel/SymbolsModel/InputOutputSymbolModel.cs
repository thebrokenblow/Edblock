namespace EdblockModel.SymbolsModel;

public class InputOutputSymbolModel : BlockSymbolModel
{
    public const int offsetTextField = 40;

    public override double GetTextFieldWidth()
    {
        return Width - offsetTextField;
    }

    public override double GetTextFieldHeight()
    {
       return Height;
    }

    public override double GetTextFieldLeftOffset()
    {
        return offsetTextField / 2;
    }

    public override double GetTextFieldTopOffset()
    {
        return 0;
    }
}