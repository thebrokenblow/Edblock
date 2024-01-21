using System.Collections.Generic;
using EdblockViewModel.Symbols.ScaleRectangles;

namespace EdblockViewModel.AbstractionsVM;

public interface IHasScaleRectangles
{
    public List<ScaleRectangle> ScaleRectangles { get; init; }
    public BuilderScaleRectangles BuilderScaleRectangles { get; init; }
}