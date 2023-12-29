namespace EdblockModel.SymbolsModel;

public class LinkSymbolModel : BlockSymbolModel
{
    public override double GetTextFieldWidth()
    {
        return Math.Sqrt(Height * Height / 2);
    }

    public override double GetTextFieldHeight()
    {
        return Math.Sqrt(Height * Height / 2);
    }

    public override double GetTextFieldLeftOffset()
    {
        return (Height - Math.Sqrt(Height * Height / 2)) / 2;
    }

    public override double GetTextFieldTopOffset()
    {
        return (Height - Math.Sqrt(Height * Height / 2)) / 2;
    }
}