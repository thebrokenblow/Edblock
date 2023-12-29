namespace EdblockModel.SymbolsModel;

public class ActionSymbolModel : BlockSymbolModel
{
    public override double GetTextFieldWidth()
    {
        return Width;
    }

    public override double GetTextFieldHeight()
    {
        return Height;
    }

    public override double GetTextFieldLeftOffset()
    {
        return 0;
    }

    public override double GetTextFieldTopOffset()
    {
        return 0;
    }
}