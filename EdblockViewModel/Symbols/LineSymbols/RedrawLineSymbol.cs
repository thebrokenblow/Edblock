using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.ConnectionPoints;
using System.IO;

namespace EdblockViewModel.Symbols.LineSymbols;

internal class RedrawLineSymbol
{
    private readonly BlockSymbol _movableBlockSymbol;
    private readonly BlockSymbol? _unmovableBlockSymbol;
    private readonly DrawnLineSymbolVM _drawnLineSymbolVM;
    private readonly PositionConnectionPoint _positionOutgoing;
    private readonly PositionConnectionPoint _positionIncoming;
    private const int baseLineOffset = 20;

    public RedrawLineSymbol(DrawnLineSymbolVM drawnLineSymbolVM, BlockSymbol movableBlockSymbol)
    {
        _drawnLineSymbolVM = drawnLineSymbolVM;
        _movableBlockSymbol = movableBlockSymbol;

        if (_drawnLineSymbolVM.SymbolaIncomingLine == movableBlockSymbol)
        {
            _unmovableBlockSymbol = _drawnLineSymbolVM.SymbolOutgoingLine;
        }
        else
        {
            _unmovableBlockSymbol = _drawnLineSymbolVM.SymbolaIncomingLine;
        }

        _positionOutgoing = drawnLineSymbolVM.PositionOutgoingConnectionPoint;
        _positionIncoming = drawnLineSymbolVM.PositionIncomingConnectionPoint;  
    }

    private void SetCoordnateOneVertical()
    {
        var firstLine = _drawnLineSymbolVM.LineSymbols[^1];

        if (_drawnLineSymbolVM.LineSymbols.Count > 1)
        {
            _drawnLineSymbolVM.LineSymbols.Clear();
            _drawnLineSymbolVM.LineSymbols.Add(firstLine);
        }
        else
        {
            firstLine.X1 = _unmovableBlockSymbol!.XCoordinate + _unmovableBlockSymbol.Width / 2;
            firstLine.Y1 = _unmovableBlockSymbol.YCoordinate + _unmovableBlockSymbol.Height;
            firstLine.X2 = _movableBlockSymbol.XCoordinate + _movableBlockSymbol.Width / 2;
            firstLine.Y2 = _movableBlockSymbol.YCoordinate;
        }
    }

    private void RedrawBottomToTop()
    {
        
    }

    public void Redraw()
    {
        if (_unmovableBlockSymbol == null)
        {
            return;
        }

        if (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Top)
        {
            int unmovableBlockSymbolX = _unmovableBlockSymbol.XCoordinate;
            int unmovableBlockSymbolY = _unmovableBlockSymbol.YCoordinate;

            int unmovableBlockSymbolWidth = _unmovableBlockSymbol.Width;
            int unmovableBlockSymbolHeight = _unmovableBlockSymbol.Height;

            int movableBlockSymbolX = _movableBlockSymbol.XCoordinate;
            int movableBlockSymbolY = _movableBlockSymbol.YCoordinate;

            int movableBlockSymbolWidth = _movableBlockSymbol.Width;
            int movableBlockSymbolHeight = _movableBlockSymbol.Height;

            if (unmovableBlockSymbolY + unmovableBlockSymbolHeight < movableBlockSymbolY)
            {
                if ((unmovableBlockSymbolX + unmovableBlockSymbolWidth / 2) == (movableBlockSymbolX + movableBlockSymbolWidth / 2))
                {
                    SetCoordnateOneVertical();
                }
                else
                {
                    //if (_drawnLineSymbolVM.LineSymbols.Count == 3)
                    //{
                    //    var firstLine = _drawnLineSymbolVM.LineSymbols[^3];
                    //    firstLine.X1 = _symbolOutgoingLine!.XCoordinate + _symbolOutgoingLine!.Width / 2;
                    //    firstLine.Y1 = _symbolOutgoingLine!.YCoordinate + _symbolOutgoingLine!.Height;
                    //    firstLine.X2 = _symbolOutgoingLine!.XCoordinate + _symbolOutgoingLine!.Width / 2;
                    //    firstLine.Y2 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2;

                    //    var secondLine = _drawnLineSymbolVM.LineSymbols[^2];
                    //    secondLine.X1 = _symbolOutgoingLine!.XCoordinate + _symbolOutgoingLine!.Width / 2;
                    //    secondLine.Y1 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2;
                    //    secondLine.X2 = _symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2;
                    //    secondLine.Y2 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2;

                    //    var thirdLine = _drawnLineSymbolVM.LineSymbols[^1];
                    //    thirdLine.X1 = _symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2;
                    //    thirdLine.Y1 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2;
                    //    thirdLine.X2 = _symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2;
                    //    thirdLine.Y2 = _symbolaIncomingLine!.YCoordinate;
                    //}
                    //else
                    //{
                    //    _drawnLineSymbolVM.LineSymbols.Clear();

                    //    var firstLine = new LineSymbolVM
                    //    {
                    //        X1 = _symbolOutgoingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2,
                    //        Y1 = _symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height,
                    //        X2 = _symbolOutgoingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2,
                    //        Y2 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2
                    //    };

                    //    var secondLine = new LineSymbolVM
                    //    {
                    //        X1 = _symbolOutgoingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2,
                    //        Y1 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2,
                    //        X2 = _symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2,
                    //        Y2 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2
                    //    };

                    //    var thirdLine = new LineSymbolVM
                    //    {
                    //        X1 = _symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2,
                    //        Y1 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2,
                    //        X2 = _symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2,
                    //        Y2 = _symbolaIncomingLine!.YCoordinate
                    //    };

                    //    _drawnLineSymbolVM.LineSymbols.Add(firstLine);
                    //    _drawnLineSymbolVM.LineSymbols.Add(secondLine);
                    //    _drawnLineSymbolVM.LineSymbols.Add(thirdLine);
                    //}
                }
            }
            else
            {
                if (_drawnLineSymbolVM.LineSymbols.Count == 5)
                {
                    var firstLine = _drawnLineSymbolVM.LineSymbols[^1];
                    firstLine.X1 = _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate + _drawnLineSymbolVM.SymbolaIncomingLine.Width / 2;
                    firstLine.Y1 = _drawnLineSymbolVM.SymbolaIncomingLine!.YCoordinate - baseLineOffset;
                    firstLine.X2 = _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate + _drawnLineSymbolVM.SymbolaIncomingLine.Width / 2;
                    firstLine.Y2 = _drawnLineSymbolVM.SymbolaIncomingLine!.YCoordinate;

                    var secondLine = _drawnLineSymbolVM.LineSymbols[^2];
                    secondLine.X1 = (_drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width + _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate) / 2;
                    secondLine.Y1 = _drawnLineSymbolVM.SymbolaIncomingLine!.YCoordinate - baseLineOffset;
                    secondLine.X2 = _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate + _drawnLineSymbolVM.SymbolaIncomingLine.Width / 2;
                    secondLine.Y2 = _drawnLineSymbolVM.SymbolaIncomingLine!.YCoordinate - baseLineOffset;

                    var thirdLine = _drawnLineSymbolVM.LineSymbols[^3];
                    thirdLine.X1 = (_drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width + _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate) / 2;
                    thirdLine.Y1 = _drawnLineSymbolVM.SymbolOutgoingLine!.YCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Height + baseLineOffset;
                    thirdLine.X2 = (_drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width + _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate) / 2;
                    thirdLine.Y2 = _drawnLineSymbolVM.SymbolaIncomingLine!.YCoordinate - baseLineOffset;

                    var fourthLine = _drawnLineSymbolVM.LineSymbols[^4];
                    fourthLine.X1 = _drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width / 2;
                    fourthLine.Y1 = _drawnLineSymbolVM.SymbolOutgoingLine!.YCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Height + baseLineOffset;
                    fourthLine.X2 = (_drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width + _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate) / 2;
                    fourthLine.Y2 = _drawnLineSymbolVM.SymbolOutgoingLine!.YCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Height + baseLineOffset;


                    var fifthLine = _drawnLineSymbolVM.LineSymbols[^5];
                    fifthLine.X1 = _drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width / 2;
                    fifthLine.Y1 = _drawnLineSymbolVM.SymbolOutgoingLine!.YCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Height;
                    fifthLine.X2 = _drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width / 2;
                    fifthLine.Y2 = _drawnLineSymbolVM.SymbolOutgoingLine!.YCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Height + baseLineOffset;
                }
                else
                {
                    _drawnLineSymbolVM.LineSymbols.Clear();

                    var firstLine = new LineSymbolVM
                    {
                        X1 = _drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width / 2,
                        Y1 = _drawnLineSymbolVM.SymbolOutgoingLine!.YCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Height,
                        X2 = _drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width / 2,
                        Y2 = _drawnLineSymbolVM.SymbolOutgoingLine!.YCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Height + baseLineOffset
                    };

                    var secondLine = new LineSymbolVM
                    {
                        X1 = _drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width / 2,
                        Y1 = _drawnLineSymbolVM.SymbolOutgoingLine!.YCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Height + baseLineOffset,
                        X2 = (_drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width + _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate) / 2,
                        Y2 = _drawnLineSymbolVM.SymbolOutgoingLine!.YCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Height + baseLineOffset
                    };

                    var thirdLine = new LineSymbolVM
                    {
                        X1 = (_drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width + _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate) / 2,
                        Y1 = _drawnLineSymbolVM.SymbolOutgoingLine!.YCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Height + baseLineOffset,
                        X2 = (_drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width + _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate) / 2,
                        Y2 = _drawnLineSymbolVM.SymbolaIncomingLine!.YCoordinate - baseLineOffset,
                    };

                    var fourthLine = new LineSymbolVM
                    {
                        X1 = (_drawnLineSymbolVM.SymbolOutgoingLine!.XCoordinate + _drawnLineSymbolVM.SymbolOutgoingLine.Width + _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate) / 2,
                        Y1 = _drawnLineSymbolVM.SymbolaIncomingLine!.YCoordinate - baseLineOffset,
                        X2 = _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate + _drawnLineSymbolVM.SymbolaIncomingLine.Width / 2,
                        Y2 = _drawnLineSymbolVM.SymbolaIncomingLine!.YCoordinate - baseLineOffset,
                    };

                    var fifthLine = new LineSymbolVM
                    {
                        X1 = _drawnLineSymbolVM.SymbolaIncomingLine!.XCoordinate + _drawnLineSymbolVM.SymbolaIncomingLine.Width / 2,
                        Y1 = _drawnLineSymbolVM.SymbolaIncomingLine!.YCoordinate - baseLineOffset,
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
