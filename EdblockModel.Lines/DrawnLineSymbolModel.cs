using EdblockModel.Lines.DecoratorLine;
using EdblockModel.Lines.DecoratorLine.Interfaces;
using EdblockModel.Lines.Factories;
using EdblockModel.Lines.Factories.Interfaces;

namespace EdblockModel.Lines;

public class DrawnLineSymbolModel()
{
    public List<LineModel> Lines { get; set; } = [];
    public string Text { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public SideSymbol OutgoingPosition { get; set; }

    private readonly IFactoryLineModel factoryLineModel = new FactoryLineModel(new FactoryCoordinateDecorator());

    public List<LineModel> Redraw(ICoordinateDecorator currentCoordinate)
    {        
        if (OutgoingPosition == SideSymbol.Top || OutgoingPosition == SideSymbol.Bottom)
        {
            foreach (var line in Lines)
            {
                line.FirstCoordinate = new SwapCoordinateDecorator(line.FirstCoordinate);
                line.SecondCoordinate = new SwapCoordinateDecorator(line.SecondCoordinate);
            }

            currentCoordinate = new SwapCoordinateDecorator(currentCoordinate);
        }

        if (Lines.Count % 2 == 1)
        {
            var lastLine = Lines[^1];
            lastLine.SecondCoordinate.X = currentCoordinate.X;

            if (currentCoordinate.Y != lastLine.FirstCoordinate.Y)
            {
                var currentLastLine = factoryLineModel.Create(
                    lastLine.SecondCoordinate.X,
                    lastLine.SecondCoordinate.Y,
                    lastLine.SecondCoordinate.X,
                    lastLine.SecondCoordinate.Y);

                Lines.Add(currentLastLine);
            }
        }
        else
        {
            var lastLine = Lines[^1];
            var penultimateLine = Lines[^2];
            penultimateLine.SecondCoordinate.X = currentCoordinate.X;

            if (currentCoordinate.Y == lastLine.FirstCoordinate.Y)
            {
                Lines.Remove(lastLine);
            }
            else
            {
                lastLine.FirstCoordinate.X = currentCoordinate.X;
                lastLine.SecondCoordinate.X = currentCoordinate.X;
                lastLine.SecondCoordinate.Y = currentCoordinate.Y;
            }
        }

        foreach (var line in Lines)
        {
            line.FirstCoordinate.X = line.FirstCoordinate.X;
            line.FirstCoordinate.Y = line.FirstCoordinate.Y;

            line.SecondCoordinate.X = line.SecondCoordinate.X;
            line.SecondCoordinate.Y = line.SecondCoordinate.Y;
        }

        return Lines;
    }
}