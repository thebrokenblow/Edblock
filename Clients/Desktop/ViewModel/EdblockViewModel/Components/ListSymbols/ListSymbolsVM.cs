using System;
using Prism.Commands;
using EdblockViewModel.Symbols;
using EdblockViewModel.Components.ListSymbols.Interfaces;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstractions;

namespace EdblockViewModel.Components.ListSymbols;

public class ListSymbolsVM : IListSymbolsVM
{
    public DelegateCommand<string> CreateBlockSymbolCommand { get; }

    private readonly IListCanvasSymbolsVM _listCanvasSymbolsVM;
    private readonly Func<Type, BlockSymbolVM> _factoryBlockSymbol;

    private readonly Dictionary<string, Type> nameByTypeBlockSymbol = new()
    {
        { "ActionSymbolVM", typeof(ActionSymbolVM)}
    };

    public ListSymbolsVM(IListCanvasSymbolsVM listCanvasSymbolsVM, Func<Type, BlockSymbolVM> factoryBlockSymbol)
    {
        _listCanvasSymbolsVM = listCanvasSymbolsVM;
        _factoryBlockSymbol = factoryBlockSymbol;

        CreateBlockSymbolCommand = new(CreateBlockSymbol);
    }

    public void CreateBlockSymbol(string nameBlockSymbol)
    {
        var typeBlockSymbol = nameByTypeBlockSymbol[nameBlockSymbol];
        var blockSymbolVM = _factoryBlockSymbol.Invoke(typeBlockSymbol);

        _listCanvasSymbolsVM.AddBlockSymbol(blockSymbolVM);
    }
}