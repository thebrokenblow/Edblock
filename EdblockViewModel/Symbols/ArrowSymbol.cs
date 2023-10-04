using System.Windows;
using System.Windows.Media;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

public class ArrowSymbol : Symbol
{
    private const int WidthArrow = 10;
    private const int HeightArrow = 10;
    public ArrowSymbol()
    {
    }

    private PointCollection pointArrowSymbol = new();
    public PointCollection PointArrowSymbol
    {
        get
        {
            return pointArrowSymbol;
        }
        set
        {
            pointArrowSymbol = value;
            OnPropertyChanged();
        }
    }

    public void DrawRigthArrow(int currentX, int currentY)
    {
        var pointArrowSymbol = new PointCollection();

        var topPoint = new Point(currentX - WidthArrow, currentY + HeightArrow / 2);
        var middlePoint = new Point(currentX - WidthArrow, currentY - HeightArrow / 2);
        var bottomPoint = new Point(currentX, currentY);

        pointArrowSymbol.Add(topPoint);
        pointArrowSymbol.Add(middlePoint);
        pointArrowSymbol.Add(bottomPoint);

        PointArrowSymbol = pointArrowSymbol;
    }
}