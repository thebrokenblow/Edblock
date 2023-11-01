using EdblockModel.Symbols.ConnectionPoints;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.LineSymbols;

internal class RedrawLineSymbol
{
    private readonly BlockSymbol? _symbolOutgoingLine;
    private readonly BlockSymbol? _symbolaIncomingLine;
    private readonly DrawnLineSymbolVM _drawnLineSymbolVM;

    public RedrawLineSymbol(DrawnLineSymbolVM drawnLineSymbolVM)
    {
        _symbolOutgoingLine = drawnLineSymbolVM.SymbolOutgoingLine;
        _symbolaIncomingLine = drawnLineSymbolVM.SymbolaIncomingLine;
        _drawnLineSymbolVM = drawnLineSymbolVM;

    }

    public void Redraw()
    {

        if (_drawnLineSymbolVM.PositionOutgoingConnectionPoint == PositionConnectionPoint.Bottom &&
            _drawnLineSymbolVM.PositionIncomingConnectionPoint == PositionConnectionPoint.Top)
        {
            if (_symbolOutgoingLine?.YCoordinate != _symbolaIncomingLine?.YCoordinate)
            {
                if (_symbolOutgoingLine?.XCoordinate == _symbolaIncomingLine?.XCoordinate)
                {
                    if (_drawnLineSymbolVM.LineSymbols.Count == 1)
                    {
                        var lastLine = _drawnLineSymbolVM.LineSymbols[0];
                        lastLine.X1 = _symbolOutgoingLine!.XCoordinate + _symbolOutgoingLine.Width / 2;
                        lastLine.Y1 = _symbolOutgoingLine!.YCoordinate + _symbolOutgoingLine.Height;
                        lastLine.X2 = _symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine.Width / 2;
                        lastLine.Y2 = _symbolaIncomingLine!.YCoordinate;
                    }
                    else
                    {
                        _drawnLineSymbolVM.LineSymbols.Clear();
                        var lastLine = new LineSymbolVM()
                        {
                            X1 = _symbolOutgoingLine!.XCoordinate + _symbolOutgoingLine.Width / 2,
                            Y1 = _symbolOutgoingLine!.YCoordinate + _symbolOutgoingLine.Height,
                            X2 = _symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine.Width / 2,
                            Y2 = _symbolaIncomingLine!.YCoordinate
                        };
                        _drawnLineSymbolVM.LineSymbols.Add(lastLine);
                    }
                }
                else
                {
                    if (_drawnLineSymbolVM.LineSymbols.Count == 3)
                    {
                        var firstLine = _drawnLineSymbolVM.LineSymbols[^3];

                        firstLine.X2 = _symbolOutgoingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2;
                        firstLine.Y2 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2;

                        var secondLine = _drawnLineSymbolVM.LineSymbols[^2];
                        secondLine.X1 = _symbolOutgoingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2;
                        secondLine.Y1 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2;
                        secondLine.X2 = _symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2;
                        secondLine.Y2 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2;

                        var thirdLine = _drawnLineSymbolVM.LineSymbols[^1];
                        thirdLine.X1 = _symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2;
                        thirdLine.Y1 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2;
                        thirdLine.X2 = _symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2;
                        thirdLine.Y2 = _symbolaIncomingLine!.YCoordinate;
                    }
                    else
                    {
                        _drawnLineSymbolVM.LineSymbols.Clear();

                        var firstLine = new LineSymbolVM
                        {
                            X1 = _symbolOutgoingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2,
                            Y1 = _symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height,
                            X2 = _symbolOutgoingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2,
                            Y2 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2
                        };

                        var secondLine = new LineSymbolVM
                        {
                            X1 = _symbolOutgoingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2,
                            Y1 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2,
                            X2 = _symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2,
                            Y2 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2
                        };

                        var thirdLine = new LineSymbolVM
                        {
                            X1 = _symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2,
                            Y1 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2,
                            X2 = _symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2,
                            Y2 = _symbolaIncomingLine!.YCoordinate
                        };

                        _drawnLineSymbolVM.LineSymbols.Add(firstLine);
                        _drawnLineSymbolVM.LineSymbols.Add(secondLine);
                        _drawnLineSymbolVM.LineSymbols.Add(thirdLine);

                    }
                }
            }
            var line = _drawnLineSymbolVM.LineSymbols[^1];
            _drawnLineSymbolVM.ArrowSymbol.ChangeOrientationArrow((line.X2, line.Y2), _drawnLineSymbolVM.PositionIncomingConnectionPoint);
        }
    }
}
