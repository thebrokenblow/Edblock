namespace EdblockModel.Symbols.LineSymbols.CompletedLine;

internal class BuildLineCoordinateDecorato
{
    private bool IsSetSwapLineCoordinate = false;

    public BuildLineCoordinateDecorato SetSwapFinalCoordinate()
    {
        IsSetSwapLineCoordinate = true;

        return this;
    }

    public ILineCoordinateDecorator Build(ILineCoordinateDecorator coordinateDecorator)
    {
        if (IsSetSwapLineCoordinate)
        {
            coordinateDecorator = new SwapLineCoordinate(coordinateDecorator);
        }

        return coordinateDecorator;
    }
}