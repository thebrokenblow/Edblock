﻿using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

namespace EdblockViewModel.Symbols;

public class CycleForSymbolVM : BlockSymbolVM, IHasTextFieldVM, IHasConnectionPoint, IHasScaleRectangles
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; init; }
    public List<ScaleRectangle> ScaleRectangles { get; init; }
    public List<ConnectionPointVM> ConnectionPointsVM { get; init; }
    public BuilderConnectionPointsVM BuilderConnectionPointsVM { get; init; }
    public CoordinateConnectionPointVM CoordinateConnectionPointVM { get; init; }
    public BuilderScaleRectangles BuilderScaleRectangles { get; init; }

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
    
    private const int sideProjection = 10;

    private const string defaultText = "Цикл for";
    private const string defaultColor = "#FFC618";

    public CycleForSymbolVM(EdblockVM edblockVM) : base(edblockVM)
    {
        TextFieldSymbolVM = new(CanvasSymbolsVM, this);

        CoordinateConnectionPointVM = new(this);

        BuilderConnectionPointsVM = new(
            CanvasSymbolsVM,
            this,
            _checkBoxLineGostVM);

        ConnectionPointsVM = BuilderConnectionPointsVM
            .AddTopConnectionPoint()
            .AddRightConnectionPoint()
            .AddBottomConnectionPoint()
            .AddLeftConnectionPoint()
            .Create();

        BuilderScaleRectangles = new(
            CanvasSymbolsVM,
            edblockVM.PopupBoxMenuVM.ScaleAllSymbolVM,
            this);

        ScaleRectangles =
            BuilderScaleRectangles
                        .AddMiddleTopRectangle()
                        .AddRightTopRectangle()
                        .AddRightMiddleRectangle()
                        .AddRightBottomRectangle()
                        .AddMiddleBottomRectangle()
                        .AddLeftBottomRectangle()
                        .AddLeftMiddleRectangle()
                        .AddLeftTopRectangle()
                        .Build();

        Color = defaultColor;

        TextFieldSymbolVM.Text = defaultText;

        SetWidth(defaultWidth);
        SetHeight(defaultHeigth);
    }

    public override void SetWidth(double width)
    {
        Width = width;

        TextFieldSymbolVM.Width = Width - sideProjection * 2;
        TextFieldSymbolVM.LeftOffset = sideProjection;

        SetCoordinatePolygonPoints();
        ChangeCoordinateScaleRectangle();
        CoordinateConnectionPointVM.SetCoordinate();
    }

    public override void SetHeight(double height)
    {
        Height = height;

        TextFieldSymbolVM.Height = height;

        SetCoordinatePolygonPoints();
        ChangeCoordinateScaleRectangle();
        CoordinateConnectionPointVM.SetCoordinate();
    }

    public void SetCoordinatePolygonPoints()
    {
        Points = new()
        {
            new Point(sideProjection, 0),
            new Point(0, sideProjection),
            new Point(0, Height - sideProjection),
            new Point(sideProjection, Height),
            new Point(Width - sideProjection, Height),
            new Point(Width, Height - sideProjection),
            new Point(Width, sideProjection),
            new Point(Width - sideProjection, 0),
        };
    }
}