namespace EdblockViewModel.Symbols.Abstraction;

public interface IArrayDecorator
{
    int this[int index]
    {
        get;
        set;
    }
}