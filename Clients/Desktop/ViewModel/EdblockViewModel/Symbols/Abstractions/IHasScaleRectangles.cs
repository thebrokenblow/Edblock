using System.Collections.Generic;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

namespace EdblockViewModel.Symbols.Abstractions;

public interface IHasScaleRectangles
{
    public List<ScaleRectangle> ScaleRectangles { get; set; }
}