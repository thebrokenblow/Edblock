using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using EdblockModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.Abstraction;

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

    public void SetCoodinateRigthArrow(int x, int y)
    {
        var coordinatesArror = ArrowSymbolModel.GetCoordinateRigth(x, y);
        SetCoodinate(coordinatesArror);
    }

    public void SetCoodinateLeftArrow(int x, int y)
    {
        var coordinatesArror = ArrowSymbolModel.GetCoordinateLeft(x, y);
        SetCoodinate(coordinatesArror);
    }

    public void SetCoodinateUpperArrow(int x, int y)
    {
        var coordinatesArror = ArrowSymbolModel.GetCoordinateUpper(x, y);
        SetCoodinate(coordinatesArror);
    }

    public void SetCoodinateBottomArrow(int x, int y)
    {
        var coordinatesArror = ArrowSymbolModel.GetCoordinateBottom(x, y);
        SetCoodinate(coordinatesArror);
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