using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Shapes;
using EdblockModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.LineSymbols;

public class ListLineSymbol : Symbol
{
    public BlockSymbol? SymbolOutgoingLine { get; set; }
    public BlockSymbol? SymbolaIncomingLine { get; set; }
    public ObservableCollection<LineSymbol> LineSymbols { get; init; } = new();
    public ListLineSymbolModel LineSymbolModel { get; init; } = new();
    public ArrowSymbol ArrowSymbol { get; set; } = new();

    public ListLineSymbol(LineSymbolModel lineSymbolModel)
    {
        LineSymbolModel.LinesSymbols.Add(lineSymbolModel);
    }

    public void ChangeCoordination(int x, int y)
    {
        var linesSymbModel = LineSymbolModel.GetLines(x, y);

        ChangeOrientationArrow(x, y);
        if (linesSymbModel.Count == 1)
        {
            LineSymbols.Clear();
            var lineSymbol = FactoryLineSymbol.CreateLine(linesSymbModel[0]);
            LineSymbols.Add(lineSymbol);
        }
        else
        {
            LineSymbols.Clear();
            for (int i = 0; i < linesSymbModel.Count; i++)
            {
                var lineSymbol = FactoryLineSymbol.CreateLine(linesSymbModel[i]);
                LineSymbols.Add(lineSymbol);

            }
        }
    }

    private void ChangeOrientationArrow(int x, int y)
    {
        var firstLine = LineSymbols[0];
        if (firstLine.X1 == x)
        {
            if (y > firstLine.Y1)
            {
                ArrowSymbol.SetCoodinateBottomArrow(x, y);
            }
            else
            {
                ArrowSymbol.SetCoodinateUpperArrow(x, y);
            }
        }
        else
        {
            if (x > firstLine.X1)
            {
                ArrowSymbol.SetCoodinateRigthArrow(x, y);
            }
            else
            {
                ArrowSymbol.SetCoodinateLeftArrow(x, y);
            }
        }
    }
}