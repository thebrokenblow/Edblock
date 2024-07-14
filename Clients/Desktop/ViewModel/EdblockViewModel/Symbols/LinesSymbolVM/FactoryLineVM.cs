using EdblockModel.Lines;
using EdblockViewModel.Symbols.LinesSymbolVM.Interfaces;

namespace EdblockViewModel.Symbols.LinesSymbolVM;

public class FactoryLineVM : IFactoryLineVM
{
    public LineSymbolVM CreateLineByModel(DrawnLineSymbolVM drawnLineSymbolVM, LineModel lineModel) =>
        new(drawnLineSymbolVM)
        {
            X1 = lineModel.FirstCoordinate.X,
            Y1 = lineModel.FirstCoordinate.Y,
            X2 = lineModel.SecondCoordinate.X,
            Y2 = lineModel.SecondCoordinate.Y,
        };
}