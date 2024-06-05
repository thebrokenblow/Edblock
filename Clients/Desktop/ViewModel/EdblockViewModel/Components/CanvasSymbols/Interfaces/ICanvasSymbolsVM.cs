using Prism.Commands;
using System.Windows;
using System.Windows.Input;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

namespace EdblockViewModel.Components.CanvasSymbols.Interfaces;

public interface ICanvasSymbolsVM
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
    ScalingCanvasSymbolsVM ScalingCanvasSymbolsVM { get; }
    IListCanvasSymbolsVM ListCanvasSymbolsVM { get; }

    void RemoveSelectedSymbols();
    void SetDefaultValues();
    void RedrawSymbols();
}