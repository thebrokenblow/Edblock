using System;
using System.Collections.Generic;
using EdblockModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

internal class FactoryBlockSymbol
{
    private readonly Dictionary<string, Func<string, BlockSymbolVM>> instanceSymbolByName = new();
    public FactoryBlockSymbol(CanvasSymbolsVM canvasSymbolsVM)
    {
        instanceSymbolByName = new()
        {
            { "ActionSymbol", _ => new ActionSymbol(canvasSymbolsVM) }
        };

    }

    public BlockSymbolVM Create(string nameBlockSymbol)
    {
        return instanceSymbolByName[nameBlockSymbol].Invoke(nameBlockSymbol);
    }

    internal BlockSymbolVM CreateByModel(BlockSymbolModel symbolModel)
    {
        var symbolVM = Create(symbolModel.NameOfSymbol);

        symbolVM.XCoordinate = symbolModel.XCoordinate;
        symbolVM.YCoordinate = symbolModel.YCoordinate;

        symbolVM.Width = symbolModel.Width;
        symbolVM.Height = symbolModel.Height;

        symbolModel.Text = symbolModel.Text;

        return symbolVM;
    }
}