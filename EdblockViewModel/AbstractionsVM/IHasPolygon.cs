using System.Windows.Media;

namespace EdblockViewModel.AbstractionsVM;

public interface IHasPolygon
{
    public PointCollection? Points { get; set; }
    public void SetCoordinatePolygonPoints();
}