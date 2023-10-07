using System.Windows.Media;
using System.Collections.Generic;
using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.Symbols.ConnectionPoints;

internal class ColorConnectionPoint
{
    private static readonly Brush? FocusFill;
    public static readonly Brush? NotFocusFill;
    private static readonly Brush? FocusStroke;
    public static readonly Brush? NotFocusStroke;
    private static readonly BrushConverter brushConverter = new();
    static ColorConnectionPoint()
    {
        SetBrushColor(ref FocusFill, ColorConnectionPointModel.HexFocusFill);
        SetBrushColor(ref NotFocusFill, ColorConnectionPointModel.HexNotFocusFill);
        SetBrushColor(ref FocusStroke, ColorConnectionPointModel.HexFocusStroke);
        SetBrushColor(ref NotFocusStroke, ColorConnectionPointModel.HexNotFocusStroke);
    }

    private static void SetBrushColor(ref Brush? brush, string hexColor)
    {
        var convertFrom = brushConverter.ConvertFrom(hexColor);
        if (convertFrom != null)
        {
            brush = (Brush)convertFrom;
        }
    }

    public static void Show(List<ConnectionPoint> connectionPoints)
    {
        ChangeColor(connectionPoints, FocusFill);
    }

    public static void Hide(List<ConnectionPoint> connectionPoints)
    {
        ChangeColor(connectionPoints, NotFocusFill);
    }

    public static void ShowStroke(ConnectionPoint connectionPoint)
    {
        connectionPoint.Stroke = FocusStroke;
    }

    public static void HideStroke(ConnectionPoint connectionPoint)
    {
        connectionPoint.Stroke = NotFocusStroke;
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