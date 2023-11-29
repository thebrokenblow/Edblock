using EdblockModel.Symbols.Enum;
using EdblockModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols;

namespace EdblockModel.Symbols.LineSymbols;

public class RedrawnLine
{
    private readonly BlockSymbolModel _symbolOutgoingLine;
    private readonly BlockSymbolModel? _symbolaIncomingLine;
    private readonly List<CoordinateLine> coordinatesLines;
    private readonly List<CoordinateLine> decoratedCoordinatesLines;
    private readonly List<LineSymbolModel> symbolLinesModel;
    private readonly PositionConnectionPoint _positionOutgoing;
    private readonly PositionConnectionPoint _positionIncoming;
    private const int baseLineOffset = 20;

    public RedrawnLine(DrawnLineSymbolModel drawnLineSymbolModel)
    {
        _symbolOutgoingLine = drawnLineSymbolModel.SymbolOutgoingLine;
        _symbolaIncomingLine = drawnLineSymbolModel.SymbolIncomingLine;

        coordinatesLines = new();
        decoratedCoordinatesLines = new();

        _positionOutgoing = drawnLineSymbolModel.OutgoingPosition;
        _positionIncoming = drawnLineSymbolModel.IncomingPosition;

        symbolLinesModel = drawnLineSymbolModel.LinesSymbolModel;
    }

    public List<LineSymbolModel>? GetRedrawLine()
    {
        if (_symbolaIncomingLine == null)
        {
            return null;
        }

        var widthlOutgoingSymbol = _symbolOutgoingLine.Width;
        var widthlIncomingSymbol = _symbolaIncomingLine.Width;

        var borderCoordinateOutgoingSymbol = _symbolOutgoingLine.GetBorderCoordinate(_positionOutgoing);
        var borderCoordinateIncomingSymbol = _symbolaIncomingLine.GetBorderCoordinate(_positionIncoming);

        if (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Top)
        {
            if (borderCoordinateOutgoingSymbol.x == borderCoordinateIncomingSymbol.x && borderCoordinateOutgoingSymbol.y < borderCoordinateIncomingSymbol.y)
            {
                ChangeCountLines(1);
                SetCoordnateOneLine(borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);
            }
            else if (borderCoordinateOutgoingSymbol.y < borderCoordinateIncomingSymbol.y)
            {
                var buildCoordinateDecorator = new BuilderCoordinateDecorator();
                var coordinateDecoratorSymbolOutgoing = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateOutgoingSymbol));
                var coordinateDecoratorSymbolIncoming = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateIncomingSymbol));

                ChangeCountLines(3);
                ChangeCountDecoratedLines(3, buildCoordinateDecorator);
                SetCoordnateTreeLine(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming);
                SetCoordinate();
            }
            else
            {
                var buildCoordinateDecorator = new BuilderCoordinateDecorator();
                var coordinateDecoratorSymbolOutgoing = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateOutgoingSymbol));
                var coordinateDecoratorSymbolIncoming = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateIncomingSymbol));

                ChangeCountLines(5);
                ChangeCountDecoratedLines(5, buildCoordinateDecorator);
                SetCoordnateFive(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming, widthlIncomingSymbol);
                SetCoordinate();
            }
        }
        else if (_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Bottom)
        {
            if (borderCoordinateOutgoingSymbol.x == borderCoordinateIncomingSymbol.x && borderCoordinateOutgoingSymbol.y > borderCoordinateIncomingSymbol.y)
            {
                ChangeCountLines(1);
                SetCoordnateOneLine(borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);
            }
            else if (borderCoordinateOutgoingSymbol.y > borderCoordinateIncomingSymbol.y)
            {
                var buildCoordinateDecorator = new BuilderCoordinateDecorator();
                var coordinateDecoratorSymbolOutgoing = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateOutgoingSymbol));
                var coordinateDecoratorSymbolIncoming = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateIncomingSymbol));

                ChangeCountLines(3);
                ChangeCountDecoratedLines(3, buildCoordinateDecorator);
                SetCoordnateTreeLine(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming);
                SetCoordinate();
            }
            else
            {
                var buildCoordinateDecorator = new BuilderCoordinateDecorator().SetInversionYCoordinate();
                var coordinateDecoratorSymbolOutgoing = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateOutgoingSymbol));
                var coordinateDecoratorSymbolIncoming = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateIncomingSymbol));

                ChangeCountLines(5);
                ChangeCountDecoratedLines(5, buildCoordinateDecorator);
                SetCoordnateFive(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming, widthlIncomingSymbol);
                SetCoordinate();
            }
        }
        else if (_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Top)
        {
            var buildCoordinateDecorator = new BuilderCoordinateDecorator();
            var coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
            var coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

            ChangeCountLines(3);
            ChangeCountDecoratedLines(3, buildCoordinateDecorator);

            if (borderCoordinateIncomingSymbol.y <= borderCoordinateOutgoingSymbol.y)
            {
                SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, coordinateSymbolIncoming.Y);
            }
            else
            {
                SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, coordinateSymbolOutgoing.Y);
            }

            SetCoordinate();
        }
        else if (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Bottom)
        {
            var buildCoordinateDecorator = new BuilderCoordinateDecorator().SetInversionYCoordinate();
            var coordinateSymbolOutgoing = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateOutgoingSymbol));
            var coordinateSymbolIncoming = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateIncomingSymbol));

            ChangeCountLines(3);
            ChangeCountDecoratedLines(3, buildCoordinateDecorator);

            if (borderCoordinateIncomingSymbol.y <= borderCoordinateOutgoingSymbol.y)
            {
                SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, coordinateSymbolOutgoing.Y);
            }
            else
            {
                SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, coordinateSymbolIncoming.Y);
            }

            SetCoordinate();
        }

        return symbolLinesModel;
    }

    private void SetCoordnateTreeLine(ICoordinateDecorator coordinateDecoratorSymbolOutgoing, ICoordinateDecorator coordinateDecoratorSymbolIncoming)
    {
        decoratedCoordinatesLines[0].FirstCoordinate.X = coordinateDecoratorSymbolOutgoing.X;
        decoratedCoordinatesLines[0].FirstCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y;
        decoratedCoordinatesLines[0].SecondCoordinate.X = coordinateDecoratorSymbolOutgoing.X;
        decoratedCoordinatesLines[0].SecondCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y + (coordinateDecoratorSymbolIncoming.Y - coordinateDecoratorSymbolOutgoing.Y) / 2;

        decoratedCoordinatesLines[1].FirstCoordinate.X = coordinateDecoratorSymbolOutgoing.X;
        decoratedCoordinatesLines[1].FirstCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y + (coordinateDecoratorSymbolIncoming.Y - coordinateDecoratorSymbolOutgoing.Y) / 2;
        decoratedCoordinatesLines[1].SecondCoordinate.X = coordinateDecoratorSymbolIncoming.X;
        decoratedCoordinatesLines[1].SecondCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y + (coordinateDecoratorSymbolIncoming.Y - coordinateDecoratorSymbolOutgoing.Y) / 2;

        decoratedCoordinatesLines[2].FirstCoordinate.X = coordinateDecoratorSymbolIncoming.X;
        decoratedCoordinatesLines[2].FirstCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y + (coordinateDecoratorSymbolIncoming.Y - coordinateDecoratorSymbolOutgoing.Y) / 2;
        decoratedCoordinatesLines[2].SecondCoordinate.X = coordinateDecoratorSymbolIncoming.X;
        decoratedCoordinatesLines[2].SecondCoordinate.Y = coordinateDecoratorSymbolIncoming.Y;
    }

    private void SetCoordnateFive(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming, int widthIncommingSymbol)
    {
        int offsetIncommingSymbolLine = widthIncommingSymbol / 2 + baseLineOffset;

        decoratedCoordinatesLines[0].FirstCoordinate.X = coordinateSymbolOutgoing.X;
        decoratedCoordinatesLines[0].FirstCoordinate.Y = coordinateSymbolOutgoing.Y;
        decoratedCoordinatesLines[0].SecondCoordinate.X = coordinateSymbolOutgoing.X;
        decoratedCoordinatesLines[0].SecondCoordinate.Y = coordinateSymbolOutgoing.Y + baseLineOffset;

        decoratedCoordinatesLines[1].FirstCoordinate.X = coordinateSymbolOutgoing.X;
        decoratedCoordinatesLines[1].FirstCoordinate.Y = coordinateSymbolOutgoing.Y + baseLineOffset;
        decoratedCoordinatesLines[1].SecondCoordinate.X = coordinateSymbolOutgoing.X + offsetIncommingSymbolLine;
        decoratedCoordinatesLines[1].SecondCoordinate.Y = coordinateSymbolOutgoing.Y + baseLineOffset;

        decoratedCoordinatesLines[2].FirstCoordinate.X = coordinateSymbolOutgoing.X + offsetIncommingSymbolLine;
        decoratedCoordinatesLines[2].FirstCoordinate.Y = coordinateSymbolOutgoing.Y + baseLineOffset;
        decoratedCoordinatesLines[2].SecondCoordinate.X = coordinateSymbolOutgoing.X + offsetIncommingSymbolLine;
        decoratedCoordinatesLines[2].SecondCoordinate.Y = coordinateSymbolIncoming.Y - baseLineOffset;

        decoratedCoordinatesLines[3].FirstCoordinate.X = coordinateSymbolOutgoing.X + offsetIncommingSymbolLine;
        decoratedCoordinatesLines[3].FirstCoordinate.Y = coordinateSymbolIncoming.Y - baseLineOffset;
        decoratedCoordinatesLines[3].SecondCoordinate.X = coordinateSymbolIncoming.X;
        decoratedCoordinatesLines[3].SecondCoordinate.Y = coordinateSymbolIncoming.Y - baseLineOffset;

        decoratedCoordinatesLines[4].FirstCoordinate.X = coordinateSymbolIncoming.X;
        decoratedCoordinatesLines[4].FirstCoordinate.Y = coordinateSymbolIncoming.Y - baseLineOffset;
        decoratedCoordinatesLines[4].SecondCoordinate.X = coordinateSymbolIncoming.X;
        decoratedCoordinatesLines[4].SecondCoordinate.Y = coordinateSymbolIncoming.Y;
    }

    private void SetCoordinatesIdenticalSides(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming, int coordinateUnmovableSymbol)
    {
        var firstLine = decoratedCoordinatesLines[0];

        firstLine.FirstCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.FirstCoordinate.Y = coordinateSymbolOutgoing.Y;

        firstLine.SecondCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.SecondCoordinate.Y = coordinateUnmovableSymbol - baseLineOffset;

        var secondLine = decoratedCoordinatesLines[1];

        secondLine.FirstCoordinate.X = firstLine.SecondCoordinate.X;
        secondLine.FirstCoordinate.Y = firstLine.SecondCoordinate.Y;

        secondLine.SecondCoordinate.X = coordinateSymbolIncoming.X;
        secondLine.SecondCoordinate.Y = firstLine.SecondCoordinate.Y;

        var thirdLine = decoratedCoordinatesLines[2];

        thirdLine.FirstCoordinate.X = secondLine.SecondCoordinate.X;
        thirdLine.FirstCoordinate.Y = secondLine.SecondCoordinate.Y;

        thirdLine.SecondCoordinate.X = coordinateSymbolIncoming.X;
        thirdLine.SecondCoordinate.Y = coordinateSymbolIncoming.Y;
    }
    private void SetCoordinate()
    {
        for (int i = 0; i < symbolLinesModel.Count; i++)
        {
            var lineSymbol = symbolLinesModel[i];

            lineSymbol.X1 = coordinatesLines[i].FirstCoordinate.X;
            lineSymbol.Y1 = coordinatesLines[i].FirstCoordinate.Y;
            lineSymbol.X2 = coordinatesLines[i].SecondCoordinate.X;
            lineSymbol.Y2 = coordinatesLines[i].SecondCoordinate.Y;
        }
    }

    private void ChangeCountLines(int countLines)
    {
        var currentCountLines = symbolLinesModel.Count;

        if (countLines == currentCountLines)
        {
            return;
        }

        symbolLinesModel.Clear();

        for (int i = 0; i < countLines; i++)
        {
            var lineSymbol = new LineSymbolModel();
            symbolLinesModel.Add(lineSymbol);
        }
    }

    private void ChangeCountDecoratedLines(int countLines, BuilderCoordinateDecorator buildCoordinateDecorator)
    {
        var currentCountLines = decoratedCoordinatesLines.Count;

        if (countLines == currentCountLines)
        {
            return;
        }

        coordinatesLines.Clear();
        decoratedCoordinatesLines.Clear();

        for (int i = 0; i < countLines; i++)
        {
            var firstCoordinate = new CoordinateDecorator();
            var secondCoordinate = new CoordinateDecorator();
            var coordinateLine = new CoordinateLine(firstCoordinate, secondCoordinate);
            coordinatesLines.Add(coordinateLine);

            var firstDecoratedCoordinate = buildCoordinateDecorator.Build(firstCoordinate);
            var secondDecoratedCoordinate = buildCoordinateDecorator.Build(secondCoordinate);
            var decoratedCoordinateLine = new CoordinateLine(firstDecoratedCoordinate, secondDecoratedCoordinate);
            buildCoordinateDecorator.Build(secondCoordinate);

            decoratedCoordinatesLines.Add(decoratedCoordinateLine);
        }
    }

    private void SetCoordnateOneLine((int x, int y) borderCoordinateSymbolOutgoing, (int x, int y) borderCoordinateSymbolaIncoming)
    {
        var lastLine = symbolLinesModel[^1];

        lastLine.X1 = borderCoordinateSymbolOutgoing.x;
        lastLine.Y1 = borderCoordinateSymbolOutgoing.y;
        lastLine.X2 = borderCoordinateSymbolaIncoming.x;
        lastLine.Y2 = borderCoordinateSymbolaIncoming.y;
    }

    //public void Redraw()
    //{
    //    //if (_symbolOutgoingLine == null || _symbolaIncomingLine == null)
    //    //{
    //    //    return;
    //    //}

    //    //var widthlOutgoing = _symbolOutgoingLine.Width;
    //    //var widthlIncoming = _symbolaIncomingLine.Width;

    //    //var borderCoordinateSymbolOutgoing = _symbolOutgoingLine.GetBorderCoordinate(_positionOutgoing);
    //    //var borderCoordinateSymbolIncoming = _symbolaIncomingLine.GetBorderCoordinate(_positionIncoming);

    //    //if (_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Bottom)
    //    //{
    //    //    if (borderCoordinateSymbolOutgoing.x == borderCoordinateSymbolIncoming.x)
    //    //    {
    //    //        SetCoordnateOneLine(borderCoordinateSymbolOutgoing, borderCoordinateSymbolIncoming);
    //    //    }
    //    //    else if (borderCoordinateSymbolOutgoing.y > borderCoordinateSymbolIncoming.y)
    //    //    {
    //    //        var buildCoordinateDecorator = new BuilderCoordinateDecorator();
    //    //        var coordinateDecoratorSymbolOutgoing = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateSymbolOutgoing));
    //    //        var coordinateDecoratorSymbolIncoming = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateSymbolIncoming));

    //    //        SetCoordnateTreeLine(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming, buildCoordinateDecorator);
    //    //    }
    //    //    else
    //    //    {
    //    //        var buildCoordinateDecorator = new BuildCoordinateDecorator().SetInversionYCoordinate();
    //    //        var coordinateDecoratorSymbolOutgoing = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolOutgoing));
    //    //        var coordinateDecoratorSymbolIncoming = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolIncoming));

    //    //        SetCoordnateFive(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming, buildCoordinateDecorator);
    //    //    }
    //    //}
    //    //else if (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Top)
    //    //{
    //    //    if (borderCoordinateSymbolOutgoing.x == borderCoordinateSymbolIncoming.x)
    //    //    {
    //    //        SetCoordnateOneLine(borderCoordinateSymbolOutgoing, borderCoordinateSymbolIncoming);
    //    //    }
    //    //    else if (borderCoordinateSymbolOutgoing.y < borderCoordinateSymbolIncoming.y)
    //    //    {
    //    //        var buildCoordinateDecorator = new BuildCoordinateDecorator();
    //    //        var coordinateDecoratorSymbolOutgoing = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolOutgoing));
    //    //        var coordinateDecoratorSymbolIncoming = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolIncoming));

    //    //        SetCoordnateTreeLine(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming, buildCoordinateDecorator);
    //    //    }
    //    //    else
    //    //    {
    //    //        var buildCoordinateDecorator = new BuildCoordinateDecorator();
    //    //        var coordinateDecoratorSymbolOutgoing = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolOutgoing));
    //    //        var coordinateDecoratorSymbolIncoming = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolIncoming));

    //    //        SetCoordnateFive(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming, buildCoordinateDecorator);
    //    //    }
    //    //}
    //    //else if (_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Top)
    //    //{
    //    //    if ((borderCoordinateSymbolOutgoing.x - widthlOutgoing / 2 <= borderCoordinateSymbolIncoming.x
    //    //        && borderCoordinateSymbolOutgoing.x >= borderCoordinateSymbolIncoming.x)
    //    //        || borderCoordinateSymbolOutgoing.x + widthlOutgoing / 2 >= borderCoordinateSymbolIncoming.x
    //    //        && borderCoordinateSymbolOutgoing.x <= borderCoordinateSymbolIncoming.x)
    //    //    {
    //    //        var buildCoordinateDecorator = new BuildCoordinateDecorator().SetInversionXCoordinate();
    //    //        var coordinateDecoratorSymbolOutgoing = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolOutgoing));
    //    //        var coordinateDecoratorSymbolIncoming = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolIncoming));

    //    //        SetCoordnateFive1(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming, buildCoordinateDecorator);
    //    //    }
    //    //    else
    //    //    {
    //    //        var buildCoordinateDecorator = new BuildCoordinateDecorator();
    //    //        var coordinateDecoratorSymbolOutgoing = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolOutgoing));
    //    //        var coordinateDecoratorSymbolIncoming = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolIncoming));

    //    //        SetCoordnateTreeLine1(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming, buildCoordinateDecorator);
    //    //    }

    //    //}
    //    //else if (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Bottom)
    //    //{
    //    //    if (borderCoordinateSymbolOutgoing.x - widthlOutgoing / 2 <= borderCoordinateSymbolIncoming.x && borderCoordinateSymbolOutgoing.x >= borderCoordinateSymbolIncoming.x)
    //    //    {
    //    //        var buildCoordinateDecorator = new BuildCoordinateDecorator().SetInversionYCoordinate();
    //    //        var coordinateDecoratorSymbolOutgoing = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolOutgoing));
    //    //        var coordinateDecoratorSymbolIncoming = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolIncoming));

    //    //        SetCoordnateFive1(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming, buildCoordinateDecorator);
    //    //    }
    //    //    else if (borderCoordinateSymbolOutgoing.x + widthlOutgoing / 2 >= borderCoordinateSymbolIncoming.x
    //    //        && borderCoordinateSymbolOutgoing.x <= borderCoordinateSymbolIncoming.x)
    //    //    {
    //    //        var buildCoordinateDecorator = new BuildCoordinateDecorator().SetInversionYCoordinate();
    //    //        var coordinateDecoratorSymbolOutgoing = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolOutgoing));
    //    //        var coordinateDecoratorSymbolIncoming = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolIncoming));

    //    //        SetCoordnateFive2(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming, buildCoordinateDecorator);
    //    //    }
    //    //    else
    //    //    {
    //    //        var buildCoordinateDecorator = new BuildCoordinateDecorator().SetInversionYCoordinate();
    //    //        var coordinateDecoratorSymbolOutgoing = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolOutgoing));
    //    //        var coordinateDecoratorSymbolIncoming = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolIncoming));

    //    //        SetCoordnateTreeLine1(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming, buildCoordinateDecorator);
    //    //    }
    //    //}
    //    //else if (_positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Right)
    //    //{
    //    //    if (borderCoordinateSymbolOutgoing.y == borderCoordinateSymbolIncoming.y)
    //    //    {
    //    //        SetCoordnateOneLine(borderCoordinateSymbolOutgoing, borderCoordinateSymbolIncoming);
    //    //    }
    //    //    else if (borderCoordinateSymbolOutgoing.x > borderCoordinateSymbolIncoming.x)
    //    //    {
    //    //        var buildCoordinateDecorator = new BuildCoordinateDecorator().SetSwap();
    //    //        var coordinateDecoratorSymbolOutgoing = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolOutgoing));
    //    //        var coordinateDecoratorSymbolIncoming = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolIncoming));

    //    //        SetCoordnateTreeLine(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming, buildCoordinateDecorator);
    //    //    }
    //    //    else
    //    //    {
    //    //        var buildCoordinateDecorator = new BuildCoordinateDecorator().SetSwap().SetInversionXCoordinate();
    //    //        var coordinateDecoratorSymbolOutgoing = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolOutgoing));
    //    //        var coordinateDecoratorSymbolIncoming = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolIncoming));
    //    //        SetCoordnateTreeLine(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming, new BuildCoordinateDecorator().SetSwap());

    //    //        SetCoordnateFive(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming, buildCoordinateDecorator);
    //    //    }
    //    //}
    //    //else if (_positionOutgoing == PositionConnectionPoint.Right && _positionIncoming == PositionConnectionPoint.Left)
    //    //{
    //    //    if (borderCoordinateSymbolOutgoing.y == borderCoordinateSymbolIncoming.y)
    //    //    {
    //    //        SetCoordnateOneLine(borderCoordinateSymbolOutgoing, borderCoordinateSymbolIncoming);
    //    //    }
    //    //    else if (borderCoordinateSymbolOutgoing.x < borderCoordinateSymbolIncoming.x)
    //    //    {
    //    //        var buildCoordinateDecorator = new BuildCoordinateDecorator().SetSwap();
    //    //        var coordinateDecoratorSymbolOutgoing = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolOutgoing));
    //    //        var coordinateDecoratorSymbolIncoming = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolIncoming));

    //    //        SetCoordnateTreeLine(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming, buildCoordinateDecorator);
    //    //    }
    //    //    else
    //    //    {
    //    //        var buildCoordinateDecorator = new BuildCoordinateDecorator().SetSwap();
    //    //        var coordinateDecoratorSymbolOutgoing = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolOutgoing));
    //    //        var coordinateDecoratorSymbolIncoming = buildCoordinateDecorator.Create(new CoordinateDecorator(borderCoordinateSymbolIncoming));
    //    //        SetCoordnateTreeLine(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming, new BuildCoordinateDecorator().SetSwap());

    //    //        SetCoordnateFive(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming, buildCoordinateDecorator);
    //    //    }
    //    //}        
    //}

    //private void SetCoordnateOneLine((int x, int y) borderCoordinateSymbolOutgoing, (int x, int y) borderCoordinateSymbolaIncoming)
    //{
    //    ChangeCountLinesVM(1);

    //    var firstLine = _drawnLineSymbolVM.LineSymbols[^1];

    //    firstLine.X1 = borderCoordinateSymbolOutgoing.x;
    //    firstLine.Y1 = borderCoordinateSymbolOutgoing.y;
    //    firstLine.X2 = borderCoordinateSymbolaIncoming.x;
    //    firstLine.Y2 = borderCoordinateSymbolaIncoming.y;
    //}

    ////private void ChangeCountDecoratedLines(int countLines, BuildCoordinateDecorator buildCoordinateDecorator)
    ////{
    ////    var currentCountLines = DecoratedCoordinatesLines.Count;

    ////    if (countLines == currentCountLines)
    ////    {
    ////        return;
    ////    }

    ////    CoordinatesLines.Clear();
    ////    DecoratedCoordinatesLines.Clear();

    ////    for (int i = 0; i < countLines; i++)
    ////    {
    ////        var firstCoordinate = new CoordinateDecorator();
    ////        var secondCoordinate = new CoordinateDecorator();
    ////        var coordinateLine = new CoordinateLine(firstCoordinate, secondCoordinate);
    ////        CoordinatesLines.Add(coordinateLine);

    ////        var firstDecoratedCoordinate = buildCoordinateDecorator.Create(firstCoordinate);
    ////        var secondDecoratedCoordinate = buildCoordinateDecorator.Create(secondCoordinate);
    ////        var decoratedCoordinateLine = new CoordinateLine(firstDecoratedCoordinate, secondDecoratedCoordinate);
    ////        buildCoordinateDecorator.Create(secondCoordinate);

    ////        DecoratedCoordinatesLines.Add(decoratedCoordinateLine);
    ////    }
    ////}

    //private void ChangeCountLinesVM(int countLines)
    //{
    //    var currentCountLines = _drawnLineSymbolVM.LineSymbols.Count;

    //    if (countLines == currentCountLines)
    //    {
    //        return;
    //    }

    //    _drawnLineSymbolVM.LineSymbols.Clear();

    //    for (int i = 0; i < countLines; i++)
    //    {
    //        _drawnLineSymbolVM.LineSymbols.Add(new LineSymbolVM());
    //    }
    //}

    ////private void SetCoordnateTreeLine(ICoordinateDecorator coordinateDecoratorSymbolOutgoing, ICoordinateDecorator coordinateDecoratorSymbolIncoming, BuildCoordinateDecorator buildCoordinateDecorator)
    ////{
    ////    ChangeCountLinesVM(3);
    ////    ChangeCountDecoratedLines(3, buildCoordinateDecorator);

    ////    DecoratedCoordinatesLines[0].FirstCoordinate.X = coordinateDecoratorSymbolOutgoing.X;
    ////    DecoratedCoordinatesLines[0].FirstCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y;
    ////    DecoratedCoordinatesLines[0].SecondCoordinate.X = coordinateDecoratorSymbolOutgoing.X;
    ////    DecoratedCoordinatesLines[0].SecondCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y + (coordinateDecoratorSymbolIncoming.Y - coordinateDecoratorSymbolOutgoing.Y) / 2;

    ////    DecoratedCoordinatesLines[1].FirstCoordinate.X = coordinateDecoratorSymbolOutgoing.X;
    ////    DecoratedCoordinatesLines[1].FirstCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y + (coordinateDecoratorSymbolIncoming.Y - coordinateDecoratorSymbolOutgoing.Y) / 2;
    ////    DecoratedCoordinatesLines[1].SecondCoordinate.X = coordinateDecoratorSymbolIncoming.X;
    ////    DecoratedCoordinatesLines[1].SecondCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y + (coordinateDecoratorSymbolIncoming.Y - coordinateDecoratorSymbolOutgoing.Y) / 2;

    ////    DecoratedCoordinatesLines[2].FirstCoordinate.X = coordinateDecoratorSymbolIncoming.X;
    ////    DecoratedCoordinatesLines[2].FirstCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y + (coordinateDecoratorSymbolIncoming.Y - coordinateDecoratorSymbolOutgoing.Y) / 2;
    ////    DecoratedCoordinatesLines[2].SecondCoordinate.X = coordinateDecoratorSymbolIncoming.X;
    ////    DecoratedCoordinatesLines[2].SecondCoordinate.Y = coordinateDecoratorSymbolIncoming.Y;

    ////    SetCoordinate(3);
    ////}

    ////private void SetCoordnateFive(ICoordinateDecorator coordinateDecoratorSymbolOutgoing, ICoordinateDecorator coordinateDecoratorSymbolIncoming, BuildCoordinateDecorator buildCoordinateDecorator)
    ////{
    ////    ChangeCountLinesVM(5);
    ////    ChangeCountDecoratedLines(5, buildCoordinateDecorator);

    ////    DecoratedCoordinatesLines[0].FirstCoordinate.X = coordinateDecoratorSymbolOutgoing.X;
    ////    DecoratedCoordinatesLines[0].FirstCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y;
    ////    DecoratedCoordinatesLines[0].SecondCoordinate.X = coordinateDecoratorSymbolOutgoing.X;
    ////    DecoratedCoordinatesLines[0].SecondCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y + baseLineOffset;

    ////    DecoratedCoordinatesLines[1].FirstCoordinate.X = coordinateDecoratorSymbolOutgoing.X;
    ////    DecoratedCoordinatesLines[1].FirstCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y + baseLineOffset;
    ////    DecoratedCoordinatesLines[1].SecondCoordinate.X = coordinateDecoratorSymbolOutgoing.X + (coordinateDecoratorSymbolIncoming.X - coordinateDecoratorSymbolOutgoing.X) / 4;
    ////    DecoratedCoordinatesLines[1].SecondCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y + baseLineOffset;

    ////    DecoratedCoordinatesLines[2].FirstCoordinate.X = coordinateDecoratorSymbolOutgoing.X + (coordinateDecoratorSymbolIncoming.X - coordinateDecoratorSymbolOutgoing.X) / 4;
    ////    DecoratedCoordinatesLines[2].FirstCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y + baseLineOffset;
    ////    DecoratedCoordinatesLines[2].SecondCoordinate.X = coordinateDecoratorSymbolOutgoing.X + (coordinateDecoratorSymbolIncoming.X - coordinateDecoratorSymbolOutgoing.X) / 4;
    ////    DecoratedCoordinatesLines[2].SecondCoordinate.Y = coordinateDecoratorSymbolIncoming.Y - baseLineOffset;

    ////    DecoratedCoordinatesLines[3].FirstCoordinate.X = coordinateDecoratorSymbolOutgoing.X + (coordinateDecoratorSymbolIncoming.X - coordinateDecoratorSymbolOutgoing.X) / 4;
    ////    DecoratedCoordinatesLines[3].FirstCoordinate.Y = coordinateDecoratorSymbolIncoming.Y - baseLineOffset;
    ////    DecoratedCoordinatesLines[3].SecondCoordinate.X = coordinateDecoratorSymbolIncoming.X;
    ////    DecoratedCoordinatesLines[3].SecondCoordinate.Y = coordinateDecoratorSymbolIncoming.Y - baseLineOffset;

    ////    DecoratedCoordinatesLines[4].FirstCoordinate.X = coordinateDecoratorSymbolIncoming.X;
    ////    DecoratedCoordinatesLines[4].FirstCoordinate.Y = coordinateDecoratorSymbolIncoming.Y - baseLineOffset;
    ////    DecoratedCoordinatesLines[4].SecondCoordinate.X = coordinateDecoratorSymbolIncoming.X;
    ////    DecoratedCoordinatesLines[4].SecondCoordinate.Y = coordinateDecoratorSymbolIncoming.Y;

    ////    SetCoordinate(5);
    ////}

    ////private void SetCoordnateFive2(ICoordinateDecorator coordinateDecoratorSymbolOutgoing, ICoordinateDecorator coordinateDecoratorSymbolIncoming, BuildCoordinateDecorator buildCoordinateDecorator)
    ////{
    ////    ChangeCountLinesVM(5);
    ////    ChangeCountDecoratedLines(5, buildCoordinateDecorator);

    ////    DecoratedCoordinatesLines[0].FirstCoordinate.X = coordinateDecoratorSymbolOutgoing.X;
    ////    DecoratedCoordinatesLines[0].FirstCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y;
    ////    DecoratedCoordinatesLines[0].SecondCoordinate.X = coordinateDecoratorSymbolOutgoing.X;
    ////    DecoratedCoordinatesLines[0].SecondCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y - baseLineOffset;

    ////    DecoratedCoordinatesLines[1].FirstCoordinate.X = coordinateDecoratorSymbolOutgoing.X;
    ////    DecoratedCoordinatesLines[1].FirstCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y - baseLineOffset;
    ////    DecoratedCoordinatesLines[1].SecondCoordinate.X = coordinateDecoratorSymbolOutgoing.X - coordinateDecoratorSymbolOutgoing.X / 2;
    ////    DecoratedCoordinatesLines[1].SecondCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y - baseLineOffset;

    ////    DecoratedCoordinatesLines[2].FirstCoordinate.X = coordinateDecoratorSymbolOutgoing.X - coordinateDecoratorSymbolOutgoing.X / 2;
    ////    DecoratedCoordinatesLines[2].FirstCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y - baseLineOffset;
    ////    DecoratedCoordinatesLines[2].SecondCoordinate.X = coordinateDecoratorSymbolOutgoing.X - coordinateDecoratorSymbolOutgoing.X / 2;
    ////    DecoratedCoordinatesLines[2].SecondCoordinate.Y = coordinateDecoratorSymbolIncoming.Y - baseLineOffset;

    ////    DecoratedCoordinatesLines[3].FirstCoordinate.X = coordinateDecoratorSymbolOutgoing.X - coordinateDecoratorSymbolOutgoing.X / 2;
    ////    DecoratedCoordinatesLines[3].FirstCoordinate.Y = coordinateDecoratorSymbolIncoming.Y - baseLineOffset;
    ////    DecoratedCoordinatesLines[3].SecondCoordinate.X = coordinateDecoratorSymbolIncoming.X;
    ////    DecoratedCoordinatesLines[3].SecondCoordinate.Y = coordinateDecoratorSymbolIncoming.Y - baseLineOffset;

    ////    DecoratedCoordinatesLines[4].FirstCoordinate.X = coordinateDecoratorSymbolIncoming.X;
    ////    DecoratedCoordinatesLines[4].FirstCoordinate.Y = coordinateDecoratorSymbolIncoming.Y - baseLineOffset;
    ////    DecoratedCoordinatesLines[4].SecondCoordinate.X = coordinateDecoratorSymbolIncoming.X;
    ////    DecoratedCoordinatesLines[4].SecondCoordinate.Y = coordinateDecoratorSymbolIncoming.Y;

    ////    SetCoordinate(5);
    ////}

    //private void SetCoordinate(int countLine)
    //{
    //    for (int i = 0; i < countLine; i++)
    //    {
    //        var lineSymbol = _drawnLineSymbolVM.LineSymbols[i];
    //        lineSymbol.X1 = CoordinatesLines[i].FirstCoordinate.X;
    //        lineSymbol.Y1 = CoordinatesLines[i].FirstCoordinate.Y;
    //        lineSymbol.X2 = CoordinatesLines[i].SecondCoordinate.X;
    //        lineSymbol.Y2 = CoordinatesLines[i].SecondCoordinate.Y;
    //    }
    //}
}