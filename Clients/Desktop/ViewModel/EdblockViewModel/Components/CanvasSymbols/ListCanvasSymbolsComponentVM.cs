using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;
using EdblockViewModel.Symbols.Abstractions;

namespace EdblockViewModel.Components.CanvasSymbols;

public class ListCanvasSymbolsComponentVM(IPopupBoxMenuComponentVM popupBoxMenuComponentVM) : IListCanvasSymbolsComponentVM
{
    public ObservableCollection<BlockSymbolVM> BlockSymbolsVM { get; } = [];
    public List<ScalableBlockSymbolVM> ScalableBlockSymbols { get; } = [];
    public List<BlockSymbolVM> SelectedBlockSymbols { get; } = [];
    public List<IHasTextFieldVM> SelectedSymbolsHasTextField { get; } = [];
    public BlockSymbolVM? MovableBlockSymbol { get; set; }

    public void AddBlockSymbol(BlockSymbolVM blockSymbolVM)
    {
        var firstBlockSymbolsVM = BlockSymbolsVM.FirstOrDefault();

        var isScaleAllSymbolVM = popupBoxMenuComponentVM.ScaleAllSymbolComponentVM.IsScaleAllSymbol;

        if (isScaleAllSymbolVM && firstBlockSymbolsVM is not null && blockSymbolVM is ScalableBlockSymbolVM scalableBlockSymbolVM)
        {
            scalableBlockSymbolVM.SetWidth(firstBlockSymbolsVM.Width);
            scalableBlockSymbolVM.SetHeight(firstBlockSymbolsVM.Height);

            ScalableBlockSymbols.Add(scalableBlockSymbolVM);
        }

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