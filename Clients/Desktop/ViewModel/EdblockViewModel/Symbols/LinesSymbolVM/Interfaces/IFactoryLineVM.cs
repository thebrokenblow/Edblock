using EdblockModel.Lines;

namespace EdblockViewModel.Symbols.LinesSymbolVM.Interfaces;

public interface IFactoryLineVM
{
    LineSymbolVM CreateLineByModel(DrawnLineSymbolVM drawnLineSymbolVM, LineModel lineModel);
}