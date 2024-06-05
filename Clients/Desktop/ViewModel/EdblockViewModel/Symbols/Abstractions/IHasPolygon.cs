using System.Windows.Media;

namespace EdblockViewModel.Symbols.Abstractions;

public interface IHasPolygon
{
    public PointCollection? Points { get; set; }
    public void SetCoordinatePolygonPoints();
}