using EdblockModel.Symbols.Enum;
using EdblockModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols;

namespace EdblockModel.Symbols.LineSymbols.RedrawLine;

public class RedrawnLine
{
    private readonly RedrawnLineParallelSides redrawnParallelSides;
    private readonly RedrawnLineIdenticalSides redrawnLineIdenticalSides;
    private readonly BlockSymbolModel _symbolOutgoingLine;
    private readonly BlockSymbolModel? _symbolaIncomingLine;
    private readonly List<CoordinateLine> coordinatesLines;
    private readonly List<LineSymbolModel> symbolLinesModel;
    private readonly PositionConnectionPoint _positionOutgoing;
    private readonly PositionConnectionPoint _positionIncoming;
    private const int baseLineOffset = 20;

    public RedrawnLine(DrawnLineSymbolModel drawnLineSymbolModel)
    {
        coordinatesLines = new();
        redrawnParallelSides = new(drawnLineSymbolModel, coordinatesLines, baseLineOffset);
        redrawnLineIdenticalSides = new(drawnLineSymbolModel, coordinatesLines, baseLineOffset);

        _symbolOutgoingLine = drawnLineSymbolModel.SymbolOutgoingLine;
        _symbolaIncomingLine = drawnLineSymbolModel.SymbolIncomingLine;

        _positionOutgoing = drawnLineSymbolModel.OutgoingPosition;
        _positionIncoming = drawnLineSymbolModel.IncomingPosition;

        symbolLinesModel = drawnLineSymbolModel.LinesSymbolModel;
    }

    public List<LineSymbolModel>? GetRedrawLine()
    {
        var borderCoordinateOutgoingSymbol = _symbolOutgoingLine.GetBorderCoordinate(_positionOutgoing);
        var borderCoordinateIncomingSymbol = _symbolaIncomingLine!.GetBorderCoordinate(_positionIncoming);

        redrawnParallelSides.GetRedrawLine(borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);
        redrawnLineIdenticalSides.GetRedrawLine(borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);

        SetCoordinateLineModel();

        return symbolLinesModel;
    }

    private void SetCoordinateLineModel()
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

    public static void ChangeCountLinesModel(List<LineSymbolModel> lineSymbolModel, int currentCountLines)
    {
        if (lineSymbolModel.Count == currentCountLines)
        {
            return;
        }

        lineSymbolModel.Clear();

        for (int i = 0; i < currentCountLines; i++)
        {
            var lineSymbol = new LineSymbolModel();
            lineSymbolModel.Add(lineSymbol);
        }
    }

    public static void ChangeCountDecoratedLines(
        List<CoordinateLine> coordinatesLines, 
        List<CoordinateLine> decoratedCoordinatesLines, 
        int currentCountLines,
        BuilderCoordinateDecorator? builderCoordinateDecorator = null)
    {
        if (decoratedCoordinatesLines.Count == currentCountLines)
        {
            return;
        }

        coordinatesLines.Clear();
        decoratedCoordinatesLines.Clear();
        builderCoordinateDecorator ??= new();

        for (int i = 0; i < currentCountLines; i++)
        {
            var firstCoordinate = new CoordinateDecorator();
            var secondCoordinate = new CoordinateDecorator();
            var coordinateLine = new CoordinateLine(firstCoordinate, secondCoordinate);
            coordinatesLines.Add(coordinateLine);

            var firstDecoratedCoordinate = builderCoordinateDecorator.Build(firstCoordinate);
            var secondDecoratedCoordinate = builderCoordinateDecorator.Build(secondCoordinate);
            var decoratedCoordinateLine = new CoordinateLine(firstDecoratedCoordinate, secondDecoratedCoordinate);
            decoratedCoordinatesLines.Add(decoratedCoordinateLine);
        }
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