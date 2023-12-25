namespace EdblockModel.SymbolsModel;

public class ActionSymbolModel : BlockSymbolModel
{
    public override int GetTextFieldWidth()
    {
        return Width;
    }

    public override int GetTextFieldHeight()
    {
        return Height;
    }

    public override int GetTextFieldLeftOffset()
    {
        return 0;
    }

    public override int GetTextFieldTopOffset()
    {
        return 0;
    }
}