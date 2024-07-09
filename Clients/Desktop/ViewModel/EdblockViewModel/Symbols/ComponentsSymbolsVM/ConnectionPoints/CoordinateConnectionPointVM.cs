using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints.Interfaces;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

public class CoordinateConnectionPointVM(BlockSymbolVM blockSymbolVM) : ICoordinateConnectionPointVM
{
    private const int offsetPositionConnectionPoint = 15;

    public (double, double) GetCoordinateTop() => 
        (blockSymbolVM.Width / 2, -offsetPositionConnectionPoint);

    public (double, double) GetCoordinateRight() =>
        (blockSymbolVM.Width + offsetPositionConnectionPoint, blockSymbolVM.Height / 2);

    public (double, double) GetCoordinateBottom() =>
        (blockSymbolVM.Width / 2, blockSymbolVM.Height + offsetPositionConnectionPoint);

    public (double, double) GetCoordinateLeft() =>
        (-offsetPositionConnectionPoint, blockSymbolVM.Height / 2);

    public (double, double) GetCoordinateStartDrawLineTop() =>
        (blockSymbolVM.XCoordinate + blockSymbolVM.Width / 2, blockSymbolVM.YCoordinate);

    public (double, double) GetCoordinateStartDrawLineRight() =>
        (blockSymbolVM.XCoordinate + blockSymbolVM.Width, blockSymbolVM.YCoordinate + blockSymbolVM.Height / 2);
    
    public (double, double) GetCoordinateStartDrawLineBottom() =>
        (blockSymbolVM.XCoordinate + blockSymbolVM.Width / 2, blockSymbolVM.YCoordinate + blockSymbolVM.Height);

    public (double, double) GetCoordinateStartDrawLineLeft() =>
        (blockSymbolVM.XCoordinate, blockSymbolVM.YCoordinate + blockSymbolVM.Height / 2);
}