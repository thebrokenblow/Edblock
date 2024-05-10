using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Edblock.SymbolsViewModel.Symbols.ComponentsParallelActionSymbol;
using EdblockModel.SymbolsModel.LineSymbolsModel;
using EdblockViewModel.AbstractionsVM;

namespace Edblock.SymbolsViewModel.Symbols.LineSymbols;

public class LineSymbolVM : INotifyPropertyChanged
{
    private double x1;
    public double X1
    {
        get => x1;
        set
        {
            x1 = value;
            _lineSymbolModel.X1 = x1;

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
            _lineSymbolModel.Y1 = y1;

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
            _lineSymbolModel.X2 = x2;

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
            _lineSymbolModel.Y2 = y2;

            OnPropertyChanged();
        }
    }

    private bool isSelected;
    public bool IsSelected
    {
        get => isSelected;
        set
        {
            isSelected = value;
            OnPropertyChanged();
        }
    }

    private readonly LineSymbolModel _lineSymbolModel;
    private readonly DrawnLineSymbolVM _drawnLineSymbolVM;

    public LineSymbolVM(DrawnLineSymbolVM drawnLineSymbolVM, LineSymbolModel lineSymbolModel)
    {
        _drawnLineSymbolVM = drawnLineSymbolVM;
        _lineSymbolModel = lineSymbolModel;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    public void FinishDrawingLine()
    {
        var currentDrawnLineSymbol = _drawnLineSymbolVM.CanvasSymbolsVM.CurrentDrawnLineSymbol;

        if (currentDrawnLineSymbol is null)
        {
            return;
        }

        var symbolOutgoingLine = currentDrawnLineSymbol.SymbolOutgoingLine;

        if (symbolOutgoingLine is ParallelActionSymbolViewModel or null)
        {
            return;
        }

        var lastLineSymbolVM = currentDrawnLineSymbol.LinesSymbolVM[^1];

        if (x1 == x2 && lastLineSymbolVM.y1 == lastLineSymbolVM.y2)
        {
            lastLineSymbolVM.X2 = x2;

            currentDrawnLineSymbol.ArrowSymbol.ChangePosition((x2, lastLineSymbolVM.y2));
        }
        else if (y1 == y2 && lastLineSymbolVM.x1 == lastLineSymbolVM.x2)
        {
            lastLineSymbolVM.Y2 = y2;

            currentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.x2, y2));
        }
        else
        {
            throw new Exception("Неправильное соединение линий");
        }

        if (!_drawnLineSymbolVM.CanvasSymbolsVM.BlockByDrawnLines.ContainsKey(symbolOutgoingLine))
        {
            var drawnLinesSymbolVM = new List<DrawnLineSymbolVM>
            {
                currentDrawnLineSymbol
            };

            _drawnLineSymbolVM.CanvasSymbolsVM.BlockByDrawnLines.Add(symbolOutgoingLine, drawnLinesSymbolVM);
        }
        else
        {
            _drawnLineSymbolVM.CanvasSymbolsVM.BlockByDrawnLines[symbolOutgoingLine].Add(currentDrawnLineSymbol);
        }

        var t = _drawnLineSymbolVM.CanvasSymbolsVM.CurrentDrawnLineSymbol.SymbolOutgoingLine;

        _drawnLineSymbolVM.CanvasSymbolsVM.CurrentDrawnLineSymbol = null;
    }
}