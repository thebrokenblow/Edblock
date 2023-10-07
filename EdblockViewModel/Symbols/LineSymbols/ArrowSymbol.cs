using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using EdblockModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.Symbols.LineSymbols;

public class ArrowSymbol : Symbol
{
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

    public void ChangeOrientationArrow((int x, int y) startCoordinateLine, (int x, int y) currentCoordinateLine, PositionConnectionPoint positionConnectionPoint)
    {
        var coordinatesArrow = ArrowSymbolModel.GetCoordinateArrow(startCoordinateLine, currentCoordinateLine, positionConnectionPoint);
        SetCoodinate(coordinatesArrow);
    }

    public void ChangeOrientationArrow(int currentX, int currentY, PositionConnectionPoint positionConnectionPoint)
    {
        var coordinatesArrow = ArrowSymbolModel.GetCoordinateArrow(currentX, currentY, positionConnectionPoint);
        SetCoodinate(coordinatesArrow);
    }

    private void SetCoodinate(List<(int, int)> coordinatesArrow)
    {
        var pointArrowSymbol = new PointCollection();
        foreach (var coordinate in coordinatesArrow)
        {
            var pointCoordinate = new Point(coordinate.Item1, coordinate.Item2);
            pointArrowSymbol.Add(pointCoordinate);
        }

        PointArrowSymbol = pointArrowSymbol;
    }
}