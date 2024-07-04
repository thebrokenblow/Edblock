using System.Collections.Generic;
using System.Collections.ObjectModel;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.LinesSymbolVM;
using Microsoft.Identity.Client;

namespace EdblockViewModel.Components.CanvasSymbols.Interfaces;

public interface IListCanvasSymbolsComponentVM
{
    ObservableCollection<BlockSymbolVM> BlockSymbolsVM { get; }
    ObservableCollection<DrawnLineSymbolVM> DrawnLinesVM { get; }
    Dictionary<BlockSymbolVM, List<DrawnLineSymbolVM>> DrawnLinesByBlockSymbol { get; }
    List<BlockSymbolVM> SelectedBlockSymbolsVM { get; }
    List<DrawnLineSymbolVM> SelectedDrawnLinesVM { get; }
    List<ScalableBlockSymbolVM> ScalableBlockSymbols { get; }
    List<IHasTextFieldVM> SelectedSymbolsHasTextField { get; }
    BlockSymbolVM? MovableBlockSymbol { get; set; }

    void AddBlockSymbol(BlockSymbolVM blockSymbolVM);
    void RemoveSelectedBlockSymbols();
    void ClearSelectedBlockSymbols();
    void ChangeFocusTextField();
    void RemoveDrawnLinesVM();
    void RemoveSelectedDrawnLinesVM();
}