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

    private const int countHorizontalLine = 3;
    private const int lengthHorizontalLine = 20;
    private const int spaceBetweenHorizontalLines = 10;
    public CommentSymbolVM(EdblockVM edblockVM) : base(edblockVM)
    {
        canvasSymbolsVM = edblockVM.CanvasSymbolsVM;

        HorizontalLines = new();

        int xCoordinate = 0;

        for (int i = 0; i < countHorizontalLine; i++)
        {
            var сommentHorizontalLine = new CommentLine()
            {
                X1 = xCoordinate,
                Y1 = 30,
                X2 = xCoordinate + lengthHorizontalLine,
                Y2 = 30,
            };

            HorizontalLines.Add(сommentHorizontalLine);

            xCoordinate += lengthHorizontalLine + spaceBetweenHorizontalLines;
        }

        xCoordinate -= spaceBetweenHorizontalLines;

        VerticalBaseline = new()
        {
            X1 = xCoordinate,
            Y1 = 60,
            X2 = xCoordinate,
            Y2 = 0,
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
            Y1 = 60,
            X2 = xCoordinate + lengthHorizontalLine,
            Y2 = 60,
        };

        Width = 100;
        Height = 60;
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

    public override void SetHeight(int height)
    {

    }

    public override void SetWidth(int width)
    {

    }

    public void SetCoordinateBlockSymbol()
    {
        if (BlockSymbolVM is not null)
        {
            XCoordinate = BlockSymbolVM.XCoordinate + BlockSymbolVM.Width;
            YCoordinate = BlockSymbolVM.YCoordinate;
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