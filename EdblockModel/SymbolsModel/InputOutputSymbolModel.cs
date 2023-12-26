namespace EdblockModel.SymbolsModel;

public class InputOutputSymbolModel : BlockSymbolModel
{
    public const int offsetTextField = 40;

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