namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints.Interfaces;

public interface ICoordinateConnectionPointVM
{
    (double, double) GetCoordinateTop();
    (double, double) GetCoordinateRight();
    (double, double) GetCoordinateBottom();
    (double, double) GetCoordinateLeft();

    (double, double) GetCoordinateStartDrawLineTop();
    (double, double) GetCoordinateStartDrawLineRight();
    (double, double) GetCoordinateStartDrawLineBottom();
    (double, double) GetCoordinateStartDrawLineLeft();
}