using System.Reflection;
using Edblock.SymbolsViewModel.Core;
using Edblock.PagesViewModel.Pages;
using SerializationEdblock.SymbolsSerializable;
using Edblock.SymbolsViewModel.Attributes;
using Edblock.SymbolsViewModel.Symbols.SwitchCaseConditionSymbols;

namespace Edblock.SymbolsViewModel.Symbols;

internal class FactoryBlockSymbolViewModel(EditorViewModel editorViewModel)
{
    public BlockSymbolViewModel CreateBlockSymbolVM(BlockSymbolSerializable blockSymbolSerializable)
    {
        var symbolType = GetTypeBlockSymbolVM(blockSymbolSerializable.NameSymbol);
        var blockSymbol = Activator.CreateInstance(symbolType, editorViewModel);

        if (blockSymbol is not BlockSymbolViewModel)
        {
            throw new Exception($"Не удалось создать объект с именем {blockSymbolSerializable.NameSymbol}");
        }

        var blockSymbolVM = (BlockSymbolViewModel)blockSymbol;

        blockSymbolVM.SetWidth(blockSymbolSerializable.Width);
        blockSymbolVM.SetHeight(blockSymbolSerializable.Height);

        blockSymbolVM.XCoordinate = blockSymbolSerializable.XCoordinate;
        blockSymbolVM.YCoordinate = blockSymbolSerializable.YCoordinate;

        if (blockSymbolVM is IHasTextField blockSymbolHasTextFieldVM)
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

    public SwitchCaseSymbolViewModel CreateBlockSymbolVM(SwitchCaseSymbolsSerializable switchCaseSymbolsSerializable)
    {
        var symbolType = GetTypeBlockSymbolVM(switchCaseSymbolsSerializable.NameSymbol);
        var blockSymbol = Activator.CreateInstance(symbolType, editorViewModel, switchCaseSymbolsSerializable.CountLines);

        if (blockSymbol is not SwitchCaseSymbolViewModel)
        {
            throw new Exception($"Не удалось создать объект с именем {switchCaseSymbolsSerializable.NameSymbol}");
        }

        var switchCaseSymbolVM = (SwitchCaseSymbolViewModel)blockSymbol;

        switchCaseSymbolVM.SetWidth(switchCaseSymbolsSerializable.Width);
        switchCaseSymbolVM.SetHeight(switchCaseSymbolsSerializable.Height);

        switchCaseSymbolVM.XCoordinate = switchCaseSymbolsSerializable.XCoordinate;
        switchCaseSymbolVM.YCoordinate = switchCaseSymbolsSerializable.YCoordinate;

        if (switchCaseSymbolVM is IHasTextField blockSymbolHasTextFieldVM)
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

    public BlockSymbolViewModel CreateBlockSymbolVM(ParallelActionSymbolSerializable parallelActionSymbolSerializable)
    {
        var symbolType = GetTypeBlockSymbolVM(parallelActionSymbolSerializable.NameSymbol);

        var blockSymbol = Activator.CreateInstance(
            symbolType,
            editorViewModel,
            parallelActionSymbolSerializable.CountSymbolsIncoming,
            parallelActionSymbolSerializable.CountSymbolsOutgoing);

        if (blockSymbol is not BlockSymbolViewModel)
        {
            throw new Exception($"Не удалось создать объект с именем {parallelActionSymbolSerializable.NameSymbol}");
        }

        var parallelActionSymbolVM = (BlockSymbolViewModel)blockSymbol;

        parallelActionSymbolVM.SetWidth(parallelActionSymbolSerializable.Width);
        parallelActionSymbolVM.SetHeight(parallelActionSymbolSerializable.Height);

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