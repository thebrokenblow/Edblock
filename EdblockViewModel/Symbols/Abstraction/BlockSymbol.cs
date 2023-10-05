﻿using Prism.Commands;
using System.Windows.Input;
using EdblockModel.Symbols;
using System.Collections.Generic;
using EdblockModel.Symbols.Abstraction;
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
    protected const int defaultWidth = 140;
    protected const int defaultHeigth = 60;
    public BlockSymbol(string nameBlockSymbol, CanvasSymbolsVM canvasSymbolsVM)
    {
        _canvasSymbolsVM = canvasSymbolsVM;

        TextField = new(canvasSymbolsVM);

        BlockSymbolModel = FactoryBlockSymbolModel.Create(nameBlockSymbol);
        BlockSymbolModel.Width = defaultWidth;
        BlockSymbolModel.Height = defaultHeigth;

        Width = defaultWidth;
        Height = defaultHeigth;

        var factoryConnectionPoints = new FactoryConnectionPoints(_canvasSymbolsVM, this);
        ConnectionPoints = factoryConnectionPoints.Create();

        var factoryScaleRectangles = new FactoryScaleRectangles(_canvasSymbolsVM, this);
        ScaleRectangles = factoryScaleRectangles.Create();

        EnterCursor = new(ShowStroke);
        LeaveCursor = new(HideStroke);
    }

    public void ShowStroke()
    {
        if (_canvasSymbolsVM.DraggableSymbol == null && _canvasSymbolsVM.ScaleData == null)
        {
            ColorConnectionPoint.Show(ConnectionPoints);
            ColorScaleRectangle.Show(ScaleRectangles);
            TextField.Cursor = Cursors.SizeAll;
        }
    }

    public void HideStroke()
    {
        ColorConnectionPoint.Hide(ConnectionPoints);
        ColorScaleRectangle.Hide(ScaleRectangles);
    }

    public abstract void SetWidth(int width);
    public abstract void SetHeight(int height);

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