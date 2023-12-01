using EdblockModel.Symbols.Enum;
using EdblockModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols;

namespace EdblockModel.Symbols.LineSymbols.RedrawLine;

public class RedrawnLine
{
    public List<CoordinateLine> DecoratedCoordinatesLines { get; init; }

    private readonly RedrawnLineParallelSides redrawnParallelSides;
    private readonly RedrawnLineIdenticalSides redrawnLineIdenticalSides;
    private readonly BlockSymbolModel _symbolOutgoingLine;
    private readonly BlockSymbolModel? _symbolaIncomingLine;
    private readonly List<CoordinateLine> _coordinatesLines;
    private readonly List<LineSymbolModel> _linesSymbolModel;
    private readonly PositionConnectionPoint _positionOutgoing;
    private readonly PositionConnectionPoint _positionIncoming;
    private const int baseLineOffset = 40;

    public RedrawnLine(DrawnLineSymbolModel drawnLineSymbolModel)
    {
        DecoratedCoordinatesLines = new();

        redrawnParallelSides = new(drawnLineSymbolModel, this, baseLineOffset);
        redrawnLineIdenticalSides = new(drawnLineSymbolModel, this, baseLineOffset);

        _symbolOutgoingLine = drawnLineSymbolModel.SymbolOutgoingLine;
        _symbolaIncomingLine = drawnLineSymbolModel.SymbolIncomingLine;
        _linesSymbolModel = drawnLineSymbolModel.LinesSymbolModel;

        _coordinatesLines = new();

        _positionOutgoing = drawnLineSymbolModel.OutgoingPosition;
        _positionIncoming = drawnLineSymbolModel.IncomingPosition;
    }

    public List<LineSymbolModel> GetRedrawLine()
    {
        var borderCoordinateOutgoingSymbol = _symbolOutgoingLine.GetBorderCoordinate(_positionOutgoing);
        var borderCoordinateIncomingSymbol = _symbolaIncomingLine!.GetBorderCoordinate(_positionIncoming);

        redrawnParallelSides.RedrawLine(borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);
        redrawnLineIdenticalSides.RedrawLine(borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);

        SetCoordinateLineModel();

        return _linesSymbolModel;
    }

    private void SetCoordinateLineModel()
    {
        for (int i = 0; i < _linesSymbolModel.Count; i++)
        {
            var lineSymbol = _linesSymbolModel[i];

            lineSymbol.X1 = _coordinatesLines[i].FirstCoordinate.X;
            lineSymbol.Y1 = _coordinatesLines[i].FirstCoordinate.Y;
            lineSymbol.X2 = _coordinatesLines[i].SecondCoordinate.X;
            lineSymbol.Y2 = _coordinatesLines[i].SecondCoordinate.Y;
        }
    }

    public void ChangeCountLinesModel(int currentCountLines)
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

        _coordinatesLines.Clear();
        DecoratedCoordinatesLines.Clear();

        for (int i = 0; i < currentCountLines; i++)
        {
            ICoordinateDecorator firstCoordinate = new CoordinateDecorator();
            ICoordinateDecorator secondCoordinate = new CoordinateDecorator();
            var coordinateLine = new CoordinateLine(firstCoordinate, secondCoordinate);
            _coordinatesLines.Add(coordinateLine);

            if (builderCoordinateDecorator is not null)
            {
                firstCoordinate = builderCoordinateDecorator.Build(firstCoordinate);
                secondCoordinate = builderCoordinateDecorator.Build(secondCoordinate);
            }

            var decoratedCoordinateLine = new CoordinateLine(firstCoordinate, secondCoordinate);
            DecoratedCoordinatesLines.Add(decoratedCoordinateLine);
        }
    }
}