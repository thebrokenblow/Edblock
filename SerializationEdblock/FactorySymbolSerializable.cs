using EdblockModel.SymbolsModel;
using EdblockModel.SymbolsModel.LineSymbolsModel;
using SerializationEdblock.SymbolsSerializable;

namespace SerializationEdblock;

public class FactorySymbolSerializable
{
    public static BlockSymbolSerializable Create(BlockSymbolModel blockSymbolModel)
    {
        var textFieldSerializable = CreateTextFieldSerializable(blockSymbolModel.TextFieldSymbolModel);

        var blockSymbolSerializable = new BlockSymbolSerializable
        {
            Id = blockSymbolModel.Id,
            NameSymbol = blockSymbolModel.NameSymbol,
            Color = blockSymbolModel.Color,
            TextFieldSerializable = textFieldSerializable,
            Width = blockSymbolModel.Width,
            Height = blockSymbolModel.Height,
            XCoordinate = blockSymbolModel.XCoordinate,
            YCoordinate = blockSymbolModel.YCoordinate
        };

        return blockSymbolSerializable;
    }

    public static SwitchCaseSymbolsSerializable Create(SwitchCaseSymbolModel switchCaseSymbolModel)
    {
        var textFieldSerializable = CreateTextFieldSerializable(switchCaseSymbolModel.TextFieldSymbolModel);

        var switchCaseSymbolsSerializable = new SwitchCaseSymbolsSerializable
        {
            Id = switchCaseSymbolModel.Id,
            NameSymbol = switchCaseSymbolModel.NameSymbol,
            Color = switchCaseSymbolModel.Color,
            Width = switchCaseSymbolModel.Width,
            Height = switchCaseSymbolModel.Height,
            TextFieldSerializable = textFieldSerializable,
            XCoordinate = switchCaseSymbolModel.XCoordinate,
            YCoordinate = switchCaseSymbolModel.YCoordinate,
            CountLines = switchCaseSymbolModel.CountLine,
        };

        return switchCaseSymbolsSerializable;
    }

    public static ParallelActionSymbolSerializable Create(ParallelActionSymbolModel parallelActionSymbolModel)
    {
        var parallelActionSymbolSerializable = new ParallelActionSymbolSerializable
        {
            Id = parallelActionSymbolModel.Id,
            NameSymbol = parallelActionSymbolModel.NameSymbol,
            Color = parallelActionSymbolModel.Color,
            Width = parallelActionSymbolModel.Width,
            Height = parallelActionSymbolModel.Height,
            XCoordinate = parallelActionSymbolModel.XCoordinate,
            YCoordinate = parallelActionSymbolModel.YCoordinate,
            CountSymbolsOutgoing = parallelActionSymbolModel.CountSymbolsOutgoing,
            CountSymbolsIncoming = parallelActionSymbolModel.CountSymbolsIncoming,
        };

        return parallelActionSymbolSerializable;
    }

    private static TextFieldSerializable? CreateTextFieldSerializable(TextFieldSymbolModel? textFieldSymbolModel)
    {
        TextFieldSerializable? textFieldSerializable = null;

        if (textFieldSymbolModel is not null)
        {
            textFieldSerializable = new TextFieldSerializable()
            {
                Text = textFieldSymbolModel.Text,
                FontFamily = textFieldSymbolModel.FontFamily,
                FontSize = textFieldSymbolModel.FontSize,
                TextAlignment = textFieldSymbolModel.TextAlignment,
                FontWeight = textFieldSymbolModel.FontWeight,
                FontStyle = textFieldSymbolModel.FontStyle,
                TextDecorations = textFieldSymbolModel.TextDecorations,
            };
        }

        return textFieldSerializable;
    }

    public static DrawnLineSymbolSerializable CreateDrawnLineSymbolSerializable(DrawnLineSymbolModel drawnLineSymbolModel)
    {
        var symbolOutgoingLine = drawnLineSymbolModel.SymbolOutgoingLine;
        var symbolIncomingLine = drawnLineSymbolModel.SymbolIncomingLine;
        var color = drawnLineSymbolModel.Color;

        BlockSymbolSerializable? symbolOutgoingLineSerializable = null;
        BlockSymbolSerializable? symbolIncomingLineSerializable = null;

        if (symbolOutgoingLine is not null)
        {
            symbolOutgoingLineSerializable = Create(symbolOutgoingLine);
            
        }

        if (symbolIncomingLine is not null)
        {
            symbolIncomingLineSerializable = Create(symbolIncomingLine);
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