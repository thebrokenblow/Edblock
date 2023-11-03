using System;
using System.Windows.Input;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.ScaleRectangles;

public record ScaleData(
    Cursor Cursor,
    BlockSymbol BlockSymbol, 
    Func<ScaleData, CanvasSymbolsVM, int>? GetWidthSymbol, 
    Func<ScaleData, CanvasSymbolsVM, int>? GetHeigthSymbol, 
    int InitialWidth,
    int InitialHeigth,
    int InitialX,
    int InitialY);