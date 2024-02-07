using System;
using System.Linq;
using System.Reflection;
using EdblockViewModel.AttributeVM;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;
using SerializationEdblock.SymbolsSerializable;

namespace EdblockViewModel.Symbols;

internal class FactoryBlockSymbolVM
{
    private readonly EdblockVM _edblockVM;

    public FactoryBlockSymbolVM(EdblockVM edblockVM)
    {
        _edblockVM = edblockVM;
    }

    public BlockSymbolVM CreateBlockSymbolVM(BlockSymbolSerializable blockSymbolSerializable)
    {
        var symbolType = GetTypeBlockSymbolVM(blockSymbolSerializable.NameSymbol);
        var blockSymbol = Activator.CreateInstance(symbolType, _edblockVM);

        if (blockSymbol is not BlockSymbolVM)
        {
            throw new Exception($"Не удалось создать объект с именем {blockSymbolSerializable.NameSymbol}");
        }

        var blockSymbolVM = (BlockSymbolVM)blockSymbol;

        blockSymbolVM.Width = blockSymbolSerializable.Width;
        blockSymbolVM.Height = blockSymbolSerializable.Height;

        blockSymbolVM.XCoordinate = blockSymbolSerializable.XCoordinate;
        blockSymbolVM.YCoordinate = blockSymbolSerializable.YCoordinate;

        if (blockSymbolVM is IHasTextFieldVM blockSymbolHasTextFieldVM)
        {
            var textField = blockSymbolHasTextFieldVM.TextFieldSymbolVM;
            var textFieldSerializable = blockSymbolSerializable.TextFieldSerializable;

            if (textFieldSerializable is not null)
            {
                textField.Text = textFieldSerializable.Text;
                textField.FontFamily = textFieldSerializable.FontFamily;
                textField.FontSize = textFieldSerializable.FontSize;
                textField.TextAlignment = textFieldSerializable.TextAlignment;
                textField.FontWeight = textFieldSerializable.FontWeight;
                textField.FontStyle = textFieldSerializable.FontStyle;
                textField.TextDecorations = textFieldSerializable.TextDecorations;
            }
        }

        return blockSymbolVM;
    }

    public SwitchCaseSymbolVM CreateBlockSymbolVM(SwitchCaseSymbolsSerializable switchCaseSymbolsSerializable)
    {
        var symbolType = GetTypeBlockSymbolVM(switchCaseSymbolsSerializable.NameSymbol);
        var blockSymbol = Activator.CreateInstance(symbolType, _edblockVM, switchCaseSymbolsSerializable.CountLines);

        if (blockSymbol is not SwitchCaseSymbolVM)
        {
            throw new Exception($"Не удалось создать объект с именем {switchCaseSymbolsSerializable.NameSymbol}");
        }

        var switchCaseSymbolVM = (SwitchCaseSymbolVM)blockSymbol;

        switchCaseSymbolVM.Width = switchCaseSymbolsSerializable.Width;
        switchCaseSymbolVM.Height = switchCaseSymbolsSerializable.Height;

        switchCaseSymbolVM.XCoordinate = switchCaseSymbolsSerializable.XCoordinate;
        switchCaseSymbolVM.YCoordinate = switchCaseSymbolsSerializable.YCoordinate;

        if (switchCaseSymbolVM is IHasTextFieldVM blockSymbolHasTextFieldVM)
        {
            var textField = blockSymbolHasTextFieldVM.TextFieldSymbolVM;
            var textFieldSerializable = switchCaseSymbolsSerializable.TextFieldSerializable;

            if (textFieldSerializable is not null)
            {
                textField.Text = textFieldSerializable.Text;
                textField.FontFamily = textFieldSerializable.FontFamily;
                textField.FontSize = textFieldSerializable.FontSize;
                textField.TextAlignment = textFieldSerializable.TextAlignment;
                textField.FontWeight = textFieldSerializable.FontWeight;
                textField.FontStyle = textFieldSerializable.FontStyle;
                textField.TextDecorations = textFieldSerializable.TextDecorations;
            }
        }

        return switchCaseSymbolVM;
    }

    private static Type GetTypeBlockSymbolVM(string nameSymbolVM)
    {
        return Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(
            type =>
            {
                var attr = type.GetCustomAttribute<SymbolTypeAttribute>();
                if (attr is null)
                {
                    return false;
                }
                return attr.NameSymbol == nameSymbolVM;
            }) ?? throw new Exception($"Нет класса или атрибута с именем {nameSymbolVM}");
    }
}