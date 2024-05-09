using System.Windows.Media;

namespace Edblock.SymbolsViewModel.Core;

public interface IHasPolygon
{
    public PointCollection? Points { get; set; }
    public void SetCoordinatePolygonPoints();
}