using System.Linq;
using EdblockViewModel.PagesVM;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.ComponentsVM.CanvasSymbols;

namespace EdblockViewModel.ComponentsVM;

public class ListSymbolsVM(EditorVM editorVM)
{
    public EditorVM EditorVM => editorVM;

    private readonly CanvasSymbolsVM canvasSymbolsVM = editorVM.CanvasSymbolsVM;
    private readonly PopupBoxMenuVM popupBoxMenuVM = editorVM.PopupBoxMenuVM;

    public void AddBlockSymbol(BlockSymbolVM blockSymbolVM)
    {
        var firstBlockSymbolsVM = canvasSymbolsVM.ListCanvasSymbolsVM.BlockSymbolsVM.FirstOrDefault();
        var isScaleAllSymbolVM = popupBoxMenuVM.ScaleAllSymbolVM.IsScaleAllSymbol;

        if (isScaleAllSymbolVM && firstBlockSymbolsVM is BlockSymbolVM firstBlockSymbolVM)
        {
            blockSymbolVM.SetWidth(firstBlockSymbolVM.Width);
            blockSymbolVM.SetHeight(firstBlockSymbolVM.Height);
        }

        canvasSymbolsVM.ListCanvasSymbolsVM.AddBlockSymbol(blockSymbolVM);
    }
}