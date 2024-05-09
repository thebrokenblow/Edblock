using System.Collections.Generic;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

namespace Edblock.SymbolsViewModel.Core;

public interface IHasScaleRectangles
{
    public List<ScaleRectangle> ScaleRectangles { get; set; }
}