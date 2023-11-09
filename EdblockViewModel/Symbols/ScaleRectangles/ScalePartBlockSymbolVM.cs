using System;
using System.Windows.Input;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.ScaleRectangles;

public record ScalePartBlockSymbolVM(
    Cursor CursorWhenScaling,
    BlockSymbolVM ScalingBlockSymbol, 
    Func<ScalePartBlockSymbolVM, CanvasSymbolsVM, int>? SetWidthBlockSymbol, 
    Func<ScalePartBlockSymbolVM, CanvasSymbolsVM, int>? SetHeigthBlockSymbol, 
    int InitialWidthBlockSymbol,
    int InitialHeigthBlockSymbol,
    int InitialXCoordinateBlockSymbol,
    int InitialYCoordinateBlockSymbol);