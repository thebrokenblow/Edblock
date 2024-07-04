using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.LinesSymbolVM;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Components.CanvasSymbols;

public class ListCanvasSymbolsComponentVM(IPopupBoxMenuComponentVM popupBoxMenuComponentVM) : IListCanvasSymbolsComponentVM
{
    public ObservableCollection<BlockSymbolVM> BlockSymbolsVM { get; } = [];
    public ObservableCollection<DrawnLineSymbolVM> DrawnLinesVM { get; } = [];
    public Dictionary<BlockSymbolVM, List<DrawnLineSymbolVM>> DrawnLinesByBlockSymbol { get; } = [];
    public List<ScalableBlockSymbolVM> ScalableBlockSymbols { get; } = [];
    public List<DrawnLineSymbolVM> SelectedDrawnLinesVM { get; } = [];
    public List<BlockSymbolVM> SelectedBlockSymbolsVM { get; } = [];
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

        foreach (var symbol in SelectedBlockSymbolsVM)
        {
            BlockSymbolsVM.Remove(symbol);
        }

        SelectedBlockSymbolsVM.Clear();
    }

    public void ClearSelectedBlockSymbols()
    {
        foreach (var selectedBlockSymbols in SelectedBlockSymbolsVM)
        {
            if (selectedBlockSymbols != MovableBlockSymbol)
            {
                selectedBlockSymbols.IsSelected = false;
            }
        }

        SelectedBlockSymbolsVM.RemoveAll(selectedBlockSymbol => selectedBlockSymbol != MovableBlockSymbol);
        SelectedSymbolsHasTextField.RemoveAll(selectedBlockSymbol => selectedBlockSymbol != MovableBlockSymbol);
    }

    public void ChangeFocusTextField()
    {
        foreach (var blockSymbolHasTextField in SelectedSymbolsHasTextField)
        {
            blockSymbolHasTextField.TextFieldSymbolVM.Focusable = false;
        }
    }

    public void RemoveDrawnLinesVM()
    {
        foreach (var selectedDrawnLinesVM in SelectedDrawnLinesVM)
        {
            if (selectedDrawnLinesVM.OutgoingConnectionPoint is not null)
            {
                selectedDrawnLinesVM.OutgoingConnectionPoint.IsHasConnectingLine = false;
            }

            if (selectedDrawnLinesVM.IncommingConnectionPoint is not null)
            {
                selectedDrawnLinesVM.IncommingConnectionPoint.IsHasConnectingLine = false;
            }

            DrawnLinesByBlockSymbol.Where(drawnLineByBlockSymbol => drawnLineByBlockSymbol.Value.Remove(selectedDrawnLinesVM));
            DrawnLinesVM.Remove(selectedDrawnLinesVM);
        }
        SelectedDrawnLinesVM.Clear();
    }

    public void RemoveSelectedDrawnLinesVM()
    {
        foreach (var selectedDrawnLinesVM in SelectedDrawnLinesVM)
        {
            selectedDrawnLinesVM.Unselect();
        }

        SelectedDrawnLinesVM.Clear();
    }
}