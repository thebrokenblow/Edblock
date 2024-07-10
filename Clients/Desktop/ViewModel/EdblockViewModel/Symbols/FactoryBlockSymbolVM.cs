using System;
using System.Linq;
using System.Reflection;
using EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;
using Edblock.SymbolsSerialization.Symbols;
using EdblockViewModel.Symbols.Attributes;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

namespace EdblockViewModel.Symbols;

public class FactoryBlockSymbolVM(
    ICanvasSymbolsComponentVM canvasSymbolsComponentVM, 
    IListCanvasSymbolsComponentVM listCanvasSymbolsComponentVM, 
    ITopSettingsMenuComponentVM topSettingsMenuComponentVM, 
    IPopupBoxMenuComponentVM popupBoxMenuComponentVM, 
    Func<Type, BlockSymbolVM> factoryBlockSymbol) : IFactoryBlockSymbolVM
{
    public BlockSymbolVM CreateBlockSymbolVM(BlockSymbolSerializable blockSymbolSerializable)
    {
        var symbolType = GetTypeBlockSymbolVM(blockSymbolSerializable.NameSymbol);
        var blockSymbolVM = factoryBlockSymbol.Invoke(symbolType) ?? throw new Exception($"Не удалось создать объект с именем {blockSymbolSerializable.NameSymbol}");

        if (blockSymbolVM is ScalableBlockSymbolVM scalableBlockSymbolVM)
        {
            scalableBlockSymbolVM.SetWidth(blockSymbolSerializable.Width);
            scalableBlockSymbolVM.SetHeight(blockSymbolSerializable.Height);
        }

        blockSymbolVM.XCoordinate = blockSymbolSerializable.XCoordinate;
        blockSymbolVM.YCoordinate = blockSymbolSerializable.YCoordinate;

        if (blockSymbolVM is IHasTextFieldVM blockSymbolHasTextFieldVM)
        {
            SeTextFieldProperties(blockSymbolSerializable, blockSymbolHasTextFieldVM);
        }

        return blockSymbolVM;
    }

    public SwitchCaseSymbolVM CreateBlockSymbolVM(SwitchCaseSymbolsSerializable switchCaseSymbolsSerializable)
    {
        var symbolType = GetTypeBlockSymbolVM(switchCaseSymbolsSerializable.NameSymbol);

        var builderScaleRectangles = new BuilderScaleRectangles(canvasSymbolsComponentVM, popupBoxMenuComponentVM.ScaleAllSymbolComponentVM);
        var blockSymbol = Activator.CreateInstance(
            symbolType,
            builderScaleRectangles,
            canvasSymbolsComponentVM,
            listCanvasSymbolsComponentVM,
            topSettingsMenuComponentVM,
            popupBoxMenuComponentVM,
            switchCaseSymbolsSerializable.CountLines);

        if (blockSymbol is not SwitchCaseSymbolVM)
        {
            throw new Exception($"Не удалось создать объект с именем {switchCaseSymbolsSerializable.NameSymbol}");
        }

        var switchCaseSymbolVM = (SwitchCaseSymbolVM)blockSymbol;

        switchCaseSymbolVM.SetWidth(switchCaseSymbolsSerializable.Width);
        switchCaseSymbolVM.SetHeight(switchCaseSymbolsSerializable.Height);

        switchCaseSymbolVM.XCoordinate = switchCaseSymbolsSerializable.XCoordinate;
        switchCaseSymbolVM.YCoordinate = switchCaseSymbolsSerializable.YCoordinate;

        if (switchCaseSymbolVM is IHasTextFieldVM blockSymbolHasTextFieldVM)
        {
            SeTextFieldProperties(switchCaseSymbolsSerializable, blockSymbolHasTextFieldVM);
        }

        return switchCaseSymbolVM;
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

    private static void SeTextFieldProperties(BlockSymbolSerializable blockSymbolSerializable, IHasTextFieldVM symbolHasTextField)
    {
        var textField = symbolHasTextField.TextFieldSymbolVM;
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
}