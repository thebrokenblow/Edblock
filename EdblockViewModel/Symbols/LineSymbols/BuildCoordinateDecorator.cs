using System;

namespace EdblockViewModel.Symbols.LineSymbols;

public class BuildCoordinateDecorator
{
    private bool IsSetSwapCoordinate = false;
    private bool IsInversionXCoordinate = false;
    private bool IsInversionYCoordinate = false;

    public BuildCoordinateDecorator SetSwap()
    {
        IsSetSwapCoordinate = true;

        return this;
    }

    public BuildCoordinateDecorator SetInversionXCoordinate()
    {
        IsInversionXCoordinate = true;

        return this;
    }

    public BuildCoordinateDecorator SetInversionYCoordinate()
    {
        IsInversionYCoordinate = true;

        return this;
    }

    public ICoordinateDecorator Create(ICoordinateDecorator coordinate)
    {
        if (IsSetSwapCoordinate && IsInversionXCoordinate && IsInversionYCoordinate)
        {
            ICoordinateDecorator inversionXCoordinateDecorator = new InversionXCoordinateDecorator(coordinate);
            ICoordinateDecorator InversionYCoordinateDecorator = new InversionYCoordinateDecorator(inversionXCoordinateDecorator);
            ICoordinateDecorator swapCoordinateDecorator = new SwapCoordinateDecorator(InversionYCoordinateDecorator);

            return swapCoordinateDecorator;
        }

        if (!IsSetSwapCoordinate && IsInversionXCoordinate && IsInversionYCoordinate)
        {
            ICoordinateDecorator inversionXCoordinateDecorator = new InversionXCoordinateDecorator(coordinate);
            ICoordinateDecorator InversionYCoordinateDecorator = new InversionYCoordinateDecorator(inversionXCoordinateDecorator);

            return InversionYCoordinateDecorator;
        }

        if (!IsSetSwapCoordinate && !IsInversionXCoordinate && IsInversionYCoordinate)
        {
            ICoordinateDecorator InversionYCoordinateDecorator = new InversionYCoordinateDecorator(coordinate);

            return InversionYCoordinateDecorator;
        }

        if (!IsSetSwapCoordinate && !IsInversionXCoordinate && !IsInversionYCoordinate)
        {
            return coordinate;
        }


        if (IsSetSwapCoordinate && !IsInversionXCoordinate && IsInversionYCoordinate)
        {
            ICoordinateDecorator InversionYCoordinateDecorator = new InversionYCoordinateDecorator(coordinate);
            ICoordinateDecorator swapCoordinateDecorator = new SwapCoordinateDecorator(InversionYCoordinateDecorator);

            return swapCoordinateDecorator;
        }

        if (IsSetSwapCoordinate && !IsInversionXCoordinate && !IsInversionYCoordinate)
        {
            ICoordinateDecorator swapCoordinateDecorator = new SwapCoordinateDecorator(coordinate);

            return swapCoordinateDecorator;
        }


        if (!IsSetSwapCoordinate && IsInversionXCoordinate && !IsInversionYCoordinate)
        {
            ICoordinateDecorator InversionXCoordinate = new InversionXCoordinateDecorator(coordinate);

            return InversionXCoordinate;
        }

        if (IsSetSwapCoordinate && IsInversionXCoordinate && !IsInversionYCoordinate)
        {
            ICoordinateDecorator InversionXCoordinateDecorator = new InversionXCoordinateDecorator(coordinate);
            ICoordinateDecorator swapCoordinateDecorator = new SwapCoordinateDecorator(InversionXCoordinateDecorator);

            return swapCoordinateDecorator;
        }

        throw new Exception("");
    }
}