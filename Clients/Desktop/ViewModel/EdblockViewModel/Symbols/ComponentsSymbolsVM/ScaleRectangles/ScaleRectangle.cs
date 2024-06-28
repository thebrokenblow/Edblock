﻿using System;
using Prism.Commands;
using System.Windows.Input;
using System.Collections.Generic;
using EdblockViewModel.Core;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;
using EdblockViewModel.Symbols.Abstractions;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

public class ScaleRectangle : ObservableObject
{
    private double xCoordinate;
    public double XCoordinate
    {
        get => xCoordinate;
        set
        {
            xCoordinate = value;
            OnPropertyChanged();
        }
    }

    private double yCoordinate;
    public double YCoordinate
    {
        get => yCoordinate;
        set
        {
            yCoordinate = value;
            OnPropertyChanged();
        }
    }

    private bool isShow = false;
    public bool IsShow
    {
        get => isShow;
        set
        {
            isShow = value;
            OnPropertyChanged();
        }
    }

    public DelegateCommand EnterCursor { get; }
    public DelegateCommand LeaveCursor { get; }
    public DelegateCommand ClickScaleRectangle { get; }

    private readonly ICanvasSymbolsComponentVM _canvasSymbolsVM;
    private readonly IScaleAllSymbolComponentVM _scaleAllSymbolVM;
    private readonly BlockSymbolVM _blockSymbolVM;
    private readonly Cursor _cursorScaling;
    private readonly Func<(double, double)> _getCoordinateScaleRectangle;
    private readonly PositionScaleRectangle _positionScaleRectangle;
    public ScaleRectangle(
        ICanvasSymbolsComponentVM canvasSymbolsVM,
        IScaleAllSymbolComponentVM scaleAllSymbolVM,
        BlockSymbolVM blockSymbolVM,
        Cursor cursorScaling,
        Func<(double, double)> getCoordinateScaleRectangle,
        PositionScaleRectangle positionScaleRectangle)
    {
        _blockSymbolVM = blockSymbolVM;
        _cursorScaling = cursorScaling;
        _canvasSymbolsVM = canvasSymbolsVM;
        _scaleAllSymbolVM = scaleAllSymbolVM;
        _positionScaleRectangle = positionScaleRectangle;

        _getCoordinateScaleRectangle = getCoordinateScaleRectangle;
        (XCoordinate, YCoordinate) = getCoordinateScaleRectangle.Invoke();

        EnterCursor = new(ShowScaleRectangles);
        LeaveCursor = new(HideScaleRectangles);
        ClickScaleRectangle = new(SaveScaleRectangle);
    }

    public void ChangeCoordination()
    {
        (XCoordinate, YCoordinate) = _getCoordinateScaleRectangle.Invoke();
    }

    public static void SetStateDisplay(List<ScaleRectangle> scaleRectangles, bool isShowScaleRectangle)
    {
        foreach (var scaleRectangle in scaleRectangles)
        {
            scaleRectangle.IsShow = isShowScaleRectangle;
        }
    }

    private void ShowScaleRectangles()
    {
        if (_canvasSymbolsVM.ScalePartBlockSymbol == null)
        {
            _canvasSymbolsVM.Cursor = _cursorScaling;

            if (_blockSymbolVM is IHasScaleRectangles blockSymbolHasScaleRectangles)
            {
                SetStateDisplay(blockSymbolHasScaleRectangles.ScaleRectangles, true);
            }
        }
    }

    private void HideScaleRectangles()
    {
        _canvasSymbolsVM.Cursor = Cursors.Arrow;

        if (_blockSymbolVM is IHasScaleRectangles blockSymbolHasScaleRectangles)
        {
            SetStateDisplay(blockSymbolHasScaleRectangles.ScaleRectangles, false);
        }
    }

    private void SaveScaleRectangle()
    {
        _canvasSymbolsVM.ScalePartBlockSymbol = new(
            _blockSymbolVM, 
            _cursorScaling,
            _scaleAllSymbolVM,
            _positionScaleRectangle);
    }
}