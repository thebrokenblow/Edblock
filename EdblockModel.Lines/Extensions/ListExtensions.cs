namespace EdblockModel.Lines.Extensions;

public static class ListExtensions
{
    public static TSource Penultimate<TSource>(this List<TSource> source) =>
        source[^2];
}
