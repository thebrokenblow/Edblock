using System.Collections.Generic;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

namespace EdblockViewModel.AbstractionsVM;

public interface IHasConnectionPoint
{
    public List<ConnectionPointVM> ConnectionPointsVM { get; init; }
}