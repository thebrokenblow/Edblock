using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EdblockModel.SymbolsModel.Enum;
using EdblockModel.SymbolsModel.LineSymbolsModel;

namespace EdblockViewModel.Symbols.LineSymbols;

public class ArrowSymbol : INotifyPropertyChanged
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

    private bool isSelected;
    public bool IsSelected
    {
        get => isSelected;
        set
        {
            isSelected = value;
            OnPropertyChanged();
        }
    }

    public void ChangeOrientationArrow((double x, double y) startCoordinateLine, (double x, double y) currentCoordinateLine, PositionConnectionPoint positionConnectionPoint)
    {
        var coordinatesArrow = ArrowSymbolModel.GetCoordinateArrow(startCoordinateLine, currentCoordinateLine, positionConnectionPoint);
        SetCoodinate(coordinatesArrow);
    }

    public void ChangeOrientationArrow((double x, double y) finalCoordinate, PositionConnectionPoint positionConnectionPoint)
    {
        var coordinatesArrow = ArrowSymbolModel.GetCoordinateArrow(finalCoordinate, positionConnectionPoint);
        SetCoodinate(coordinatesArrow);
    }

    private void SetCoodinate(List<(double x, double y)> coordinatesArrow)
    {
        var pointArrowSymbol = new PointCollection();

        foreach (var (x, y) in coordinatesArrow)
        {
            var pointCoordinate = new Point(x, y);
            pointArrowSymbol.Add(pointCoordinate);
        }

        PointArrowSymbol = pointArrowSymbol;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}