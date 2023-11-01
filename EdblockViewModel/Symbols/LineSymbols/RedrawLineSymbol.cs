using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.ConnectionPoints;

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
            if (_symbolOutgoingLine?.YCoordinate + _symbolOutgoingLine?.Height < _symbolaIncomingLine?.YCoordinate)
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
            else
            {
                if (_drawnLineSymbolVM.LineSymbols.Count == 5)
                {
                    var firstLine = _drawnLineSymbolVM.LineSymbols[^1];
                    firstLine.X1 = _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate + _drawnLineSymbolVM.SymbolaIncomingLine.Width / 2;
                    firstLine.Y1 = _drawnLineSymbolVM.SymbolaIncomingLine!.YCoordinate - 20;
                    firstLine.X2 = _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate + _drawnLineSymbolVM.SymbolaIncomingLine.Width / 2;
                    firstLine.Y2 = _drawnLineSymbolVM.SymbolaIncomingLine!.YCoordinate;

                    var secondLine = _drawnLineSymbolVM.LineSymbols[^2];
                    secondLine.X1 = (_drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width + _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate) / 2;
                    secondLine.Y1 = _drawnLineSymbolVM.SymbolaIncomingLine!.YCoordinate - 20;
                    secondLine.X2 = _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate + _drawnLineSymbolVM.SymbolaIncomingLine.Width / 2;
                    secondLine.Y2 = _drawnLineSymbolVM.SymbolaIncomingLine!.YCoordinate - 20;

                    var thirdLine = _drawnLineSymbolVM.LineSymbols[^3];
                    thirdLine.X1 = (_drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width + _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate) / 2;
                    thirdLine.Y1 = _drawnLineSymbolVM.SymbolOutgoingLine!.YCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Height + 20;
                    thirdLine.X2 = (_drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width + _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate) / 2;
                    thirdLine.Y2 = _drawnLineSymbolVM.SymbolaIncomingLine!.YCoordinate - 20;

                    var fourthLine = _drawnLineSymbolVM.LineSymbols[^4];
                    fourthLine.X1 = _drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width / 2;
                    fourthLine.Y1 = _drawnLineSymbolVM.SymbolOutgoingLine!.YCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Height + 20;
                    fourthLine.X2 = (_drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width + _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate) / 2;
                    fourthLine.Y2 = _drawnLineSymbolVM.SymbolOutgoingLine!.YCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Height + 20;


                    var fifthLine = _drawnLineSymbolVM.LineSymbols[^5];
                    fifthLine.X1 = _drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width / 2;
                    fifthLine.Y1 = _drawnLineSymbolVM.SymbolOutgoingLine!.YCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Height;
                    fifthLine.X2 = _drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width / 2;
                    fifthLine.Y2 = _drawnLineSymbolVM.SymbolOutgoingLine!.YCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Height + 20;
                }
                else
                {
                    _drawnLineSymbolVM.LineSymbols.Clear();

                    var firstLine = new LineSymbolVM
                    {
                        X1 = _drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width / 2,
                        Y1 = _drawnLineSymbolVM.SymbolOutgoingLine!.YCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Height,
                        X2 = _drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width / 2,
                        Y2 = _drawnLineSymbolVM.SymbolOutgoingLine!.YCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Height + 20
                    };

                    var secondLine = new LineSymbolVM
                    {
                        X1 = _drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width / 2,
                        Y1 = _drawnLineSymbolVM.SymbolOutgoingLine!.YCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Height + 20,
                        X2 = (_drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width + _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate) / 2,
                        Y2 = _drawnLineSymbolVM.SymbolOutgoingLine!.YCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Height + 20
                    };

                    var thirdLine = new LineSymbolVM
                    {
                        X1 = (_drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width + _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate) / 2,
                        Y1 = _drawnLineSymbolVM.SymbolOutgoingLine!.YCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Height + 20,
                        X2 = (_drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width + _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate) / 2,
                        Y2 = _drawnLineSymbolVM.SymbolaIncomingLine!.YCoordinate - 20,
                    };

                    var fourthLine = new LineSymbolVM
                    {
                        X1 = (_drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width + _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate) / 2,
                        Y1 = _drawnLineSymbolVM.SymbolaIncomingLine!.YCoordinate - 20,
                        X2 = _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate + _drawnLineSymbolVM.SymbolaIncomingLine.Width / 2,
                        Y2 = _drawnLineSymbolVM.SymbolaIncomingLine!.YCoordinate - 20,
                    };

                    var fifthLine = new LineSymbolVM
                    {
                        X1 = _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate + _drawnLineSymbolVM.SymbolaIncomingLine.Width / 2,
                        Y1 = _drawnLineSymbolVM.SymbolaIncomingLine!.YCoordinate - 20,
                        X2 = _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate + _drawnLineSymbolVM.SymbolaIncomingLine.Width / 2,
                        Y2 = _drawnLineSymbolVM.SymbolaIncomingLine!.YCoordinate
                    };

                    _drawnLineSymbolVM.LineSymbols.Add(firstLine);
                    _drawnLineSymbolVM.LineSymbols.Add(secondLine);
                    _drawnLineSymbolVM.LineSymbols.Add(thirdLine);
                    _drawnLineSymbolVM.LineSymbols.Add(fourthLine);
                    _drawnLineSymbolVM.LineSymbols.Add(fifthLine);
                }
            }
            
            var line = _drawnLineSymbolVM.LineSymbols[^1];
            _drawnLineSymbolVM.ArrowSymbol.ChangeOrientationArrow((line.X2, line.Y2), _drawnLineSymbolVM.PositionIncomingConnectionPoint);
        }
    }
}
