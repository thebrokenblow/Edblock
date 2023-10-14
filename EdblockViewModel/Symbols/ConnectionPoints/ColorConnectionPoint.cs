using System.Collections.Generic;

namespace EdblockViewModel.Symbols.ConnectionPoints;

internal class ColorConnectionPoint
{
    internal static void SetFill(string? hexFill, List<ConnectionPoint> connectionPoints)
    {
        foreach (var connectionPoint in connectionPoints)
        {
            connectionPoint.Fill = hexFill;
        }
    }
}