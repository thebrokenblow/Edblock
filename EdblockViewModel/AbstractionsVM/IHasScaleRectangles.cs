using System.Collections.Generic;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

namespace EdblockViewModel.AbstractionsVM;

public interface IHasScaleRectangles
{
    public List<ScaleRectangle> ScaleRectangles { get; set; }
}