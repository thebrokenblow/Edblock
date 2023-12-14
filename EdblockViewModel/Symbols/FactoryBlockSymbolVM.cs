using System;
using SerializationEdblock;
using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.Abstraction;
using EdblockModel.Symbols;

namespace EdblockViewModel.Symbols;

internal class FactoryBlockSymbolVM
{
    private readonly Dictionary<string, Func<string, BlockSymbolVM>> instanceSymbolByName;
    private readonly Dictionary<string, Func<string, BlockSymbolVM>> instanceSymbolByName1;

    private CanvasSymbolsVM _canvasSymbolsVM;
    private BlockSymbolModel? _blockSymbolModel;
    private string? _id;

    public FactoryBlockSymbolVM(CanvasSymbolsVM canvasSymbolsVM)
    {
        _canvasSymbolsVM = canvasSymbolsVM;

        instanceSymbolByName1 = new()
        {
             { "ActionSymbol", _ => new ActionSymbol(_canvasSymbolsVM, _blockSymbolModel, _id) }
        };

        instanceSymbolByName = new()
        {
            { "ActionSymbol", _ => new ActionSymbol(_canvasSymbolsVM) }
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

    public BlockSymbolVM Create(string? nameBlockSymbol, BlockSymbolModel blockSymbolModel, string id)
    {
        _blockSymbolModel = blockSymbolModel;
        _id = id; 

        if (nameBlockSymbol == null)
        {
            throw new Exception("nameBlockSymbol is null");
        }

        return instanceSymbolByName1[nameBlockSymbol].Invoke(nameBlockSymbol);
    }

    public BlockSymbolVM CreateBySerialization(BlockSymbolSerializable blockSymbolSerializable)
    {
        var nameBlockSymbol = blockSymbolSerializable.NameSymbol;
        var id = blockSymbolSerializable.Id;

        var blockSymbolModel = FactoryBlockSymbolModel.Create(nameBlockSymbol, id);
        
        var blockSymbolVM = Create(nameBlockSymbol, blockSymbolModel, id);

        blockSymbolVM.Width = blockSymbolSerializable.Width;
        blockSymbolVM.Height = blockSymbolSerializable.Height;

        blockSymbolVM.XCoordinate = blockSymbolSerializable.XCoordinate;
        blockSymbolVM.YCoordinate = blockSymbolSerializable.YCoordinate;

        blockSymbolVM.TextField.Text = blockSymbolSerializable.Text;

        return blockSymbolVM;
    }
}