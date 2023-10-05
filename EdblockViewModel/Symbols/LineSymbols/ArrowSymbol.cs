using System.Windows.Media;
using EdblockModel.Symbols;
using System.Collections.Generic;
using EdblockModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.Abstraction;
using System;

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

    public void ChangeOrientationArrow(Tuple<int, int> startCoordinate, int currentX, int currentY, PositionConnectionPoint positionConnectionPoint)
    {
        var coordinatesArrow = ArrowSymbolModel.GetCoordinateArrow(startCoordinate, currentX, currentY, positionConnectionPoint);
        SetCoodinate(coordinatesArrow);
    }

    private void SetCoodinate(List<(int, int)> coordinatesArrow)
    {
        var pointArrowSymbol = new PointCollection();
        foreach (var coordinate in coordinatesArrow)
        {
            var pointCoordinate = CoordinateSymbols.GetPointCoordinate(coordinate);
            pointArrowSymbol.Add(pointCoordinate);
        }

        PointArrowSymbol = pointArrowSymbol;
    }
}