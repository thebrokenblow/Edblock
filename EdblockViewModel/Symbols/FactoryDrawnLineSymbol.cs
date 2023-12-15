using SerializationEdblock;
using System.Collections.Generic;
using EdblockModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

internal class FactoryDrawnLineSymbol
{
    public static DrawnLineSymbolModel CreateDrawnLineSymbolModel(
        BlockSymbolVM symbolOutgoingLineVM, 
        BlockSymbolVM symbolIncomingLineVM, 
        DrawnLineSymbolSerializable drawnLineSymbolSerializable,
        List<LineSymbolModel> linesSymbolModel)
    {
        var symbolOutgoingLineModel = symbolOutgoingLineVM.BlockSymbolModel;
        var symbolIncomingLineModel = symbolIncomingLineVM.BlockSymbolModel;

        var outgoingPosition = drawnLineSymbolSerializable.OutgoingPosition;
        var incomingPosition = drawnLineSymbolSerializable.IncomingPosition;

        var text = drawnLineSymbolSerializable.Text;
        var color = drawnLineSymbolSerializable.Color;

        var drawnLineSymbolModel = new DrawnLineSymbolModel()
        {
            LinesSymbolModel = linesSymbolModel,
            SymbolOutgoingLine = symbolOutgoingLineModel,
            SymbolIncomingLine = symbolIncomingLineModel,
            OutgoingPosition = outgoingPosition,
            IncomingPosition = incomingPosition,
            Text = text,
            Color = color,
        };

        return drawnLineSymbolModel;
    }
}