using System.Collections.Generic;
using EdblockViewModel.Symbols.ScaleRectangles;

namespace EdblockViewModel.Symbols.Abstraction;

public interface IHaveScaleRectangles
{
    public List<ScaleRectangle> ScaleRectangles { get; init; }
}