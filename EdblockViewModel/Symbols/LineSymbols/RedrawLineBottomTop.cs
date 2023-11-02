using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.LineSymbols;

internal class RedrawLineBottomTop
{
    private readonly BlockSymbol? _symbolaIncomingLine;
    private readonly BlockSymbol? _symbolOutgoingLine;
    private readonly DrawnLineSymbolVM _drawnLineSymbolVM;
    private const int oneLine = 1;
    private const int treeLine = 3;
    private const int fiveLine = 5;
    private const int baseLineOffset = 20;

    public RedrawLineBottomTop(DrawnLineSymbolVM drawnLineSymbolVM)
    {
        _drawnLineSymbolVM = drawnLineSymbolVM;

        _symbolaIncomingLine = drawnLineSymbolVM.SymbolaIncomingLine;
        _symbolOutgoingLine = drawnLineSymbolVM.SymbolOutgoingLine;
    }

    public void Redraw()
    {
        if (_symbolOutgoingLine!.YCoordinate + _symbolOutgoingLine.Height < _symbolaIncomingLine!.YCoordinate)
        {
            if ((_symbolOutgoingLine!.XCoordinate + _symbolOutgoingLine.Width / 2) == (_symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine.Width / 2))
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

    private void SetCoordnateOneLine()
    {
        InitLines(oneLine);

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
        InitLines(treeLine);

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
        InitLines(fiveLine);

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
}