using System;
using System.Collections.Generic;
using EdblockModel.Enum;
using EdblockModel.SymbolsModel;
using EdblockModel.AbstractionsModel;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.Symbols.ComponentsParallelActionSymbolVM;

public class ParallelActionSymbolVM : BlockSymbolVM, IHasConnectionPoint
{
    public LineParallelActionSymbolVM UpperHorizontalLine { get; set; } = new();
    public LineParallelActionSymbolVM LowerHorizontalLine { get; set; } = new();
    public List<ConnectionPointVM> ConnectionPoints { get; init; }
    public List<ConnectionPointVM> ConnectionPoints2 { get; init; } 


    private readonly int _maxSymbols;
    private readonly int _countSymbolsIncoming;
    private readonly int _countSymbolsOutgoing;
    private readonly int _sumConnectionPoints;
    private const int defaultWidth = 140;
    private const int defaultHeigth = 20;
    private const int indentBetweenSymbol = 20;

    public ParallelActionSymbolVM(EdblockVM edblockVM, int countSymbolsIncoming, int countSymbolsOutgoing) : base(edblockVM)
    {
        _countSymbolsIncoming = countSymbolsIncoming;
        _countSymbolsOutgoing = countSymbolsOutgoing;

        _maxSymbols = Math.Max(countSymbolsIncoming, countSymbolsOutgoing);

        _sumConnectionPoints = countSymbolsIncoming + countSymbolsOutgoing;
        ConnectionPoints = new(_sumConnectionPoints);

        for (int i = 0; i < countSymbolsIncoming; i++)
        {
            var bottomConnectionPoint = new ConnectionPointVM(
                CanvasSymbolsVM,
                this,
                _checkBoxLineGostVM,
                SideSymbol.Top);

            ConnectionPoints.Add(bottomConnectionPoint);
        }

        for (int i = 0; i < countSymbolsOutgoing; i++)
        {
            var bottomConnectionPoint = new ConnectionPointVM(
                CanvasSymbolsVM,
                this,
                _checkBoxLineGostVM,
                SideSymbol.Bottom);

            ConnectionPoints2.Add(bottomConnectionPoint);
        }

        BlockSymbolModel = CreateBlockSymbolModel();

        SetWidth(defaultWidth);
        SetHeight(defaultHeigth);
    }

    public override BlockSymbolModel CreateBlockSymbolModel()
    {
        var parallelActionSymbolModel = new ParallelActionSymbolModel()
        {
            CountSymbolsIncoming = _countSymbolsIncoming,
            CountSymbolsOutgoing = _countSymbolsOutgoing
        };

        return parallelActionSymbolModel;
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
            SetXCoordinateCPLeadingLine(_countSymbolsIncoming, _sumConnectionPoints, width);
        }
        else if (_countSymbolsIncoming > _countSymbolsOutgoing)
        {
            SetXCoordinateCPLeadingLine(0, _countSymbolsIncoming, width);
            SetXCoordinateCPNotLeadingLine(_countSymbolsIncoming, _sumConnectionPoints, _countSymbolsOutgoing, width);
        }
        else
        {
            SetXCoordinateCPNotLeadingLine(_countSymbolsIncoming, _sumConnectionPoints, _countSymbolsOutgoing, width);
            SetXCoordinateCPLeadingLine(0, _countSymbolsOutgoing, width);
        }
    }

    public override void SetHeight(double height)
    {
        Height = height;

        UpperHorizontalLine.Y1 = 0;
        UpperHorizontalLine.Y2 = 0;

        LowerHorizontalLine.Y1 = height;
        LowerHorizontalLine.Y2 = height;

        if (_countSymbolsIncoming == _countSymbolsOutgoing)
        {
            for (int i = 0; i < _countSymbolsIncoming; i++)
            {
                ConnectionPoints[i].YCoordinate = -4;
                ConnectionPoints[i].YCoordinateLineDraw = 0;
            }

            for (int i = _countSymbolsIncoming; i < _sumConnectionPoints; i++)
            {
                ConnectionPoints[i].YCoordinate = height - 4;
                ConnectionPoints[i].YCoordinateLineDraw = height;
            }
        }
        else if (_countSymbolsIncoming > _countSymbolsOutgoing)
        {
            for (int i = 0; i < _countSymbolsOutgoing; i++)
            {
                ConnectionPoints[i].YCoordinate = -4;
                ConnectionPoints[i].YCoordinateLineDraw = 0;
            }

            for (int i = _countSymbolsOutgoing; i < _countSymbolsIncoming; i++)
            {
                ConnectionPoints[i].YCoordinate = height - 4;
                ConnectionPoints[i].YCoordinateLineDraw = height;
            }
        }
        else
        {
            for (int i = 0; i < _countSymbolsIncoming; i++)
            {
                ConnectionPoints[i].YCoordinate = -4;
                ConnectionPoints[i].YCoordinateLineDraw = 0;
            }

            for (int i = _countSymbolsIncoming; i < _countSymbolsOutgoing; i++)
            {
                ConnectionPoints[i].YCoordinate = height - 4;
                ConnectionPoints[i].YCoordinateLineDraw = height;
            }
        }
    }

    //Установка x координат для Точек соединения для линии с максимальным кол-во точек соединений
    private void SetXCoordinateCPLeadingLine(int begin, int end, double width)
    {
        width /= 2;
        int numberConnectionPoint = 0;

        for (int i = begin; i < end; i++)
        {
            ConnectionPoints[i].XCoordinate = width * (numberConnectionPoint + 1) + indentBetweenSymbol * numberConnectionPoint + width * numberConnectionPoint - 4;
            ConnectionPoints[i].XCoordinateLineDraw = width * (numberConnectionPoint + 1) + indentBetweenSymbol * numberConnectionPoint + width * numberConnectionPoint;

            numberConnectionPoint++;
        }
    }

    //Установка x координат для Точек соединения для линии с минимальным кол-во точек соединений
    private void SetXCoordinateCPNotLeadingLine(int begin, int end, int countSymbols, double width)
    {
        var length = (Width - width) / (countSymbols - 1);

        int numberConnectionPoint = 0;

        if (countSymbols == 1)
        {
            ConnectionPoints[begin].XCoordinate = Width / 2 - 4;
            ConnectionPoints[begin].XCoordinateLineDraw = Width / 2;
        }
        else
        {
            for (int i = begin; i < end; i++)
            {
                if (numberConnectionPoint == 0)
                {
                    ConnectionPoints[i].XCoordinate = width / 2 - 4;
                    ConnectionPoints[i].XCoordinateLineDraw = width / 2;
                }
                else
                {
                    ConnectionPoints[i].XCoordinate = ConnectionPoints[i - 1].XCoordinate + length;
                    ConnectionPoints[i].XCoordinateLineDraw = ConnectionPoints[i - 1].XCoordinateLineDraw + length;
                }

                numberConnectionPoint++;
            }
        }
    }
}