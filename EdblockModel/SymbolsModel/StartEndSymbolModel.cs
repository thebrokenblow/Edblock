using EdblockModel.SymbolsModel;

namespace EdblockModel.SymbolsModel;

public class StartEndSymbolModel : BlockSymbolModel
{
    private const int offsetTextField = 25;

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