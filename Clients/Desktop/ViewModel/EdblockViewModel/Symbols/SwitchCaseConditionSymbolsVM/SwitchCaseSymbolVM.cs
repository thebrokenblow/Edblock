using System.Collections.Generic;
using EdblockModel.SymbolsModel;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles.Interfaces;

namespace EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;

public abstract class SwitchCaseSymbolVM : ScalableBlockSymbolVM
{
    public List<ConnectionPointVM> ConnectionPointsSwitchCaseVM { get; init; }
    protected readonly int _countLines;
    public SwitchCaseSymbolVM(
        IBuilderScaleRectangles builderScaleRectangles,
        ICanvasSymbolsComponentVM canvasSymbolsComponentVM,
        IListCanvasSymbolsComponentVM listCanvasSymbolsComponentVM,
        ITopSettingsMenuComponentVM topSettingsMenuComponentVM,
        IPopupBoxMenuComponentVM popupBoxMenuComponentVM,
        int countLine) : base(
            builderScaleRectangles, 
            canvasSymbolsComponentVM, 
            listCanvasSymbolsComponentVM, 
            topSettingsMenuComponentVM,
            popupBoxMenuComponentVM)
    {
        _countLines = countLine;
        
        ConnectionPointsSwitchCaseVM = new(countLine);

        var switchCaseSymbolModel = (SwitchCaseSymbolModel)BlockSymbolModel;
        switchCaseSymbolModel.CountLine = _countLines;
    }

    public override BlockSymbolModel CreateBlockSymbolModel()
    {
        var nameSymbol = GetType().Name.ToString();

        var switchCaseSymbolModel = new SwitchCaseSymbolModel()
        {
            Id = Id,
            NameSymbol = nameSymbol
        };

        return switchCaseSymbolModel;
    }
}
