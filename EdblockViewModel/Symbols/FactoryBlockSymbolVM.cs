using System;
using SerializationEdblock;
using System.Collections.Generic;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

internal class FactoryBlockSymbolVM
{
    private readonly Dictionary<string, Func<string, BlockSymbolVM>> instanceSymbolByName;

    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly CheckBoxLineGostVM _checkBoxLineGostVM;

    public FactoryBlockSymbolVM(CanvasSymbolsVM canvasSymbolsVM, CheckBoxLineGostVM checkBoxLineGostVM)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        _checkBoxLineGostVM = checkBoxLineGostVM;

        instanceSymbolByName = new()
        {
            { "ActionSymbol", _ => new ActionSymbol(_canvasSymbolsVM, _checkBoxLineGostVM) }
        };
    }

    public BlockSymbolVM Create(string? nameBlockSymbol)
    {
        if (nameBlockSymbol == null)
        {
            throw new Exception("nameBlockSymbol is null");
        }

        return instanceSymbolByName[nameBlockSymbol].Invoke(nameBlockSymbol);
    }

    public BlockSymbolVM CreateBySerialization(BlockSymbolSerializable blockSymbolSerializable)
    {
        var nameBlockSymbol = blockSymbolSerializable.NameSymbol;
        
        var blockSymbolVM = Create(nameBlockSymbol);

        blockSymbolVM.Width = blockSymbolSerializable.Width;
        blockSymbolVM.Height = blockSymbolSerializable.Height;

        blockSymbolVM.XCoordinate = blockSymbolSerializable.XCoordinate;
        blockSymbolVM.YCoordinate = blockSymbolSerializable.YCoordinate;

        blockSymbolVM.TextField.Text = blockSymbolSerializable.Text;

        return blockSymbolVM;
    }
}