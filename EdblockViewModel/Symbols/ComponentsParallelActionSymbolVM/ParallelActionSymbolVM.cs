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
    private const int defaultWidth = 140;
    private const int defaultHeigth = 20;
    private const int indentBetweenSymbol = 20;

    public ParallelActionSymbolVM(EdblockVM edblockVM, int countSymbolsIncoming, int countSymbolsOutgoing) : base(edblockVM)
    {
        _countSymbolsIncoming = countSymbolsIncoming;
        _countSymbolsOutgoing = countSymbolsOutgoing;

        _maxSymbols = Math.Max(countSymbolsIncoming, countSymbolsOutgoing);

        ConnectionPoints = new(countSymbolsIncoming);
        ConnectionPoints2 = new(countSymbolsOutgoing);

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
            SetXCoordinateCPLeadingLine(ConnectionPoints, width);
            SetXCoordinateCPLeadingLine(ConnectionPoints2, width);
        }
        else if (_countSymbolsIncoming > _countSymbolsOutgoing)
        {
            SetXCoordinateCPLeadingLine(ConnectionPoints, width);
            SetXCoordinateCPNotLeadingLine(ConnectionPoints2, width);
        }
        else
        {
            SetXCoordinateCPLeadingLine(ConnectionPoints2, width);
            SetXCoordinateCPNotLeadingLine(ConnectionPoints, width);
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
            for (int i = 0; i < ConnectionPoints.Count; i++)
            {
                ConnectionPoints[i].YCoordinate = -4;
                ConnectionPoints[i].YCoordinateLineDraw = 0;
            }

            for (int i = 0; i < ConnectionPoints2.Count; i++)
            {
                ConnectionPoints2[i].YCoordinate = height - 4;
                ConnectionPoints2[i].YCoordinateLineDraw = height;
            }
        }
        else if (_countSymbolsIncoming > _countSymbolsOutgoing)
        {
            for (int i = 0; i < ConnectionPoints.Count; i++)
            {
                ConnectionPoints[i].YCoordinate = -4;
                ConnectionPoints[i].YCoordinateLineDraw = 0;
            }

            for (int i = 0; i < ConnectionPoints2.Count; i++)
            {
                ConnectionPoints2[i].YCoordinate = height - 4;
                ConnectionPoints2[i].YCoordinateLineDraw = height;
            }
        }
        else
        {
            for (int i = 0; i < ConnectionPoints.Count; i++)
            {
                ConnectionPoints[i].YCoordinate = -4;
                ConnectionPoints[i].YCoordinateLineDraw = 0;
            }

            for (int i = 0; i < ConnectionPoints2.Count; i++)
            {
                ConnectionPoints2[i].YCoordinate = height - 4;
                ConnectionPoints2[i].YCoordinateLineDraw = height;
            }
        }
    }

    //Установка x координат для Точек соединения для линии с максимальным кол-во точек соединений
    private void SetXCoordinateCPLeadingLine(List<ConnectionPointVM> connectionPoints, double width)
    {
        width /= 2;
        int numberConnectionPoint = 0;

        for (int i = 0; i < connectionPoints.Count; i++)
        {
            connectionPoints[i].XCoordinate = width * (numberConnectionPoint + 1) + indentBetweenSymbol * numberConnectionPoint + width * numberConnectionPoint - 4;
            connectionPoints[i].XCoordinateLineDraw = width * (numberConnectionPoint + 1) + indentBetweenSymbol * numberConnectionPoint + width * numberConnectionPoint;

            numberConnectionPoint++;
        }
    }

    //Установка x координат для Точек соединения для линии с минимальным кол-во точек соединений
    private void SetXCoordinateCPNotLeadingLine(List<ConnectionPointVM> connectionPoints, double width)
    {
        if (connectionPoints.Count == 1)
        {
            connectionPoints[0].XCoordinate = Width / 2 - 4;
            connectionPoints[0].XCoordinateLineDraw = Width / 2;
        }
        else
        {
            var numberConnectionPoint = 0;
            var length = (Width - width) / (connectionPoints.Count - 1);

            for (int i = 0; i < connectionPoints.Count; i++)
            {
                if (numberConnectionPoint == 0)
                {
                    connectionPoints[i].XCoordinate = width / 2 - 4;
                    connectionPoints[i].XCoordinateLineDraw = width / 2;
                }
                else
                {
                    connectionPoints[i].XCoordinate = connectionPoints[i - 1].XCoordinate + length;
                    connectionPoints[i].XCoordinateLineDraw = connectionPoints[i - 1].XCoordinateLineDraw + length;
                }

                numberConnectionPoint++;
            }
        }
    }
}