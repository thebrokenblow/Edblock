using EdblockModel.EnumsModel;
using Edblock.SymbolsViewModel.Core;
using Edblock.PagesViewModel.Components;
using Edblock.PagesViewModel.Components.PopupBox;

namespace Edblock.SymbolsViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

public class BuilderConnectionPointsVM(CanvasSymbolsViewModel canvasSymbolsViewModel, BlockSymbolViewModel blockSymbolViewModel, LineGostViewModel lineGostViewModel)
{
    private readonly FactoryConnectionPoint factoryConnectionPoint = new(canvasSymbolsViewModel, blockSymbolViewModel, lineGostViewModel);
    private readonly List<ConnectionPointVM> connectionPointsViewModel = [];

    public BuilderConnectionPointsVM AddTopConnectionPoint()
    {
        var topConnectionPointViewModel = factoryConnectionPoint.Create(SideSymbol.Top);
        connectionPointsViewModel.Add(topConnectionPointViewModel);

        return this;
    }

    public BuilderConnectionPointsVM AddRightConnectionPoint()
    {
        var rightConnectionPointViewModel = factoryConnectionPoint.Create(SideSymbol.Right);
        connectionPointsViewModel.Add(rightConnectionPointViewModel);

        return this;
    }

    public BuilderConnectionPointsVM AddBottomConnectionPoint()
    {
        var bottomConnectionPointViewModel = factoryConnectionPoint.Create(SideSymbol.Bottom);
        connectionPointsViewModel.Add(bottomConnectionPointViewModel);

        return this;
    }

    public BuilderConnectionPointsVM AddLeftConnectionPoint()
    {
        var leftConnectionPointViewModel = factoryConnectionPoint.Create(SideSymbol.Left);
        connectionPointsViewModel.Add(leftConnectionPointViewModel);

        return this;
    }

    public List<ConnectionPointVM> Build()
    {
        return connectionPointsViewModel;
    }
}