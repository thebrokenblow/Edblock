namespace EdblockModel.Symbols.LineSymbols.CompletedLine;

internal interface ILineCoordinateDecorator
{
    public int LineCoordinateX { get; set; }
    public int LineCoordinateY { get; set; }
    public int FinalCoordinateX { get; set; }
    public int FinalCoordinateY { get; set; }
}