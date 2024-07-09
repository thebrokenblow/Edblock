using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.LinesSymbolVM;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;
using EdblockComponentsViewModel.Subjects.Interfaces;
using EdblockComponentsViewModel.Observers.Interfaces;

namespace EdblockViewModel.Components.CanvasSymbols;

public class ListCanvasSymbolsComponentVM(IPopupBoxMenuComponentVM popupBoxMenuComponentVM, IFontSizeSubject<int> fontSizeSubject) : IListCanvasSymbolsComponentVM
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
        if (blockSymbolVM is IObserverFontSize observerFontSize)
        {
            fontSizeSubject.RegisterObserver(observerFontSize);
            observerFontSize.UpdateFontSize();
        }

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

    public void ChangeFocusTextField()
    {
        foreach (var blockSymbolHasTextField in SelectedSymbolsHasTextField)
        {
            blockSymbolHasTextField.TextFieldSymbolVM.Focusable = false;
        }
    }

    public void RemoveSelectedBlockSymbols()
    {
        MovableBlockSymbol = null;

        foreach (var selectedBlockSymbolsVM in SelectedBlockSymbolsVM)
        {
            if (DrawnLinesByBlockSymbol.TryGetValue(selectedBlockSymbolsVM, out List<DrawnLineSymbolVM>? drawnLinesVM))
            {
                foreach (var drawnLineVM in drawnLinesVM)
                {
                    if (drawnLineVM.OutgoingConnectionPoint is not null && drawnLineVM.OutgoingBlockSymbol is not null && drawnLineVM.OutgoingBlockSymbol != selectedBlockSymbolsVM)
                    {
                        drawnLineVM.OutgoingConnectionPoint.IsHasConnectingLine = false;
                        RemoveLineByBlock(drawnLineVM.OutgoingBlockSymbol, drawnLineVM);
                    }

                    if (drawnLineVM.IncommingConnectionPoint != null && drawnLineVM.IncommingBlockSymbol is not null && drawnLineVM.IncommingBlockSymbol != selectedBlockSymbolsVM)
                    {
                        drawnLineVM.IncommingConnectionPoint.IsHasConnectingLine = false;
                        RemoveLineByBlock(drawnLineVM.IncommingBlockSymbol, drawnLineVM);
                    }

                    DrawnLinesVM.Remove(drawnLineVM);
                }
            }

            BlockSymbolsVM.Remove(selectedBlockSymbolsVM);
        }

        SelectedBlockSymbolsVM.Clear();
    }

    public void RemoveSelectedDrawnLinesVM()
    {
        foreach (var selectedDrawnLinesVM in SelectedDrawnLinesVM)
        {
            if (selectedDrawnLinesVM.OutgoingConnectionPoint is not null && selectedDrawnLinesVM.OutgoingBlockSymbol is not null)
            {
                selectedDrawnLinesVM.OutgoingConnectionPoint.IsHasConnectingLine = false;
                RemoveLineByBlock(selectedDrawnLinesVM.OutgoingBlockSymbol, selectedDrawnLinesVM);
            }

            if (selectedDrawnLinesVM.IncommingConnectionPoint is not null && selectedDrawnLinesVM.IncommingBlockSymbol is not null)
            {
                selectedDrawnLinesVM.IncommingConnectionPoint.IsHasConnectingLine = false;
                RemoveLineByBlock(selectedDrawnLinesVM.IncommingBlockSymbol, selectedDrawnLinesVM);
            }

            DrawnLinesVM.Remove(selectedDrawnLinesVM);
        }

        SelectedDrawnLinesVM.Clear();
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

    public void ClearSelectedDrawnLinesVM()
    {
        foreach (var selectedDrawnLinesVM in SelectedDrawnLinesVM)
        {
            selectedDrawnLinesVM.Unselect();
        }

        SelectedDrawnLinesVM.Clear();
    }

    private void RemoveLineByBlock(BlockSymbolVM blockSymbolVM, DrawnLineSymbolVM drawnLineSymbolVM)
    {
        var drawnLinesVM = DrawnLinesByBlockSymbol[blockSymbolVM];
        drawnLinesVM.Remove(drawnLineSymbolVM);

        if (drawnLinesVM.Count == 0)
        {
            DrawnLinesByBlockSymbol.Remove(blockSymbolVM);
        }
    }
}