﻿using System;
using Prism.Commands;
using System.Windows.Input;
using EdblockModel.Symbols;
using System.Collections.Generic;
using EdblockModel.Symbols.Abstraction;
using EdblockModel.Symbols.ScaleRectangles;
using EdblockModel.Symbols.ConnectionPoints;
using EdblockViewModel.Symbols.ScaleRectangles;
using EdblockViewModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.Symbols.Abstraction;

public abstract class BlockSymbol : Symbol
{
    public List<ConnectionPoint> ConnectionPoints { get; init; }
    public List<ScaleRectangle> ScaleRectangles { get; init; }

    private int width;
    public int Width
    {
        get => width;
        set
        {
            width = value;
            OnPropertyChanged();
        }
    }

    private int heigth;
    public int Height
    {
        get => heigth;
        set
        {
            heigth = value;
            OnPropertyChanged();
        }
    }

    private int xCoordinate; 
    public int XCoordinate
    {
        get => xCoordinate;
        set
        {
            xCoordinate = value;
            OnPropertyChanged();
        }
    }

    private int yCoordinate;
    public int YCoordinate
    {
        get => yCoordinate;
        set
        {
            yCoordinate = value;
            OnPropertyChanged();
        }
    }

    public TextField TextField { get; init; }
    public BlockSymbolModel BlockSymbolModel { get; init; }
    public DelegateCommand EnterCursor { get; set; }
    public DelegateCommand LeaveCursor { get; set; }

    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly FactoryBlockSymbolModel factoryBlockSymbolModel = new();
    private readonly Dictionary<PositionConnectionPoint, Func<(int x, int y)>> borderCoordinateByPositionCP;

    protected const int defaultWidth = 140;
    protected const int defaultHeigth = 60;
    public BlockSymbol(string nameBlockSymbol, CanvasSymbolsVM canvasSymbolsVM)
    {
        _canvasSymbolsVM = canvasSymbolsVM;

        TextField = new(canvasSymbolsVM);

        BlockSymbolModel = factoryBlockSymbolModel.Create(nameBlockSymbol);
        BlockSymbolModel.Width = defaultWidth;
        BlockSymbolModel.Height = defaultHeigth;

        Width = defaultWidth;
        Height = defaultHeigth;
        HexColor = BlockSymbolModel.HexColor;

        borderCoordinateByPositionCP = new()
        {
            { PositionConnectionPoint.Top, GetTopBorderCoordinate },
            { PositionConnectionPoint.Bottom, GetBottomBorderCoordinate },
            { PositionConnectionPoint.Left, GetLeftBorderCoordinate },
            { PositionConnectionPoint.Right, GetRightBorderCoordinate }
        };

        var factoryConnectionPoints = new FactoryConnectionPoints(_canvasSymbolsVM, this);
        ConnectionPoints = factoryConnectionPoints.Create();

        var factoryScaleRectangles = new FactoryScaleRectangles(_canvasSymbolsVM, this);
        ScaleRectangles = factoryScaleRectangles.Create();

        EnterCursor = new(ShowStroke);
        LeaveCursor = new(HideStroke);
    }

    private (int x, int y) GetTopBorderCoordinate()
    {
        return (xCoordinate + width / 2, yCoordinate);
    }

    private (int x, int y) GetBottomBorderCoordinate()
    {
        return (xCoordinate + width / 2, yCoordinate + heigth);
    }

    private (int x, int y) GetLeftBorderCoordinate()
    {
        return (xCoordinate, yCoordinate + heigth / 2);
    }

    private (int x, int y) GetRightBorderCoordinate()
    {
        return (xCoordinate + width, yCoordinate + heigth / 2);
    }

    public abstract void SetWidth(int width);
    public abstract void SetHeight(int height);

    public (int x, int y) GetBorderCoordinate(PositionConnectionPoint positionConnectionPoint)
    {
        return borderCoordinateByPositionCP[positionConnectionPoint].Invoke();
    }

    public void ShowStroke()
    {
        // Условие истино, когда символ не перемещается и не масштабируется (просто навёл курсор)
        if (_canvasSymbolsVM.DraggableSymbol == null && _canvasSymbolsVM.ScaleData == null)
        {
            ConnectionPoint.SetEnterConnectionPoint(ConnectionPoints, true);
            ScaleRectangle.SetColor(ScaleRectangleModel.HexFocusFill, ScaleRectangleModel.HexFocusBorderBrush, ScaleRectangles);
            TextField.Cursor = Cursors.SizeAll;
        }
    }

    public void HideStroke()
    {
        ConnectionPoint.SetEnterConnectionPoint(ConnectionPoints, false);
        ScaleRectangle.SetColor(ScaleRectangleModel.HexNotFocusFill, ScaleRectangleModel.HexNotFocusBorderBrush, ScaleRectangles);
    }

    protected void ChangeCoordinateAuxiliaryElements()
    {
        foreach (var connectionPoint in ConnectionPoints)
        {
            connectionPoint.ChangeCoordination();
        }

        foreach (var scaleRectangle in ScaleRectangles)
        {
            scaleRectangle.ChangeCoordination();
        }
    }
}