using System;
using EdblockViewModel;
using EdblockViewModel.Symbols.Abstraction;

namespace MVVM.ViewModel.SymbolsViewModel;

public record ScaleData(
    BlockSymbol BlockSymbol, 
    Func<ScaleData, CanvasSymbolsVM, int>? GetWidthSymbol, 
    Func<ScaleData, CanvasSymbolsVM, int>? GetHeigthSymbol, 
    int InitialWidth,
    int InitialHeigth,
    int InitialX,
    int InitialY);