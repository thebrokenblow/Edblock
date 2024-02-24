using System.Collections.Generic;
using EdblockModel.EnumsModel;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.AbstractionsVM;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

public class BuilderConnectionPointsVM
{
    private readonly List<ConnectionPointVM> connectionPointsVM = new();

    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly BlockSymbolVM _blockSymbolVM;
    private readonly CheckBoxLineGostVM _checkBoxLineGostVM;

    public BuilderConnectionPointsVM(CanvasSymbolsVM canvasSymbolsVM, BlockSymbolVM blockSymbolVM, CheckBoxLineGostVM checkBoxLineGostVM)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        _blockSymbolVM = blockSymbolVM;
        _checkBoxLineGostVM = checkBoxLineGostVM;
    }

    public BuilderConnectionPointsVM AddTopConnectionPoint()
    {
        var topConnectionPointVM = CreateConnectionPoint(SideSymbol.Top);

        connectionPointsVM.Add(topConnectionPointVM);

        return this;
    }

    public BuilderConnectionPointsVM AddRightConnectionPoint()
    {
        var rightConnectionPointVM = CreateConnectionPoint(SideSymbol.Right);

        connectionPointsVM.Add(rightConnectionPointVM);

        return this;
    }

    public BuilderConnectionPointsVM AddBottomConnectionPoint()
    {
        var bottomConnectionPointVM = CreateConnectionPoint(SideSymbol.Bottom);

        connectionPointsVM.Add(bottomConnectionPointVM);

        return this;
    }

    public BuilderConnectionPointsVM AddLeftConnectionPoint()
    {
        var leftConnectionPointVM = CreateConnectionPoint(SideSymbol.Left);

        connectionPointsVM.Add(leftConnectionPointVM);

        return this;
    }

    public List<ConnectionPointVM> Create() 
    {
        return connectionPointsVM;
    }


    private ConnectionPointVM CreateConnectionPoint(SideSymbol sideSymbol)
    {
        var connectionPointVM = new ConnectionPointVM(
                _canvasSymbolsVM,
                _blockSymbolVM,
                _checkBoxLineGostVM,
                sideSymbol);

        return connectionPointVM;
    }
}
