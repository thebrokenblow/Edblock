using System.Windows.Media;
using EdblockModel.Lines;
using EdblockViewModel.Core;

namespace EdblockViewModel.Symbols.LinesSymbolVM.Components;

public class ArrowLineVM(ArrowLineModel arrowLineModel) : ObservableObject
{
    private PointCollection pointArrowSymbol = [];
    public PointCollection PointArrowSymbol
    {
        get => pointArrowSymbol;
        set
        {
            pointArrowSymbol = value;
            OnPropertyChanged();
        }
    }

    public void Redraw()
    {
        var currentArrowCoordinates = arrowLineModel.CurrentArrowCoordinates;

        int pointXCoordinateArrow = 0;
        int pointYCoordinateArrow = 0;

        PointArrowSymbol = [
                new(
                    currentArrowCoordinates[pointXCoordinateArrow++].xCoordinate, 
                    currentArrowCoordinates[pointYCoordinateArrow++].yCoordinate),

                new(
                    currentArrowCoordinates[pointXCoordinateArrow++].xCoordinate, 
                    currentArrowCoordinates[pointYCoordinateArrow++].yCoordinate),

                new(
                    currentArrowCoordinates[pointXCoordinateArrow++].xCoordinate, 
                    currentArrowCoordinates[pointYCoordinateArrow++].yCoordinate),
            ];
    }
}