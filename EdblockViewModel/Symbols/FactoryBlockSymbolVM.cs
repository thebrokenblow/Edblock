using System;
using SerializationEdblock;
using System.Collections.Generic;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

internal class FactoryBlockSymbolVM
{
    private readonly Dictionary<string, Func<string, BlockSymbolVM>> instanceSymbolByName;

    private BlockSymbolVM? _firstBlockSymbolVM;
    private readonly ScaleAllSymbolVM _scaleAllSymbolVM;

    public FactoryBlockSymbolVM(CanvasSymbolsVM canvasSymbolsVM, ScaleAllSymbolVM scaleAllSymbolVM, CheckBoxLineGostVM checkBoxLineGostVM, FontFamilyControlVM fontFamilyControlVM)
    {
        _scaleAllSymbolVM = scaleAllSymbolVM;

        instanceSymbolByName = new()
        {
            { "ActionSymbol", _ => new ActionSymbol(canvasSymbolsVM, scaleAllSymbolVM, checkBoxLineGostVM, fontFamilyControlVM) }
        };
    }

    public BlockSymbolVM Create(string? nameBlockSymbol)
    {
        if (nameBlockSymbol == null)
        {
            throw new Exception("nameBlockSymbol is null");
        }

        var blockSymbolVM = instanceSymbolByName[nameBlockSymbol].Invoke(nameBlockSymbol);
        _firstBlockSymbolVM ??= blockSymbolVM;

        if (_scaleAllSymbolVM.IsScaleAllSymbolVM)
        {
            blockSymbolVM.Width = _firstBlockSymbolVM.Width;
            blockSymbolVM.Height = _firstBlockSymbolVM.Height;
        }

        return blockSymbolVM;
    }

    public BlockSymbolVM CreateBySerialization(BlockSymbolSerializable blockSymbolSerializable)
    {
        var nameBlockSymbol = blockSymbolSerializable.NameSymbol;
        
        var blockSymbolVM = Create(nameBlockSymbol);

        blockSymbolVM.Width = blockSymbolSerializable.Width;
        blockSymbolVM.Height = blockSymbolSerializable.Height;

        blockSymbolVM.XCoordinate = blockSymbolSerializable.XCoordinate;
        blockSymbolVM.YCoordinate = blockSymbolSerializable.YCoordinate;

        blockSymbolVM.TextField.Text = blockSymbolSerializable.TextFieldSerializable.Text;
        blockSymbolVM.TextField.FontFamily = blockSymbolSerializable.TextFieldSerializable.FontFamily;

        return blockSymbolVM;
    }
}