namespace EdblockModel.AbstractionsModel;

public interface IHasSize
{
    public double Width { get; set; }
    public double Height { get; set; }
    public double MinWidth { get; }
    public double MinHeight { get; }
}