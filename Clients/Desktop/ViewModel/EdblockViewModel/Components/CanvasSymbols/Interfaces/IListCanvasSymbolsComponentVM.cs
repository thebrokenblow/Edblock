using System.Collections.Generic;
using System.Collections.ObjectModel;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.LinesSymbolVM;

namespace EdblockViewModel.Components.CanvasSymbols.Interfaces;

public interface IListCanvasSymbolsComponentVM
{
    ObservableCollection<BlockSymbolVM> BlockSymbolsVM { get; }
    ObservableCollection<DrawnLineSymbolVM> DrawnLinesVM { get; }
    List<BlockSymbolVM> SelectedBlockSymbols { get; }
    List<ScalableBlockSymbolVM> ScalableBlockSymbols { get; }
    List<IHasTextFieldVM> SelectedSymbolsHasTextField { get; }
    BlockSymbolVM? MovableBlockSymbol { get; set; }

    void AddBlockSymbol(BlockSymbolVM blockSymbolVM);
    void RemoveSelectedBlockSymbols();
    void ClearSelectedBlockSymbols();
    void ChangeFocusTextField();
}