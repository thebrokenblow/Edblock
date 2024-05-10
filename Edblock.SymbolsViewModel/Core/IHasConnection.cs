namespace Edblock.SymbolsViewModel.Core;

public interface IHasConnection
{
    public List<ConnectionPointVM> ConnectionPointsVM { get; set; }
}