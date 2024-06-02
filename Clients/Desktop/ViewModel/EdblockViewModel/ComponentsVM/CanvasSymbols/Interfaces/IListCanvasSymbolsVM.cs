using System.Collections.Generic;
using System.Collections.ObjectModel;
using EdblockViewModel.AbstractionsVM;

namespace EdblockViewModel.ComponentsVM.CanvasSymbols.Interfaces;

public interface IListCanvasSymbolsVM
{
    ObservableCollection<BlockSymbolVM> BlockSymbolsVM { get; }
    List<BlockSymbolVM> SelectedBlockSymbols { get; }
    List<IHasTextFieldVM> SelectedSymbolsHasTextField { get; }
    BlockSymbolVM? MovableBlockSymbol { get; set; }

    void AddBlockSymbol(BlockSymbolVM blockSymbolVM);
    void RemoveSelectedBlockSymbols();
    void ClearSelectedBlockSymbols();
    void ChangeFocusTextField();
}