using System.Collections.Generic;
using EdblockViewModel.Symbols.Attributes;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles.Interfaces;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.Observers.Interfaces;
using EdblockViewModel.Components.Interfaces;
using EdblockViewModel.Components.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Symbols;

[SymbolType("ActionSymbolVM")]
public sealed class ActionSymbolVM : 
    ScalableBlockSymbolVM, 
    IHasTextFieldVM, 
    IHasConnectionPoint, 
    IObserverColor, 
    IObserverFontFamily, 
    IObserverFontSize, 
    IObserverFormatText,
    IObserverTextAlignment
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; }
    public List<ConnectionPointVM> ConnectionPointsVM { get; set; } = null!;

    private const int defaultWidth = 140;
    private const int defaultHeigth = 60;

    private const string defaultText = "Действие";
    private const string defaultColor = "#FF52C0AA";

    public ActionSymbolVM(
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
            Text = defaultText
        };

        Color = defaultColor;

        AddConnectionPoints();
        
        SetWidth(defaultWidth);
        SetHeight(defaultHeigth);
    }

    public override void SetWidth(double width)
    {
        Width = width;
        TextFieldSymbolVM.Width = width;

        ChangeCoordinateScaleRectangle();

        foreach (var connectionPointVM in ConnectionPointsVM)
        {
            connectionPointVM.SetCoordinate();
        }
    }

    public override void SetHeight(double height)
    {
        Height = height;
        TextFieldSymbolVM.Height = height;

        ChangeCoordinateScaleRectangle();

        foreach (var connectionPointVM in ConnectionPointsVM)
        {
            connectionPointVM.SetCoordinate();
        }
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
    
    public void UpdateFontSize()
    {
        TextFieldSymbolVM.FontSize = _topSettingsMenuComponentVM.FontSizeSubject.SelectedFontSize;
    }

    public void UpdateColor()
    {
        Color = _topSettingsMenuComponentVM.ColorSubject.SelectedColor;
    }

    public void UpdateFontFamily()
    {
        TextFieldSymbolVM.FontFamily = _topSettingsMenuComponentVM.FontFamilySubject.SelectedFontFamily;
    }

    public void UpdateTextBold()
    {
        TextFieldSymbolVM.FontWeight = _topSettingsMenuComponentVM.FormatTextSubject.TextBold.ToString();
    }

    public void UpdateFormatItalic()
    {
        TextFieldSymbolVM.FontStyle = _topSettingsMenuComponentVM.FormatTextSubject.TextItalic.ToString();
    }

    public void UpdateTextDecorations()
    {
        TextFieldSymbolVM.TextDecorations = _topSettingsMenuComponentVM.FormatTextSubject.TextUnderline.ToString();
    }

    public void UpdateFormatText()
    {
        UpdateTextBold();
        UpdateFormatItalic();
        UpdateTextDecorations();
    }

    public void UpdateTextAlignment()
    {
        TextFieldSymbolVM.TextAlignment = _topSettingsMenuComponentVM.TextAlignmentSubject.SelectedTextAlignment.ToString();
    }
}