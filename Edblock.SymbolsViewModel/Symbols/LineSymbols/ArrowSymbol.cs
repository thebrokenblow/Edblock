using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EdblockModel.SymbolsModel.LineSymbolsModel;
using EdblockModel.SymbolsModel.LineSymbolsModel.DecoratorLineSymbolsModel;
using EdblockModel.EnumsModel;

namespace Edblock.SymbolsViewModel.Symbols.LineSymbols;

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

    public void ChangePosition((double x, double y) finalCoordinate)
    {
        var coordinatesArrow = ArrowSymbolModel.GetFinalCoordinate(finalCoordinate);
        SetCoodinate(coordinatesArrow);
    }

    public void ChangeOrientation((double x, double y) startCoordinateLine, (double x, double y) currentCoordinateLine, SideSymbol? positionConnectionPoint)
    {
        var coordinatesArrow = ArrowSymbolModel.GetCoordinateArrow(startCoordinateLine, currentCoordinateLine, positionConnectionPoint);
        SetCoodinate(coordinatesArrow);
    }

    public void ChangeOrientation((double x, double y) finalCoordinate, SideSymbol? positionConnectionPoint)
    {
        var coordinatesArrow = ArrowSymbolModel.GetCoordinateArrow(finalCoordinate, positionConnectionPoint);
        SetCoodinate(coordinatesArrow);
    }

    private void SetCoodinate(List<(double x, double y)> coordinatesArrow)
    {
        if (coordinatesArrow is null)
        {
            return;
        }

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