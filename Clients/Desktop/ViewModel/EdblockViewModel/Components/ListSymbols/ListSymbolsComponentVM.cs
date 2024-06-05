using System;
using Prism.Commands;
using EdblockViewModel.Symbols;
using EdblockViewModel.Components.ListSymbols.Interfaces;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstractions;

namespace EdblockViewModel.Components.ListSymbols;

public class ListSymbolsComponentVM : IListSymbolsComponentVM
{
    public DelegateCommand<string> CreateBlockSymbolCommand { get; }

    private readonly IListCanvasSymbolsComponentVM _llistCanvasSymbolsComponentVM;
    private readonly Func<Type, BlockSymbolVM> _factoryBlockSymbolVM;

    private readonly Dictionary<string, Type> nameByTypeBlockSymbol = new()
    {
        { "ActionSymbolVM", typeof(ActionSymbolVM)}
    };

    public ListSymbolsComponentVM(IListCanvasSymbolsComponentVM listCanvasSymbolsComponentVM, Func<Type, BlockSymbolVM> factoryBlockSymbol)
    {
        _llistCanvasSymbolsComponentVM = listCanvasSymbolsComponentVM;
        _factoryBlockSymbolVM = factoryBlockSymbol;

        CreateBlockSymbolCommand = new(CreateBlockSymbol);
    }

    public void CreateBlockSymbol(string nameBlockSymbol)
    {
        var typeBlockSymbol = nameByTypeBlockSymbol[nameBlockSymbol];
        var blockSymbolVM = _factoryBlockSymbolVM.Invoke(typeBlockSymbol);

        _llistCanvasSymbolsComponentVM.AddBlockSymbol(blockSymbolVM);
    }
}