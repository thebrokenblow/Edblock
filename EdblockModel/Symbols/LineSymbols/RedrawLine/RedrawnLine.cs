using EdblockModel.Symbols.Enum;
using EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols;

namespace EdblockModel.Symbols.LineSymbols.RedrawLine;

public class RedrawnLine
{
    public List<CoordinateLine> DecoratedCoordinatesLines { get; set; }
    private readonly BlockSymbolModel? _symbolOutgoingLine;
    private readonly BlockSymbolModel? _symbolIncomingLine;
    private readonly PositionConnectionPoint _positionOutgoing;
    private readonly PositionConnectionPoint _positionIncoming;
    private List<CoordinateLine> _coordinatesLines;
    private readonly RedrawnLineParallelSides redrawnParallelSides;
    private readonly RedrawnLineNoParallelSides redrawnLineNoParallelSides;
    private readonly List<LineSymbolModel> _linesSymbolModel;
    private const int baseLineOffset = 20;

    public RedrawnLine(DrawnLineSymbolModel drawnLineSymbolModel)
    {
        _coordinatesLines = new();
        DecoratedCoordinatesLines = new();

        _symbolOutgoingLine = drawnLineSymbolModel.SymbolOutgoingLine;
        _symbolIncomingLine = drawnLineSymbolModel.SymbolIncomingLine;

        _positionOutgoing = drawnLineSymbolModel.OutgoingPosition;
        _positionIncoming = drawnLineSymbolModel.IncomingPosition;

        redrawnParallelSides = new(drawnLineSymbolModel, this, baseLineOffset);
        redrawnLineNoParallelSides = new(drawnLineSymbolModel, this, baseLineOffset);

        _linesSymbolModel = drawnLineSymbolModel.LinesSymbolModel;
    }

    public List<LineSymbolModel> GetRedrawLine()
    {
        var borderCoordinateOutgoingSymbol = _symbolOutgoingLine!.GetBorderCoordinate(_positionOutgoing);
        var borderCoordinateIncomingSymbol = _symbolIncomingLine!.GetBorderCoordinate(_positionIncoming);

        bool isParallel = IsParallel();

        if (isParallel)
        {
            redrawnParallelSides.RedrawLine(borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);
        }
        else
        {
            redrawnLineNoParallelSides.RedrawLine(borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);
        }

        SetCoordinateLineModel();

        return _linesSymbolModel;
    }

    public void ChangeCountLines(int currentCountLines, BuilderCoordinateDecorator? builderCoordinateDecorator = null)
    {
        if (DecoratedCoordinatesLines.Count == currentCountLines)
        {
            return;
        }

        _linesSymbolModel.Clear();
        _coordinatesLines.Clear();
        DecoratedCoordinatesLines.Clear();

        for (int i = 0; i < currentCountLines; i++)
        {
            ICoordinateDecorator coordinateSymbolOutgoing = new CoordinateDecorator();
            ICoordinateDecorator coordinateSymbolIncoming = new CoordinateDecorator();

            var coordinateLine = new CoordinateLine(coordinateSymbolOutgoing, coordinateSymbolIncoming);

            (coordinateSymbolOutgoing, coordinateSymbolIncoming) =
                SetBuilderCoordinate(coordinateSymbolOutgoing, coordinateSymbolIncoming, builderCoordinateDecorator);

            var decoratedCoordinateLine = new CoordinateLine(coordinateSymbolOutgoing, coordinateSymbolIncoming);

            var lineSymbol = new LineSymbolModel();

            _linesSymbolModel.Add(lineSymbol);
            _coordinatesLines.Add(coordinateLine);
            DecoratedCoordinatesLines.Add(decoratedCoordinateLine);
        }
    }

    public static (ICoordinateDecorator, ICoordinateDecorator) SetBuilderCoordinate(
    ICoordinateDecorator coordinateSymbolOutgoing,
    ICoordinateDecorator coordinateSymbolIncoming,
    BuilderCoordinateDecorator? builderCoordinateDecorator = null)
    {
        if (builderCoordinateDecorator == null)
        {
            return (coordinateSymbolOutgoing, coordinateSymbolIncoming);
        }
        else
        {
            coordinateSymbolOutgoing = builderCoordinateDecorator.Build(coordinateSymbolOutgoing);
            coordinateSymbolIncoming = builderCoordinateDecorator.Build(coordinateSymbolIncoming);

            return (coordinateSymbolOutgoing, coordinateSymbolIncoming);
        }
    }

    public void ReverseCoordinateLine()
    {
        foreach (var coordinatsLine in _coordinatesLines)
        {
            (coordinatsLine.CoordinateSymbolOutgoing, coordinatsLine.CoordinateSymbolIncoming) =
                (coordinatsLine.CoordinateSymbolIncoming, coordinatsLine.CoordinateSymbolOutgoing);
        }

        _coordinatesLines = Enumerable.Reverse(_coordinatesLines).ToList();
    }

    private bool IsParallel()
    {
        bool istParallel = 
            (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Top) ||
            (_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Bottom) ||
            (_positionOutgoing == PositionConnectionPoint.Right && _positionIncoming == PositionConnectionPoint.Left) ||
            (_positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Right) ||
            (_positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Left) ||
            (_positionOutgoing == PositionConnectionPoint.Right && _positionIncoming == PositionConnectionPoint.Right) ||
            (_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Top) ||
            (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Bottom);

        return istParallel;
    }

    private void SetCoordinateLineModel()
    {
        for (int i = 0; i < _coordinatesLines.Count; i++)
        {
            _linesSymbolModel[i].X1 = _coordinatesLines[i].CoordinateSymbolOutgoing.X;
            _linesSymbolModel[i].Y1 = _coordinatesLines[i].CoordinateSymbolOutgoing.Y;

            _linesSymbolModel[i].X2 = _coordinatesLines[i].CoordinateSymbolIncoming.X;
            _linesSymbolModel[i].Y2 = _coordinatesLines[i].CoordinateSymbolIncoming.Y;
        }
    }
}