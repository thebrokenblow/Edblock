﻿using System.Windows.Media;
using System.Collections.Generic;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.Attributes;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles.Interfaces;
using EdblockViewModel.Components.Interfaces;
using EdblockViewModel.Components.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Symbols;

[SymbolType("InputOutputSymbolVM")]
public sealed class InputOutputSymbolVM : ScalableBlockSymbolVM, IHasTextFieldVM, IHasConnectionPoint
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; init; }
    public List<ConnectionPointVM> ConnectionPointsVM { get; set; } = null!;

    private readonly CoordinateConnectionPointVM coordinateConnectionPointVM;

    private PointCollection? points;
    public PointCollection? Points
    {
        get => points;
        set
        {
            points = value;
            OnPropertyChanged();
        }
    }

    private const int defaultWidth = 140;
    private const int defaultHeigth = 60;

    private const int sideProjection = 20;

    private const string defaultText = "Ввод / Вывод";
    private const string defaultColor = "#FF008080";

    public InputOutputSymbolVM(
        IBuilderScaleRectangles builderScaleRectangles,
        ICanvasSymbolsComponentVM canvasSymbolsComponentVM,
        IListCanvasSymbolsComponentVM listCanvasSymbolsComponentVM,
        ITopSettingsMenuComponentVM topSettingsMenuComponentVM,
        IPopupBoxMenuComponentVM popupBoxMenuComponentVM) : base(
            builderScaleRectangles, 
            canvasSymbolsComponentVM, 
            listCanvasSymbolsComponentVM, 
            topSettingsMenuComponentVM, 
            popupBoxMenuComponentVM)
    {
        TextFieldSymbolVM = new(_canvasSymbolsComponentVM, this)
        {
            Text = defaultText,
            LeftOffset = sideProjection
        };

        Color = defaultColor;

        AddConnectionPoints();

        coordinateConnectionPointVM = new(this);

        SetWidth(defaultWidth);
        SetHeight(defaultHeigth);
    }

    public override void SetWidth(double width)
    {
        Width = width;
        TextFieldSymbolVM.Width = width - sideProjection * 2;

        SetCoordinatePolygonPoints();
        ChangeCoordinateScaleRectangle();

        foreach (var connectionPointsVM in ConnectionPointsVM)
        {
            connectionPointsVM.SetCoordinate();
        }
    }

    public override void SetHeight(double height)
    {
        Height = height;
        TextFieldSymbolVM.Height = height;

        SetCoordinatePolygonPoints();
        ChangeCoordinateScaleRectangle();

        foreach (var connectionPointsVM in ConnectionPointsVM)
        {
            connectionPointsVM.SetCoordinate();
        }
    }

    public void SetCoordinatePolygonPoints()
    {
        Points =
        [
            new(sideProjection, 0),
            new(0, height),
            new(width - sideProjection, height),
            new(width, 0)
        ];
    }

    private void AddConnectionPoints()
    {
        var builderConnectionPointsVM = new BuilderConnectionPointsVM(
            _canvasSymbolsComponentVM,
            _lineStateStandardComponentVM,
            this);

        ConnectionPointsVM = builderConnectionPointsVM
            .AddTopConnectionPoint()
            .AddRightConnectionPoint()
            .AddBottomConnectionPoint()
            .AddLeftConnectionPoint()
            .Build();
    }
}