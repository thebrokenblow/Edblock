using System.Collections.Generic;
using System.Collections.ObjectModel;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.ComponentsVM.Symbols.Interfaces;

namespace EdblockViewModel.ComponentsVM.Symbols;

public class ListCanvasSymbolsVM : IListCanvasSymbolsVM
{
    public ObservableCollection<BlockSymbolVM> BlockSymbolsVM { get; } = [];
    public List<IHasTextFieldVM> BlockSymbolsHasTextField { get; } = [];
    public List<BlockSymbolVM> SelectedBlockSymbols { get; } = [];

    public void AddSymbol(BlockSymbolVM blockSymbolVM)
    {
        if (blockSymbolVM is IHasTextFieldVM blockSymbolHasTextField)
        {
            BlockSymbolsHasTextField.Add(blockSymbolHasTextField);
        }

        BlockSymbolsVM.Add(blockSymbolVM);
    }

    public void RemoveSelectedSymbols()
    {
        foreach (var selectedBlockSymbol in SelectedBlockSymbols)
        {
            selectedBlockSymbol.IsSelected = false;
        }

        SelectedBlockSymbols.Clear();
    }
}