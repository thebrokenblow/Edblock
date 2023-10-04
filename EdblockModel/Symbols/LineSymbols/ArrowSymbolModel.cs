namespace EdblockModel.Symbols.LineSymbols;

public class ArrowSymbolModel
{
    private const int WidthArrow = 8;
    private const int HeightArrow = 8;

    public List<(int, int)> DrawRigthArrow(int currentX, int currentY)
    {
        var coordinateArrow = new List<(int, int)>
        {
            (currentX - WidthArrow, currentY + HeightArrow / 2),
            (currentX - WidthArrow, currentY - HeightArrow / 2),
            (currentX, currentY)
        };

        return coordinateArrow;
    }
}