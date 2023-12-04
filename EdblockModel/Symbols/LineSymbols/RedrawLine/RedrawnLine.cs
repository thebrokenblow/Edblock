using EdblockViewModel.Symbols.LineSymbols;
using EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols;
using EdblockModel.Symbols.Abstraction;
using EdblockModel.Symbols.Enum;

namespace EdblockModel.Symbols.LineSymbols.RedrawLine;

public class RedrawnLine
{
    public List<CoordinateLine> DecoratedCoordinatesLines { get; set; }
    private readonly BlockSymbolModel _symbolOutgoingLine;
    private readonly BlockSymbolModel? _symbolIncomingLine;
    private readonly PositionConnectionPoint _positionOutgoing;
    private readonly PositionConnectionPoint _positionIncoming;
    private List<CoordinateLine> coordinatesLines;
    private readonly RedrawnLineParallelSides redrawnParallelSides;
    private readonly RedrawnLineIdenticalSides redrawnLineIdenticalSides;
    private readonly RedrawnLineNoParallelSides redrawnLineNoParallelSides;
    private List<LineSymbolModel> _linesSymbolModel;
    private const int baseLineOffset = 20;

    public RedrawnLine(DrawnLineSymbolModel drawnLineSymbolModel)
    {
        coordinatesLines = new();
        DecoratedCoordinatesLines = new();

        _symbolOutgoingLine = drawnLineSymbolModel.SymbolOutgoingLine;
        _symbolIncomingLine = drawnLineSymbolModel.SymbolIncomingLine;

        _positionOutgoing = drawnLineSymbolModel.OutgoingPosition;
        _positionIncoming = drawnLineSymbolModel.IncomingPosition;

        redrawnParallelSides = new(drawnLineSymbolModel, this, baseLineOffset);
        redrawnLineIdenticalSides = new(drawnLineSymbolModel, this, baseLineOffset);
        redrawnLineNoParallelSides = new(drawnLineSymbolModel, this, baseLineOffset);

        _linesSymbolModel = drawnLineSymbolModel.LinesSymbolModel;
    }

    public List<LineSymbolModel> GetRedrawLine()
    {
        var borderCoordinateOutgoingSymbol = _symbolOutgoingLine.GetBorderCoordinate(_positionOutgoing);
        var borderCoordinateIncomingSymbol = _symbolIncomingLine!.GetBorderCoordinate(_positionIncoming);

        redrawnParallelSides.RedrawLine();
        redrawnLineIdenticalSides.RedrawLine();

        if (IsNotParallel())
        {
            redrawnLineNoParallelSides.RedrawLine(borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);
        }

        SetCoordinateLineModel();

        return _linesSymbolModel;
    }

    private bool IsNotParallel()
    {
        bool isNotParallel = 
            (_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Right) ||
            (_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Left) ||
            (_positionOutgoing == PositionConnectionPoint.Right && _positionIncoming == PositionConnectionPoint.Top) ||
            (_positionOutgoing == PositionConnectionPoint.Right && _positionIncoming == PositionConnectionPoint.Bottom) ||
            (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Left) ||
            (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Right) ||
            (_positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Top) ||
            (_positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Bottom);

        return isNotParallel;
    }

    public void ReverseCoordinateLine()
    {
        foreach (var coordinatsLine in coordinatesLines)
        {
            (coordinatsLine.FirstCoordinate, coordinatsLine.SecondCoordinate) = (coordinatsLine.SecondCoordinate, coordinatsLine.FirstCoordinate);
        }

        coordinatesLines = Enumerable.Reverse(coordinatesLines).ToList();
    }

    public void SetCoordinateLineModel()
    {
        var countDecoratedCoordinatesLines = DecoratedCoordinatesLines.Count;
        ChangeCountLinesModel(countDecoratedCoordinatesLines);

        for (int i = 0; i < coordinatesLines.Count; i++)
        {
            var lineSymbol = _linesSymbolModel[i];

            lineSymbol.X1 = coordinatesLines[i].FirstCoordinate.X;
            lineSymbol.Y1 = coordinatesLines[i].FirstCoordinate.Y;

            lineSymbol.X2 = coordinatesLines[i].SecondCoordinate.X;
            lineSymbol.Y2 = coordinatesLines[i].SecondCoordinate.Y;
        }
    }

    private void ChangeCountLinesModel(int currentCountLines)
    {
        if (_linesSymbolModel.Count == currentCountLines)
        {
            return;
        }

        _linesSymbolModel.Clear();

        for (int i = 0; i < currentCountLines; i++)
        {
            var lineSymbol = new LineSymbolModel();
            _linesSymbolModel.Add(lineSymbol);
        }
    }

    public void ChangeCountDecoratedLines(int currentCountLines, BuilderCoordinateDecorator? builderCoordinateDecorator = null)
    {
        if (DecoratedCoordinatesLines.Count == currentCountLines)
        {
            return;
        }

        DecoratedCoordinatesLines.Clear();

        for (int i = 0; i < currentCountLines; i++)
        {
            ICoordinateDecorator firstLineCoordinate = new CoordinateDecorator();
            ICoordinateDecorator secondCoordinate = new CoordinateDecorator();

            var coordinateLine = new CoordinateLine(firstLineCoordinate, secondCoordinate);

            coordinatesLines.Add(coordinateLine);

            if (builderCoordinateDecorator is not null)
            {
                firstLineCoordinate = builderCoordinateDecorator.Build(firstLineCoordinate);
                secondCoordinate = builderCoordinateDecorator.Build(secondCoordinate);
            }

            var decoratedCoordinateLine = new CoordinateLine(firstLineCoordinate, secondCoordinate);
            DecoratedCoordinatesLines.Add(decoratedCoordinateLine);
        }
    }
}