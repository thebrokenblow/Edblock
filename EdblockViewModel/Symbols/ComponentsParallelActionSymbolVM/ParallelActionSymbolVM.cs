using System;
using System.Collections.Generic;
using EdblockModel.EnumsModel;
using EdblockModel.SymbolsModel;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.AttributeVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

namespace EdblockViewModel.Symbols.ComponentsParallelActionSymbolVM;

[SymbolType("ParallelActionSymbolVM")]
public class ParallelActionSymbolVM : BlockSymbolVM, IHasConnectionPoint
{
    public LineParallelActionSymbolVM UpperHorizontalLine { get; set; } = new();
    public LineParallelActionSymbolVM LowerHorizontalLine { get; set; } = new();
    public List<ConnectionPointVM> ConnectionPoints { get; init; }

    private readonly int _maxSymbols;
    private readonly int _sumCountSymbols;
    private readonly int _countSymbolsIncoming;
    private readonly int _countSymbolsOutgoing;

    private const int lineOffset = 20;
    private const int defaultWidth = 140;
    private const int defaultHeigth = 60;
    private const int indentBetweenSymbol = 20;

    public ParallelActionSymbolVM(EdblockVM edblockVM, int countSymbolsIncoming, int countSymbolsOutgoing) : base(edblockVM)
    {
        _countSymbolsIncoming = countSymbolsIncoming;
        _countSymbolsOutgoing = countSymbolsOutgoing;

        _maxSymbols = Math.Max(countSymbolsIncoming, countSymbolsOutgoing);
        _sumCountSymbols = countSymbolsIncoming + countSymbolsOutgoing;

        BlockSymbolModel = CreateBlockSymbolModel();
        ConnectionPoints = new(countSymbolsIncoming + countSymbolsOutgoing);

        for (int i = 0; i < countSymbolsIncoming; i++)
        {
            var bottomConnectionPoint = new ConnectionPointVM(CanvasSymbolsVM, this, _checkBoxLineGostVM, SideSymbol.Top);

            ConnectionPoints.Add(bottomConnectionPoint);
        }

        for (int i = 0; i < countSymbolsOutgoing; i++)
        {
            var bottomConnectionPoint = new ConnectionPointVM(CanvasSymbolsVM, this, _checkBoxLineGostVM, SideSymbol.Bottom);

            ConnectionPoints.Add(bottomConnectionPoint);
        }


        SetWidth(defaultWidth);
        SetHeight(defaultHeigth);
    }

    public override BlockSymbolModel CreateBlockSymbolModel()
    {
        var parallelActionSymbolModel = new ParallelActionSymbolModel()
        {
            Id = Id,
            NameSymbol = GetType().Name.ToString(),
            CountSymbolsIncoming = _countSymbolsIncoming,
            CountSymbolsOutgoing = _countSymbolsOutgoing
        };

        return parallelActionSymbolModel;
    }

    public override void SetHeight(double height)
    {
        Height = height;

        UpperHorizontalLine.Y1 = lineOffset;
        UpperHorizontalLine.Y2 = lineOffset;

        var yLowerHorizontalLine = height - indentBetweenSymbol;

        LowerHorizontalLine.Y1 = yLowerHorizontalLine;
        LowerHorizontalLine.Y2 = yLowerHorizontalLine;

        if (_countSymbolsIncoming == _countSymbolsOutgoing)
        {
            SetYCoordinateCP(0, _countSymbolsIncoming, lineOffset);
            SetYCoordinateCP(_countSymbolsIncoming, _sumCountSymbols, yLowerHorizontalLine);
        }
        else if (_countSymbolsIncoming > _countSymbolsOutgoing)
        {
            SetYCoordinateCP(0, _countSymbolsIncoming, lineOffset);
            SetYCoordinateCP(_countSymbolsIncoming, _sumCountSymbols, yLowerHorizontalLine);
        }
        else
        {
            SetYCoordinateCP(0, _countSymbolsIncoming, lineOffset);
            SetYCoordinateCP(_countSymbolsIncoming, _sumCountSymbols, yLowerHorizontalLine);
        }
    }

    private void SetYCoordinateCP(int begin, int end, double Symbols)
    {
        for (int i = begin; i < end; i++)
        {
            ConnectionPoints[i].YCoordinate = Symbols;
            ConnectionPoints[i].YCoordinateLineDraw = Symbols;
        }
    }

    public override void SetWidth(double width)
    {
        Width = width * _maxSymbols + indentBetweenSymbol * (_maxSymbols - 1);

        UpperHorizontalLine.X1 = 0;
        UpperHorizontalLine.X2 = Width;

        LowerHorizontalLine.X1 = 0;
        LowerHorizontalLine.X2 = Width;

        if (_countSymbolsIncoming == _countSymbolsOutgoing)
        {
            SetXCoordinateCPLeadingLine(0, _countSymbolsIncoming, width);
            SetXCoordinateCPLeadingLine(_countSymbolsIncoming, _sumCountSymbols, width);
        }
        else if (_countSymbolsIncoming > _countSymbolsOutgoing)
        {
            SetXCoordinateCPLeadingLine(0, _countSymbolsIncoming, width);
            SetXCoordinateCPNotLeadingLine(_countSymbolsIncoming, _sumCountSymbols, width);
        }
        else
        {
            SetXCoordinateCPNotLeadingLine(0, _countSymbolsIncoming, width);
            SetXCoordinateCPLeadingLine(_countSymbolsIncoming, _sumCountSymbols, width);
        }
    }

    //Установка x координат для Точек соединения для линии с максимальным кол-во точек соединений
    private void SetXCoordinateCPLeadingLine(int begin, int end, double widthSymbols)
    {
        widthSymbols /= 2;
        int numberConnectionPoint = 0;

        for (int i = begin; i < end; i++)
        {
            ConnectionPoints[i].XCoordinate = widthSymbols * (2 * numberConnectionPoint + 1) + indentBetweenSymbol * numberConnectionPoint;
            ConnectionPoints[i].XCoordinateLineDraw = widthSymbols * (2 * numberConnectionPoint + 1) + indentBetweenSymbol * numberConnectionPoint;

            numberConnectionPoint++;
        }
    }

    //Установка x координат для Точек соединения для линии с минимальным кол-во точек соединений
    private void SetXCoordinateCPNotLeadingLine(int begin, int end, double widthSymbols)
    {
        if (end - begin == 1)
        {
            ConnectionPoints[begin].XCoordinate = Width / 2;
            ConnectionPoints[begin].XCoordinateLineDraw = Width / 2;
        }
        else
        {
            var length = (Width - widthSymbols) / (end - begin - 1);

            ConnectionPoints[begin].XCoordinate = widthSymbols / 2;
            ConnectionPoints[begin].XCoordinateLineDraw = widthSymbols / 2;

            for (int i = begin + 1; i < end; i++)
            {
                ConnectionPoints[i].XCoordinate = ConnectionPoints[i - 1].XCoordinate + length;
                ConnectionPoints[i].XCoordinateLineDraw = ConnectionPoints[i - 1].XCoordinateLineDraw + length;
            }
        }
    }
}