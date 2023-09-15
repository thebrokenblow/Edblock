using System.Windows.Media;
using System.Collections.Generic;

namespace EdblockViewModel.ConnectionPointVM;

internal static class ColorConnectionPoint
{
    private static readonly Brush? HoverFill;
    private static readonly Brush? HoverStroke;
    private const string hexHoverFill = "#777777";
    private const string hexHoverStroke = "#00ff00";
    static ColorConnectionPoint()
    {
        var brushConverterConnectionPoint = new BrushConverter();
        var convertFromHoverFill = brushConverterConnectionPoint.ConvertFrom(hexHoverFill);

        if (convertFromHoverFill != null)
        {
            HoverFill = (Brush)convertFromHoverFill;
        }

        var convertFromHoverStroke = brushConverterConnectionPoint.ConvertFrom(hexHoverStroke);
        if (convertFromHoverStroke != null)
        {
            HoverStroke = (Brush)convertFromHoverStroke;
        }
    }

    public static void Show(List<ConnectionPoint> connectionPoints)
    {
        ChangeColor(connectionPoints, HoverFill);
    }

    public static void Hide(List<ConnectionPoint> connectionPoints)
    {
        ChangeColor(connectionPoints, Brushes.Transparent);
    }

    public static void ShowStroke(ConnectionPoint connectionPoint)
    {
        connectionPoint.Stroke = HoverStroke;
    }

    public static void HideStroke(ConnectionPoint connectionPoint)
    {
        connectionPoint.Stroke = Brushes.Transparent;
    }

    private static void ChangeColor(List<ConnectionPoint> connectionPoints, Brush? fill)
    {
        if (fill != null)
        {
            foreach (var connectionPoint in connectionPoints)
            {
                connectionPoint.Fill = fill;
            }
        }
    }
}