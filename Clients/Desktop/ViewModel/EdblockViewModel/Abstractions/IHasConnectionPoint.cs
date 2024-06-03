using System.Collections.Generic;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

namespace EdblockViewModel.Abstractions;

public interface IHasConnectionPoint
{
    public List<ConnectionPointVM> ConnectionPointsVM { get; set; }
}