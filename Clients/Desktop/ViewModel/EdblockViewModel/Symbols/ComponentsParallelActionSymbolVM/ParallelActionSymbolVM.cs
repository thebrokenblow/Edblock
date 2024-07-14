using System;
using System.Collections.Generic;
using EdblockModel.SymbolsModel;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.Attributes;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.Interfaces;
using EdblockViewModel.Components.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Symbols.ComponentsParallelActionSymbolVM;

[SymbolType("ParallelActionSymbolVM")]
public sealed class ParallelActionSymbolVM : BlockSymbolVM, IHasConnectionPoint
{
    public LineParallelActionSymbolVM UpperHorizontalLine { get; set; } = new();
    public LineParallelActionSymbolVM LowerHorizontalLine { get; set; } = new();
    public List<ConnectionPointVM> ConnectionPointsVM { get; set; }

    private readonly int _maxSymbols;
    private readonly int _sumCountSymbols;
    private readonly int _countSymbolsIncoming;
    private readonly int _countSymbolsOutgoing;

    private const int lineOffset = 20;
    private const int defaultWidth = 140;
    private const int defaultHeigth = 60;
    private const int indentBetweenSymbol = 20;

    private const string defaultColor = "Black";
    public ParallelActionSymbolVM(
        ICanvasSymbolsComponentVM canvasSymbolsComponentVM,
        IListCanvasSymbolsComponentVM listCanvasSymbolsComponentVM,
        ITopSettingsMenuComponentVM topSettingsMenuComponentVM,
        IPopupBoxMenuComponentVM popupBoxMenuComponentVM, 
        int countSymbolsIncoming, 
        int countSymbolsOutgoing) : base(
            canvasSymbolsComponentVM, 
            listCanvasSymbolsComponentVM, 
            topSettingsMenuComponentVM, 
            popupBoxMenuComponentVM)
    {
        _countSymbolsIncoming = countSymbolsIncoming;
        _countSymbolsOutgoing = countSymbolsOutgoing;

        _maxSymbols = Math.Max(countSymbolsIncoming, countSymbolsOutgoing);
        _sumCountSymbols = countSymbolsIncoming + countSymbolsOutgoing;
        Color = defaultColor;

        var parallelActionSymbolModel = (ParallelActionSymbolModel)BlockSymbolModel;
        parallelActionSymbolModel.CountSymbolsIncoming = _countSymbolsIncoming;
        parallelActionSymbolModel.CountSymbolsOutgoing = _countSymbolsOutgoing;

        ConnectionPointsVM = new(countSymbolsIncoming + countSymbolsOutgoing);

        //FactoryConnectionPoint factoryConnectionPoint = null; new FactoryConnectionPoint(
        //    canvasSymbolsComponentVM, 
        //    lineStateStandardComponentVM, 
        //    this);

        //for (int i = 0; i < countSymbolsIncoming; i++)
        //{
        //    var bottomConnectionPoint = factoryConnectionPoint.Create(SideSymbol.Top);
        //    ConnectionPointsVM.Add(bottomConnectionPoint);
        //}

        //for (int i = 0; i < countSymbolsOutgoing; i++)
        //{
        //    var bottomConnectionPoint = factoryConnectionPoint.Create(SideSymbol.Bottom);
        //    ConnectionPointsVM.Add(bottomConnectionPoint);
        //}

        SetWidth(defaultWidth);
        SetHeight(defaultHeigth);
    }

    public override BlockSymbolModel CreateBlockSymbolModel()
    {
        var nameSymbol = GetType().Name.ToString();

        var parallelActionSymbolModel = new ParallelActionSymbolModel()
        {
            Id = Id,
            NameSymbol = nameSymbol,
        };

        return parallelActionSymbolModel;
    }

    public void SetHeight(double height)
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

    private void SetYCoordinateCP(int begin, int end, double yCoordinate)
    {
        for (int i = begin; i < end; i++)
        {
            ConnectionPointsVM[i].YCoordinate = yCoordinate;
            ConnectionPointsVM[i].YCoordinateLineDraw = yCoordinate;
        }
    }

    public void SetWidth(double width)
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
            ConnectionPointsVM[i].XCoordinate = widthSymbols * (2 * numberConnectionPoint + 1) + indentBetweenSymbol * numberConnectionPoint;
            ConnectionPointsVM[i].XCoordinateLineDraw = widthSymbols * (2 * numberConnectionPoint + 1) + indentBetweenSymbol * numberConnectionPoint;

            numberConnectionPoint++;
        }
    }

    //Установка x координат для Точек соединения для линии с минимальным кол-во точек соединений
    private void SetXCoordinateCPNotLeadingLine(int begin, int end, double widthSymbols)
    {
        var halfWidth = Width / 2;

        if (end - begin == 1)
        {
            ConnectionPointsVM[begin].XCoordinate = halfWidth;
            ConnectionPointsVM[begin].XCoordinateLineDraw = halfWidth;
        }
        else
        {
            var length = (Width - widthSymbols) / (end - begin - 1);

            ConnectionPointsVM[begin].XCoordinate = widthSymbols / 2;
            ConnectionPointsVM[begin].XCoordinateLineDraw = widthSymbols / 2;

            for (int i = begin + 1; i < end; i++)
            {
                ConnectionPointsVM[i].XCoordinate = ConnectionPointsVM[i - 1].XCoordinate + length;
                ConnectionPointsVM[i].XCoordinateLineDraw = ConnectionPointsVM[i - 1].XCoordinateLineDraw + length;
            }
        }
    }
}