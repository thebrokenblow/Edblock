using System.Collections.Generic;
using System.Collections.ObjectModel;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.ComponentsVM.CanvasSymbols.Interfaces;

namespace EdblockViewModel.ComponentsVM.CanvasSymbols;

public class ListCanvasSymbolsVM : IListCanvasSymbolsVM
{
    public ObservableCollection<BlockSymbolVM> BlockSymbolsVM { get; } = [];
    public List<BlockSymbolVM> SelectedBlockSymbols { get; } = [];
    public List<IHasTextFieldVM> SelectedSymbolsHasTextField { get; } = [];
    public BlockSymbolVM? MovableBlockSymbol { get; set; }

    public void AddBlockSymbol(BlockSymbolVM blockSymbolVM)
    {
        BlockSymbolsVM.Add(blockSymbolVM);

        ClearSelectedBlockSymbols();

        MovableBlockSymbol = blockSymbolVM;
        MovableBlockSymbol.MoveMiddle = true;
        MovableBlockSymbol.SetSelectedProperties();
    }

    public void RemoveSelectedBlockSymbols()
    {
        MovableBlockSymbol = null;

        foreach (var symbol in SelectedBlockSymbols)
        {
            BlockSymbolsVM.Remove(symbol);
        }

        SelectedBlockSymbols.Clear();
    }

    public void ClearSelectedBlockSymbols()
    {
        foreach (var selectedBlockSymbols in SelectedBlockSymbols)
        {
            if (selectedBlockSymbols != MovableBlockSymbol)
            {
                selectedBlockSymbols.IsSelected = false;
            }
        }

        SelectedBlockSymbols.RemoveAll(selectedBlockSymbol => selectedBlockSymbol != MovableBlockSymbol);
        SelectedSymbolsHasTextField.RemoveAll(selectedBlockSymbol => selectedBlockSymbol != MovableBlockSymbol);
    }

    public void ChangeFocusTextField()
    {
        foreach (var blockSymbolHasTextField in SelectedSymbolsHasTextField)
        {
            blockSymbolHasTextField.TextFieldSymbolVM.Focusable = false;
        }
    }
}