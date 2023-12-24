namespace EdblockModel.Symbols;

public class ConditionSymbolModel : BlockSymbolModel
{
    public override int GetTextFieldWidth()
    {
        return Width / 2;
    }

    public override int GetTextFieldHeight()
    {
        return Height / 2;
    }

    public override int GetTextFieldLeftOffset()
    {
        return Width / 4;
    }

    public override int GetTextFieldTopOffset()
    {
        return Height / 4;
    }
}
