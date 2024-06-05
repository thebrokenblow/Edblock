using Prism.Commands;

namespace EdblockViewModel.Components.ListSymbols.Interfaces;

public interface IListSymbolsComponentVM
{
    DelegateCommand<string> CreateBlockSymbolCommand { get; }
    void CreateBlockSymbol(string nameBlockSymbol);
}