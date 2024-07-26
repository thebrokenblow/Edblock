namespace EdblockModel.Lines.Factories.Interfaces;

public interface IFactoryLineModel
{
    LineModel Create(double firstXCoordinate, double firstYCoordinate, double secondXCoordinate, double secondYCoordinate);
}