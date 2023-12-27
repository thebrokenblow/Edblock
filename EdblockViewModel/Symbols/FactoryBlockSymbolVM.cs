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

    public FactoryBlockSymbolVM(EdblockVM edblockVM)
    {
        _scaleAllSymbolVM = edblockVM.PopupBoxMenuVM.ScaleAllSymbolVM;

        instanceSymbolByName = new()
        {
            { "ActionSymbol", _ => new ActionSymbolVM(edblockVM) },
            { "ConditionSymbol", _ => new ConditionSymbolVM(edblockVM) }
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

        var textField = blockSymbolVM.TextFieldVM;
        var textFieldSerializable = blockSymbolSerializable.TextFieldSerializable;

        if (textFieldSerializable != null)
        {
            textField.Text = textFieldSerializable.Text;
            textField.FontFamily = textFieldSerializable.FontFamily;
            textField.FontSize = textFieldSerializable.FontSize;
            textField.TextAlignment = textFieldSerializable.TextAlignment;
            textField.FontWeight = textFieldSerializable.FontWeight;
            textField.FontStyle = textFieldSerializable.FontStyle;
            textField.TextDecorations = textFieldSerializable.TextDecorations;
        }

        return blockSymbolVM;
    }
}