namespace EdblockModel.Symbols.LineSymbols.CompletedLine;

internal class SwapLineCoordinate : ILineCoordinateDecorator
{
    public int LineCoordinateX
    {
        get => _lineCoordinateDecorator.FinalCoordinateX;
        set => _lineCoordinateDecorator.FinalCoordinateX = value;
    }

    public int LineCoordinateY
    {
        get => _lineCoordinateDecorator.FinalCoordinateY;
        set => _lineCoordinateDecorator.FinalCoordinateY = value;
    }

    public int FinalCoordinateX
    {
        get => _lineCoordinateDecorator.LineCoordinateX;
        set => _lineCoordinateDecorator.LineCoordinateX = value;
    }

    public int FinalCoordinateY
    {
        get => _lineCoordinateDecorator.LineCoordinateY;
        set => _lineCoordinateDecorator.LineCoordinateY = value;
    }

    private readonly ILineCoordinateDecorator _lineCoordinateDecorator;
    public SwapLineCoordinate(ILineCoordinateDecorator lineCoordinateDecorator)
    {
        _lineCoordinateDecorator = lineCoordinateDecorator;
    }
}
