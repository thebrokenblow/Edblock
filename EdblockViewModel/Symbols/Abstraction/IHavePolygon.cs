using System.Windows.Media;

namespace EdblockViewModel.Symbols.Abstraction;

public interface IHavePolygon
{
    public PointCollection? Points { get; set; }
    public void SetCoordinatePolygonPoints();
}