namespace EdblockModel.SymbolsModel.LineSymbolsModel.DecoratorLineSymbols;

public class BuilderCoordinateDecorator
{
    private bool IsSetSwapCoordinate = false;
    private bool IsInversionXCoordinate = false;
    private bool IsInversionYCoordinate = false;

    public BuilderCoordinateDecorator SetSwap()
    {
        IsSetSwapCoordinate = true;

        return this;
    }

    public BuilderCoordinateDecorator SetInversionXCoordinate()
    {
        IsInversionXCoordinate = true;

        return this;
    }

    public BuilderCoordinateDecorator SetInversionYCoordinate()
    {
        IsInversionYCoordinate = true;

        return this;
    }

    public ICoordinateDecorator Build(ICoordinateDecorator coordinateDecorator)
    {
        if (IsInversionXCoordinate)
        {
            coordinateDecorator = new InversionXCoordinateDecorator(coordinateDecorator);
        }

        if (IsInversionYCoordinate)
        {
            coordinateDecorator = new InversionYCoordinateDecorator(coordinateDecorator);
        }

        if (IsSetSwapCoordinate)
        {
            coordinateDecorator = new SwapCoordinateDecorator(coordinateDecorator);
        }

        return coordinateDecorator;
    }
}