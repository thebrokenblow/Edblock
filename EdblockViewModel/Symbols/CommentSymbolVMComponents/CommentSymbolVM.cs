using System.Collections.Generic;
using EdblockModel.SymbolsModel;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.Symbols.CommentSymbolVMComponents;

public class CommentSymbolVM : BlockSymbolVM
{
    public List<CommentLine> HorizontalLines { get; set; }
    public CommentLine VerticalBaseline { get; set; }
    public CommentLine UpperHorizontalBaseline { get; set; }
    public CommentLine LowerHorizontalBaseline { get; set; }
    public BlockSymbolVM? BlockSymbolVM { get; set; }
    public ConnectionPointVM? ConnectionPointVM { get; set; }

    private readonly CanvasSymbolsVM canvasSymbolsVM;

    private double heightTextField;
    public double HeightTextField
    {
        get => heightTextField;
        set
        {
            value = value - value % 10 + 10;

            if (value > defaultHeight)
            {
                heightTextField = value;
            }
            else
            {
                heightTextField = defaultHeight;
            }

            VerticalBaseline.Y1 = 0;
            VerticalBaseline.Y2 = heightTextField;

            YCoordinate -= (heightTextField - Height) / 2;

            Height = heightTextField;

            LowerHorizontalBaseline.Y1 = heightTextField;
            LowerHorizontalBaseline.Y2 = heightTextField;

            foreach (var horizontalLine in HorizontalLines)
            {
                horizontalLine.Y1 = VerticalBaseline.Y2 / 2;
                horizontalLine.Y2 = VerticalBaseline.Y2 / 2;
            }
        }
    }

    private double widthTextField;
    public double WidthTextField 
    {
        get => widthTextField;
        set
        {
            widthTextField = value;
            Width = xCoordinate + (int)widthTextField;
        }
    }

    private const string defaultText = "Комментарий";

    private int xCoordinate = 0;
    private const int defaultHeight = 60;
    private const int countHorizontalLine = 3;
    private const int lengthHorizontalLine = 20;
    private const int spaceBetweenHorizontalLines = 10;
    public CommentSymbolVM(EdblockVM edblockVM) : base(edblockVM)
    {
        TextFieldVM.Text = defaultText;
        TextFieldVM.Height = defaultHeight;
        canvasSymbolsVM = edblockVM.CanvasSymbolsVM;

        HorizontalLines = new();

        for (int i = 0; i < countHorizontalLine; i++)
        {
            var сommentHorizontalLine = new CommentLine()
            {
                X1 = xCoordinate,
                Y1 = defaultHeight / 2,
                X2 = xCoordinate + lengthHorizontalLine,
                Y2 = defaultHeight / 2,
            };

            HorizontalLines.Add(сommentHorizontalLine);

            xCoordinate += lengthHorizontalLine + spaceBetweenHorizontalLines;
        }

        xCoordinate -= spaceBetweenHorizontalLines;

        VerticalBaseline = new()
        {
            X1 = xCoordinate,
            Y1 = 0,
            X2 = xCoordinate,
            Y2 = defaultHeight,
        };

        UpperHorizontalBaseline = new()
        {
            X1 = xCoordinate,
            Y1 = 0,
            X2 = xCoordinate + lengthHorizontalLine,
            Y2 = 0,
        };

        LowerHorizontalBaseline = new()
        {
            X1 = xCoordinate,
            Y1 = defaultHeight,
            X2 = xCoordinate + lengthHorizontalLine,
            Y2 = defaultHeight,
        };

        TextFieldVM.LeftOffset = xCoordinate;

        Width = xCoordinate + (int)widthTextField;
        Height = defaultHeight;
    }

    public override void SetCoordinate((int x, int y) currentCoordinate, (int x, int y) previousCoordinate)
    {
        var currentDrawnLineSymbol = CanvasSymbolsVM.СurrentDrawnLineSymbol;

        if (currentDrawnLineSymbol == null) //Условие истино, если не рисуется линия
        {
            CanvasSymbolsVM.RemoveSelectDrawnLine();

            XCoordinate = currentCoordinate.x + 10;
            YCoordinate = currentCoordinate.y - Height / 2;
        }
    }

    public override BlockSymbolModel CreateBlockSymbolModel()
    {
        return new ActionSymbolModel();
    }

    public override void SetHeight(double height)
    {

    }

    public override void SetWidth(double width)
    {

    }

    public void SetCoordinateBlockSymbol()
    {
        if (BlockSymbolVM is not null)
        {
            XCoordinate = BlockSymbolVM.XCoordinate + BlockSymbolVM.Width;
            YCoordinate = BlockSymbolVM.YCoordinate + BlockSymbolVM.Height / 2 - VerticalBaseline.Y2 / 2;
        }
    }

    public void AddСomment(BlockSymbolVM blockSymbolVM)
    {
        BlockSymbolVM = blockSymbolVM;

        SetCoordinateBlockSymbol();

        IsSelected = false;
        canvasSymbolsVM.SelectedBlockSymbols.Remove(this);
        canvasSymbolsVM.MovableBlockSymbol = null;
    }
}