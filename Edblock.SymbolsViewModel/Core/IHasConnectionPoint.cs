using System.Collections.Generic;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

namespace Edblock.SymbolsViewModel.Core;

public interface IHasConnectionPoint
{
    public List<ConnectionPointVM> ConnectionPointsVM { get; set; }
}