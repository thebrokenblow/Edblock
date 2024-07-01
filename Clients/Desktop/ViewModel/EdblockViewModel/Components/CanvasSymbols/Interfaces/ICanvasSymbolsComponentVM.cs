using Prism.Commands;
using System.Windows;
using System.Windows.Input;
using EdblockViewModel.Symbols.LinesSymbolVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

namespace EdblockViewModel.Components.CanvasSymbols.Interfaces;

public interface ICanvasSymbolsComponentVM
{
    Rect Grid { get; }
    int XCoordinate { get; set; }
    int YCoordinate { get; set; }
    double Width { get; set; }
    double Height { get; set; }
    Cursor Cursor { get; set; }
    DelegateCommand MouseMove { get; }
    DelegateCommand MouseUp { get; }
    DelegateCommand MouseLeftButtonDown { get; }
    DelegateCommand RemoveSelectedSymbolsCommand { get; }
    ScalePartBlockSymbol? ScalePartBlockSymbol { get; set; }
    DrawnLineSymbolVM? CurrentDrawnLineSymbolVM { get; set; }
    ScalingCanvasSymbolsComponentVM ScalingCanvasSymbolsVM { get; }
    IListCanvasSymbolsComponentVM ListCanvasSymbolsComponentVM { get; }

    void ClearSelectedSymbols();
    void RemoveSelectedSymbols();
    void SetDefaultValues();
    void RedrawSymbols();
}