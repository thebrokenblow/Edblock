using System.Windows.Media;

namespace Edblock.SymbolsViewModel.Core;

public interface IHasPolygon
{
    PointCollection Points { get; set; }
    void SetCoordinatePolygonPoints();
}