using System.Windows;
using System.Windows.Media;
using EdblockModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.LineSymbols;

public class ArrowSymbol : Symbol
{
    private readonly ArrowSymbolModel arrowSymbolModel = new();

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

    public void DrawRigthArrow(int x, int y)
    {
        var pointArrowSymbol = new PointCollection();
        var Coordinates = arrowSymbolModel.DrawRigthArrow(x, y);

        foreach (var coordinate in Coordinates)
        {
            var pointCoordinate = new Point(coordinate.Item1, coordinate.Item2);
            pointArrowSymbol.Add(pointCoordinate);
        }

        PointArrowSymbol = pointArrowSymbol;
    }
}