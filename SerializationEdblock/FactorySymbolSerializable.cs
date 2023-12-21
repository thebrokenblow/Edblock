using EdblockModel.Symbols;
using EdblockModel.Symbols.LineSymbols;

namespace SerializationEdblock;

public class FactorySymbolSerializable
{
    public static BlockSymbolSerializable CreateBlockSymbolSerializable(BlockSymbolModel blockSymbolModel)
    {
        var textFieldSerializable = new TextFieldSerializable()
        {
            Text = blockSymbolModel.TextFieldModel.Text,
            FontFamily = blockSymbolModel.TextFieldModel.FontFamily,
            FontSize = blockSymbolModel.TextFieldModel.FontSize,
            TextAlignment = blockSymbolModel.TextFieldModel.TextAlignment,
            FontWeight = blockSymbolModel.TextFieldModel.FontWeight,
            FontStyle = blockSymbolModel.TextFieldModel.FontStyle,
            TextDecorations = blockSymbolModel.TextFieldModel.TextDecorations,
        };

        var blockSymbolSerializable = new BlockSymbolSerializable
        {
            Id = blockSymbolModel.Id,
            NameSymbol = blockSymbolModel.NameSymbol,
            TextFieldSerializable = textFieldSerializable,
            Width = blockSymbolModel.Width,
            Height = blockSymbolModel.Height,
            XCoordinate = blockSymbolModel.XCoordinate,
            YCoordinate = blockSymbolModel.YCoordinate
        };

        return blockSymbolSerializable;
    }

    public static DrawnLineSymbolSerializable CreateDrawnLineSymbolSerializable(DrawnLineSymbolModel drawnLineSymbolModel)
    {
        var symbolOutgoingLine = drawnLineSymbolModel.SymbolOutgoingLine;
        var symbolIncomingLine = drawnLineSymbolModel.SymbolIncomingLine;
        var color = drawnLineSymbolModel.Color;

        BlockSymbolSerializable? symbolOutgoingLineSerializable = null;
        BlockSymbolSerializable? symbolIncomingLineSerializable = null;

        if (symbolOutgoingLine is not null && symbolIncomingLine is not null)
        {
            symbolOutgoingLineSerializable = CreateBlockSymbolSerializable(symbolOutgoingLine);
            symbolIncomingLineSerializable = CreateBlockSymbolSerializable(symbolIncomingLine);
        }

        var linesSymbolSerializable = new List<LineSymbolSerializable>();

        foreach (var linesSymbolModel in drawnLineSymbolModel.LinesSymbolModel)
        {
            var lineSymbolSerializable = CreateLineSymbolSerializable(linesSymbolModel);
            linesSymbolSerializable.Add(lineSymbolSerializable);
        }

        var drawnLineSymbolSerializable = new DrawnLineSymbolSerializable()
        {
            SymbolOutgoingLine = symbolOutgoingLineSerializable,
            SymbolIncomingLine = symbolIncomingLineSerializable,
            OutgoingPosition = drawnLineSymbolModel.OutgoingPosition,
            IncomingPosition = drawnLineSymbolModel.IncomingPosition,
            LinesSymbolSerializable = linesSymbolSerializable,
            Text = drawnLineSymbolModel.Text,
            Color = color,
        };

        return drawnLineSymbolSerializable;
    }

    private static LineSymbolSerializable CreateLineSymbolSerializable(LineSymbolModel lineSymbolModel)
    {
        var lineSymbolSerializable = new LineSymbolSerializable
        {
            X1 = lineSymbolModel.X1,
            Y1 = lineSymbolModel.Y1,
            X2 = lineSymbolModel.X2,
            Y2 = lineSymbolModel.Y2
        };

        return lineSymbolSerializable;
    }

    public static LineSymbolModel CreateLineSymbolModel(LineSymbolSerializable lineSymbolSerializable)
    {
        var lineSymbolModel = new LineSymbolModel
        {
            X1 = lineSymbolSerializable.X1,
            Y1 = lineSymbolSerializable.Y1,
            X2 = lineSymbolSerializable.X2,
            Y2 = lineSymbolSerializable.Y2
        };

        return lineSymbolModel;
    }
}