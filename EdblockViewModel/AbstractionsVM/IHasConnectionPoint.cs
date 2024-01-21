using System.Collections.Generic;
using EdblockViewModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.AbstractionsVM;

public interface IHasConnectionPoint
{
    public List<ConnectionPointVM> ConnectionPoints { get; init; }
    public FactoryConnectionPoints FactoryConnectionPoints { get; init; }
}