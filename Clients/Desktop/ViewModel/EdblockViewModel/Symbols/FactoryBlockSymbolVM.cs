using System;
using System.Linq;
using System.Reflection;
using EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;
using Edblock.SymbolsSerialization.Symbols;
using EdblockViewModel.Pages;
using EdblockViewModel.Symbols.Attributes;
using EdblockViewModel.Symbols.Abstractions;

namespace EdblockViewModel.Symbols;

internal class FactoryBlockSymbolVM(EditorVM edblockVM)
{
    public BlockSymbolVM CreateBlockSymbolVM(BlockSymbolSerializable blockSymbolSerializable)
    {
        var symbolType = GetTypeBlockSymbolVM(blockSymbolSerializable.NameSymbol);
        var blockSymbol = Activator.CreateInstance(symbolType, edblockVM);

        if (blockSymbol is not BlockSymbolVM)
        {
            throw new Exception($"Не удалось создать объект с именем {blockSymbolSerializable.NameSymbol}");
        }

        var blockSymbolVM = (BlockSymbolVM)blockSymbol;

        //blockSymbolVM.SetWidth(blockSymbolSerializable.Width);
        //blockSymbolVM.SetHeight(blockSymbolSerializable.Height);

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
        var blockSymbol = Activator.CreateInstance(symbolType, edblockVM, switchCaseSymbolsSerializable.CountLines);

        if (blockSymbol is not SwitchCaseSymbolVM)
        {
            throw new Exception($"Не удалось создать объект с именем {switchCaseSymbolsSerializable.NameSymbol}");
        }

        var switchCaseSymbolVM = (SwitchCaseSymbolVM)blockSymbol;

        //switchCaseSymbolVM.SetWidth(switchCaseSymbolsSerializable.Width);
        //switchCaseSymbolVM.SetHeight(switchCaseSymbolsSerializable.Height);

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

    public BlockSymbolVM CreateBlockSymbolVM(ParallelActionSymbolSerializable parallelActionSymbolSerializable)
    {
        var symbolType = GetTypeBlockSymbolVM(parallelActionSymbolSerializable.NameSymbol);

        var blockSymbol = Activator.CreateInstance(
            symbolType,
            edblockVM, 
            parallelActionSymbolSerializable.CountSymbolsIncoming, 
            parallelActionSymbolSerializable.CountSymbolsOutgoing);

        if (blockSymbol is not BlockSymbolVM)
        {
            throw new Exception($"Не удалось создать объект с именем {parallelActionSymbolSerializable.NameSymbol}");
        }

        var parallelActionSymbolVM = (BlockSymbolVM)blockSymbol;

        //parallelActionSymbolVM.SetWidth(parallelActionSymbolSerializable.Width);
        //parallelActionSymbolVM.SetHeight(parallelActionSymbolSerializable.Height);

        parallelActionSymbolVM.XCoordinate = parallelActionSymbolSerializable.XCoordinate;
        parallelActionSymbolVM.YCoordinate = parallelActionSymbolSerializable.YCoordinate;

        return parallelActionSymbolVM;
    }

    private static Type GetTypeBlockSymbolVM(string nameSymbolVM)
    {
        return Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(
            type =>
            {
                var symbolTypeAttribute = type.GetCustomAttribute<SymbolTypeAttribute>();

                if (symbolTypeAttribute is null)
                {
                    return false;
                }

                return symbolTypeAttribute.NameSymbol == nameSymbolVM;

            }) ?? throw new Exception($"Нет класса или атрибута с именем {nameSymbolVM}");
    }
}