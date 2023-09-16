using System.Windows.Media;
using System.Collections.Generic;

namespace EdblockViewModel.Symbols.ScaleRectangles;

internal static class ColorScaleRectangle
{
    private static readonly Brush? hoverBorderBrush;
    private const string hexHoverBorderBrush = "#FF888888";

    static ColorScaleRectangle()
    {
        var brushConverter = new BrushConverter();
        var convertFrom = brushConverter.ConvertFrom(hexHoverBorderBrush);

        if (convertFrom != null)
        {
            hoverBorderBrush = (Brush)convertFrom;
        }
    }

    public static void Show(List<ScaleRectangle> scaleRectangles)
    {
        ChangeColorScaleRectangle(scaleRectangles, Brushes.White, hoverBorderBrush);
    }

    public static void Hide(List<ScaleRectangle> scaleRectangles)
    {
        ChangeColorScaleRectangle(scaleRectangles, Brushes.Transparent, Brushes.Transparent);
    }

    private static void ChangeColorScaleRectangle(List<ScaleRectangle> scaleRectangles, Brush fillScaleRectangles, Brush? borderBrushScaleRectangle)
    {
        if (borderBrushScaleRectangle != null)
        {
            foreach (var scaleRectangle in scaleRectangles)
            {
                ChangeColor(scaleRectangle, fillScaleRectangles, borderBrushScaleRectangle);
            }
        }
    }

    private static void ChangeColor(ScaleRectangle scaleRectangle, Brush fillScaleRectangles, Brush? borderBrushScaleRectangle)
    {
        scaleRectangle.Fill = fillScaleRectangles;
        scaleRectangle.BorderBrush = borderBrushScaleRectangle;
    }
}