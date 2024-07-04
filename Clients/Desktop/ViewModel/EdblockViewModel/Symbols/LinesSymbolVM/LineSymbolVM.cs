using EdblockViewModel.Core;
using System.Windows;
using System.Windows.Media;

namespace EdblockViewModel.Symbols.LinesSymbolVM;

public class LineSymbolVM(DrawnLineSymbolVM drawnLineSymbolVM) : ObservableObject
{
    private double x1;
    public double X1 
    {
        get => x1;
        set
        {
            x1 = value;
            OnPropertyChanged();
        }
    }

    private double y1;
    public double Y1 
    {
        get => y1;
        set
        {
            y1 = value;
            OnPropertyChanged();
        }
    }

    private double x2;
    public double X2 
    {
        get => x2;
        set
        {
            x2 = value;
            OnPropertyChanged();
        }
    }

    private double y2;
    public double Y2 
    {
        get => y2;
        set
        {
            y2 = value;
            OnPropertyChanged();
        }
    }

    private Brush stroke = Brushes.Black;
    public Brush Stroke
    {
        get => stroke;
        set
        {
            stroke = value;
            OnPropertyChanged();
        }
    }

    public DrawnLineSymbolVM DrawnLineSymbolVM { get; } = drawnLineSymbolVM;

    public void FinishDrawingLine()
    {
        if (DrawnLineSymbolVM.CanvasSymbolsComponentVM.CurrentDrawnLineSymbolVM is null)
        {
            return;
        }

        var currentDrawnLineSymbolVM = DrawnLineSymbolVM.CanvasSymbolsComponentVM.CurrentDrawnLineSymbolVM;
        if (currentDrawnLineSymbolVM.LinesSymbolVM.Contains(this))
        {
            MessageBox.Show("Линия не должна входить в саму себя");
            return;
        }

        var lastLine = currentDrawnLineSymbolVM.LinesSymbolVM[^1];

        if (lastLine.LineIsVertical() && LineIsHorizontal())
        {
            lastLine.Y2 = y2;
        }

        if (lastLine.LineIsHorizontal() && LineIsVertical())
        {
            lastLine.X2 = x2;
        }

        if (lastLine.LineIsVertical() && LineIsVertical() && currentDrawnLineSymbolVM.LinesSymbolVM.Count > 1)
        {
            currentDrawnLineSymbolVM.LinesSymbolVM.Remove(lastLine);
            lastLine = currentDrawnLineSymbolVM.LinesSymbolVM[^1];
            lastLine.X2 = x2;
        }

        if (lastLine.LineIsHorizontal() && LineIsHorizontal() && currentDrawnLineSymbolVM.LinesSymbolVM.Count > 1)
        {
            currentDrawnLineSymbolVM.LinesSymbolVM.Remove(lastLine);
            lastLine = currentDrawnLineSymbolVM.LinesSymbolVM[^1];
            lastLine.Y2 = y2;
        }

        drawnLineSymbolVM.CanvasSymbolsComponentVM.CurrentDrawnLineSymbolVM = null;
    }

    public bool LineIsVertical() =>
        X1 == X2;

    public bool LineIsHorizontal() =>
        Y1 == Y2;
}