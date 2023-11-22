using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using EdblockModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.Enum;

namespace EdblockViewModel.Symbols.LineSymbols;

public class ArrowSymbol : SymbolVM
{
    private PointCollection pointArrowSymbol = new();
    public PointCollection PointArrowSymbol
    {
        get => pointArrowSymbol;
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

    public void ChangeOrientationArrow((int x, int y) finalCoordinate, PositionConnectionPoint positionConnectionPoint)
    {
        var coordinatesArrow = ArrowSymbolModel.GetCoordinateArrow(finalCoordinate, positionConnectionPoint);
        SetCoodinate(coordinatesArrow);
    }

    private void SetCoodinate(List<(int x, int y)> coordinatesArrow)
    {
        var pointArrowSymbol = new PointCollection();

        foreach (var coordinate in coordinatesArrow)
        {
            var pointCoordinate = new Point(coordinate.x, coordinate.y);
            pointArrowSymbol.Add(pointCoordinate);
        }

        PointArrowSymbol = pointArrowSymbol;
    }
}