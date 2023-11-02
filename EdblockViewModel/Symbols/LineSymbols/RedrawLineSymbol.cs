using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.Symbols.LineSymbols;

internal class RedrawLineSymbol
{
    private readonly BlockSymbol? _symbolaIncomingLine;
    private readonly BlockSymbol? _symbolOutgoingLine;
    private readonly DrawnLineSymbolVM _drawnLineSymbolVM;
    private readonly PositionConnectionPoint _positionOutgoing;
    private readonly PositionConnectionPoint _positionIncoming;
    private const int baseLineOffset = 20;

    public RedrawLineSymbol(DrawnLineSymbolVM drawnLineSymbolVM)
    {
        _drawnLineSymbolVM = drawnLineSymbolVM;
        
        _symbolaIncomingLine = drawnLineSymbolVM.SymbolaIncomingLine;
        _symbolOutgoingLine = drawnLineSymbolVM.SymbolOutgoingLine;

        _positionOutgoing = drawnLineSymbolVM.PositionOutgoingConnectionPoint;
        _positionIncoming = drawnLineSymbolVM.PositionIncomingConnectionPoint;  
    }

    private void SetCoordnateOneLine()
    {
        InitLines(1);

        var firstLine = _drawnLineSymbolVM.LineSymbols[^1];
        
        firstLine.X1 = _symbolOutgoingLine!.XCoordinate + _symbolOutgoingLine.Width / 2;
        firstLine.Y1 = _symbolOutgoingLine.YCoordinate + _symbolOutgoingLine.Height;
        firstLine.X2 = _symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine.Width / 2;
        firstLine.Y2 = _symbolaIncomingLine.YCoordinate;
    }

    private void InitLines(int countLine)
    {
        if (_drawnLineSymbolVM.LineSymbols.Count != countLine)
        {
            _drawnLineSymbolVM.LineSymbols.Clear();
            for (int i = 0; i < countLine; i++)
            {
                var lineSymbolVM = new LineSymbolVM();
                _drawnLineSymbolVM.LineSymbols.Add(lineSymbolVM);
            }
        }
    }

    private void SetCoordnateTreeLine()
    {
        InitLines(3);
        
        var firstLine = _drawnLineSymbolVM.LineSymbols[^3];

        firstLine.X1 = _symbolOutgoingLine!.XCoordinate + _symbolOutgoingLine!.Width / 2;
        firstLine.Y1 = _symbolOutgoingLine!.YCoordinate + _symbolOutgoingLine!.Height;
        firstLine.X2 = _symbolOutgoingLine!.XCoordinate + _symbolOutgoingLine!.Width / 2;
        firstLine.Y2 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2;

        var secondLine = _drawnLineSymbolVM.LineSymbols[^2];

        secondLine.X1 = _symbolOutgoingLine!.XCoordinate + _symbolOutgoingLine!.Width / 2;
        secondLine.Y1 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2;
        secondLine.X2 = _symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2;
        secondLine.Y2 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2;

        var thirdLine = _drawnLineSymbolVM.LineSymbols[^1];

        thirdLine.X1 = _symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2;
        thirdLine.Y1 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2;
        thirdLine.X2 = _symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2;
        thirdLine.Y2 = _symbolaIncomingLine!.YCoordinate;
    }

    private void SetCoordnateFive()
    {
        InitLines(5);
        
        var firstLine = _drawnLineSymbolVM.LineSymbols[^5];

        firstLine.X1 = _symbolOutgoingLine!.XCoordinate + _symbolOutgoingLine.Width / 2;
        firstLine.Y1 = _symbolOutgoingLine.YCoordinate + _symbolOutgoingLine.Height;
        firstLine.X2 = _symbolOutgoingLine.XCoordinate + _symbolOutgoingLine.Width / 2;
        firstLine.Y2 = _symbolOutgoingLine.YCoordinate + _symbolOutgoingLine.Height + baseLineOffset;

        var secondLine = _drawnLineSymbolVM.LineSymbols[^4];

        secondLine.X1 = _symbolOutgoingLine.XCoordinate + _symbolOutgoingLine.Width / 2;
        secondLine.Y1 = _symbolOutgoingLine.YCoordinate + _symbolOutgoingLine.Height + baseLineOffset;
        secondLine.X2 = (_symbolOutgoingLine.XCoordinate + _symbolOutgoingLine.Width + _symbolaIncomingLine!.XCoordinate) / 2;
        secondLine.Y2 = _symbolOutgoingLine.YCoordinate + _symbolOutgoingLine.Height + baseLineOffset;

        var thirdLine = _drawnLineSymbolVM.LineSymbols[^3];

        thirdLine.X1 = (_symbolOutgoingLine.XCoordinate + _symbolOutgoingLine.Width + _symbolaIncomingLine!.XCoordinate) / 2;
        thirdLine.Y1 = _symbolOutgoingLine.YCoordinate + _symbolOutgoingLine.Height + baseLineOffset;
        thirdLine.X2 = (_symbolOutgoingLine.XCoordinate + _symbolOutgoingLine.Width + _symbolaIncomingLine.XCoordinate) / 2;
        thirdLine.Y2 = _symbolaIncomingLine.YCoordinate - baseLineOffset;

        var fourthLine = _drawnLineSymbolVM.LineSymbols[^2];

        fourthLine.X1 = (_symbolOutgoingLine.XCoordinate + _symbolOutgoingLine.Width + _symbolaIncomingLine.XCoordinate) / 2;
        fourthLine.Y1 = _symbolaIncomingLine.YCoordinate - baseLineOffset;
        fourthLine.X2 = _symbolaIncomingLine.XCoordinate + _symbolaIncomingLine.Width / 2;
        fourthLine.Y2 = _symbolaIncomingLine.YCoordinate - baseLineOffset;

        var fifthLine = _drawnLineSymbolVM.LineSymbols[^1];

        fifthLine.X1 = _symbolaIncomingLine.XCoordinate + _symbolaIncomingLine.Width / 2;
        fifthLine.Y1 = _symbolaIncomingLine.YCoordinate - baseLineOffset;
        fifthLine.X2 = _symbolaIncomingLine.XCoordinate + _symbolaIncomingLine.Width / 2;
        fifthLine.Y2 = _symbolaIncomingLine.YCoordinate;
    }

    private void RedrawBottomToTop()
    {
        int unmovableBlockSymbolX = _symbolOutgoingLine!.XCoordinate;
        int unmovableBlockSymbolY = _symbolOutgoingLine.YCoordinate;

        int unmovableBlockSymbolWidth = _symbolOutgoingLine.Width;
        int unmovableBlockSymbolHeight = _symbolOutgoingLine.Height;

        int movableBlockSymbolX = _symbolaIncomingLine!.XCoordinate;
        int movableBlockSymbolY = _symbolaIncomingLine.YCoordinate;

        int movableBlockSymbolWidth = _symbolaIncomingLine.Width;

        if (unmovableBlockSymbolY + unmovableBlockSymbolHeight < movableBlockSymbolY)
        {
            if ((unmovableBlockSymbolX + unmovableBlockSymbolWidth / 2) == (movableBlockSymbolX + movableBlockSymbolWidth / 2))
            {
                SetCoordnateOneLine();
            }
            else
            {
                SetCoordnateTreeLine();
            }
        }
        else
        {
            SetCoordnateFive();
        }
    }

    public void Redraw()
    {
        if (_symbolOutgoingLine == null)
        {
            return;
        }

        if (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Top)
        {
            RedrawBottomToTop();
        }

        var lastLine = _drawnLineSymbolVM.LineSymbols[^1];
        _drawnLineSymbolVM.ArrowSymbol.ChangeOrientationArrow((lastLine.X2, lastLine.Y2), _positionIncoming);
    }
}