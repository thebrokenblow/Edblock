using EdblockViewModel.AbstractionsVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EdblockViewModel.ComponentsVM.Symbols.Interfaces;

public interface IListCanvasSymbolsVM
{
    ObservableCollection<BlockSymbolVM> BlockSymbolsVM { get; }
    List<IHasTextFieldVM> BlockSymbolsHasTextField { get; }
    List<BlockSymbolVM> SelectedBlockSymbols { get; }

    void AddSymbol(BlockSymbolVM blockSymbolVM);
    void RemoveSelectedSymbols();
}