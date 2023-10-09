using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EdblockModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.Symbols.LineSymbols;

public class ListLineSymbol : Symbol
{
    public BlockSymbol? SymbolOutgoingLine { get; set; }
    public BlockSymbol? SymbolaIncomingLine { get; set; }
    public ListLineSymbolModel LineSymbolModel { get; init; }
    public ObservableCollection<LineSymbol> LineSymbols { get; init; } = new();
    public ArrowSymbol ArrowSymbol { get; set; } = new();
    public PositionConnectionPoint PositionConnectionPoint { get; init; }

    public ListLineSymbol(LineSymbolModel lineSymbolModel, PositionConnectionPoint positionConnectionPoint)
    {
        LineSymbolModel = new(positionConnectionPoint);
        LineSymbolModel.LinesSymbols.Add(lineSymbolModel);
        PositionConnectionPoint = positionConnectionPoint;
    }

    public void ChangeCoordination(int currentX, int currentY)
    {
        var linesSymbolModel = LineSymbolModel.LinesSymbols;

        var startCoordinate = CoordinateLineModel.GetStartCoordinate(LineSymbolModel.LinesSymbols);
        var currentCoordinateLine = (currentX, currentY);
        
        ArrowSymbol.ChangeOrientationArrow(startCoordinate, currentCoordinateLine, PositionConnectionPoint);
        LineSymbolModel.ChangeCoordinateLine(currentX, currentY);

        if (linesSymbolModel.Count == 1)
        {
            AddFirstLine(linesSymbolModel);
        }
        else
        {
            AddCurrentLine(linesSymbolModel);
        }
    }

    private void AddCurrentLine(List<LineSymbolModel> linesSymbolModel)
    {
        var currentLinesSymbolModel = linesSymbolModel.TakeLast(2).ToList();

        if (linesSymbolModel.Count < LineSymbols.Count)
        {
            DeleteLastLine(currentLinesSymbolModel);
        }
        else if (linesSymbolModel.Count > LineSymbols.Count)
        {
            AddNewLine(currentLinesSymbolModel);
        }
        else if (linesSymbolModel.Count == LineSymbols.Count)
        {
            ChangeCurrentLine(linesSymbolModel);
        }
    }

    private void DeleteLastLine(List<LineSymbolModel> linesSymbModel)
    {
        LineSymbols.RemoveAt(LineSymbols.Count - 1);
        LineSymbols[^1] = FactoryLineSymbol.CreateLine(linesSymbModel[1]);
    }

    private void AddNewLine(List<LineSymbolModel> linesSymbModel)
    {
        LineSymbols[^1] = FactoryLineSymbol.CreateLine(linesSymbModel[0]);
        LineSymbols.Add(FactoryLineSymbol.CreateLine(linesSymbModel[1]));
    }

    private void ChangeCurrentLine(List<LineSymbolModel> linesSymbolModel)
    {
        for (int i = linesSymbolModel.Count - 2; i < linesSymbolModel.Count; i++)
        {
            LineSymbols[i] = FactoryLineSymbol.CreateLine(linesSymbolModel[i]);
        }
    }

    private void AddFirstLine(List<LineSymbolModel> linesSymbolModel)
    {
        var firstLineSymbolModel = linesSymbolModel[0];
        var firstLineSymbolVM = FactoryLineSymbol.CreateLine(firstLineSymbolModel);
        
        if (LineSymbols.Count == 0)
        {
            LineSymbols.Add(firstLineSymbolVM);
        }
        else
        {
            LineSymbols[0] = firstLineSymbolVM;
        }
    }
}