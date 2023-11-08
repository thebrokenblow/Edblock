using EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols.Decorators;

namespace EdblockViewModel.Symbols.LineSymbols.DecoratorLineSymbols;

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

    public ICoordinateDecorator Create(ICoordinateDecorator coordinate)
    {
        if (IsInversionXCoordinate)
        {
            coordinate = new InversionXCoordinateDecorator(coordinate);
        }
        
        if (IsInversionYCoordinate)
        {
            coordinate = new InversionYCoordinateDecorator(coordinate);
        }
        
        if (IsSetSwapCoordinate)
        {
            coordinate = new SwapCoordinateDecorator(coordinate);
        }

        return coordinate;
    }
}