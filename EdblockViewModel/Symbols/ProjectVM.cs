using SerializationEdblock;
using System.Collections.Generic;
using EdblockViewModel.ComponentsVM;
using EdblockModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.LineSymbols;

namespace EdblockViewModel.Symbols;

internal class ProjectVM
{
    private readonly ScaleAllSymbolVM _scaleAllSymbolVM;
    private readonly CheckBoxLineGostVM _checkBoxLineGostVM;
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly FactoryBlockSymbolVM _factoryBlockSymbolVM;
    private readonly Dictionary<string, BlockSymbolVM> _blockSymbolsVMById;

    public ProjectVM(CanvasSymbolsVM canvasSymbolsVM, ScaleAllSymbolVM scaleAllSymbolVM, CheckBoxLineGostVM checkBoxLineGostVM)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        _scaleAllSymbolVM = scaleAllSymbolVM;
        _checkBoxLineGostVM = checkBoxLineGostVM;
        _blockSymbolsVMById = new();
        _factoryBlockSymbolVM = new(_canvasSymbolsVM, scaleAllSymbolVM, checkBoxLineGostVM);
    }

    public void Save(string filePath)
    {
        var blocksSymbolSerializable = new List<BlockSymbolSerializable>();
        var drawnLinesSymbolSerializable = new List<DrawnLineSymbolSerializable>();

        var SymbolsVM = _canvasSymbolsVM.SymbolsVM;

        foreach (var symbol in SymbolsVM)
        {
            if (symbol is BlockSymbolVM blockSymbolVM)
            {
                var blockSymbolModel = blockSymbolVM.BlockSymbolModel;
                var blockSymbolSerializable = FactorySymbolSerializable.CreateBlockSymbolSerializable(blockSymbolModel);

                blocksSymbolSerializable.Add(blockSymbolSerializable);
            }

            if (symbol is DrawnLineSymbolVM drawnLineSymbolVM)
            {
                var drawnLineSymbolModel = drawnLineSymbolVM.DrawnLineSymbolModel;
                var drawnLineSymbolSerializable = FactorySymbolSerializable.CreateDrawnLineSymbolSerializable(drawnLineSymbolModel);

                drawnLinesSymbolSerializable.Add(drawnLineSymbolSerializable);
            }
        }

        var projectSerializable = new ProjectSerializable()
        {
            IsScaleAllSymbolVM = _scaleAllSymbolVM.IsScaleAllSymbolVM,
            IsDrawingLinesAccordingGOST = _checkBoxLineGostVM.IsDrawingLinesAccordingGOST,
            BlocksSymbolSerializable = blocksSymbolSerializable,
            DrawnLinesSymbolSerializable = drawnLinesSymbolSerializable,
        };

        SerializationProject.Write(projectSerializable, filePath);
    }

    public async void Load(string filePath)
    {
        var loadedProject = await SerializationProject.Read(filePath);

        _scaleAllSymbolVM.IsScaleAllSymbolVM = loadedProject.IsScaleAllSymbolVM;
        _checkBoxLineGostVM.IsDrawingLinesAccordingGOST = loadedProject.IsDrawingLinesAccordingGOST;

        LoadBlocksSymbols(loadedProject);
        LoadDrawnLinesSymbol(loadedProject);
    }

    private void LoadBlocksSymbols(ProjectSerializable projectSerializable)
    {
        _canvasSymbolsVM.SymbolsVM.Clear();
        _blockSymbolsVMById.Clear();

        var blocksSymbolSerializable = projectSerializable.BlocksSymbolSerializable;

        foreach (var blockSymbolSerializable in blocksSymbolSerializable)
        {
            var blockSymbolVM = _factoryBlockSymbolVM.CreateBySerialization(blockSymbolSerializable);

            if (blockSymbolSerializable.Id != null)
            {
                _blockSymbolsVMById.Add(blockSymbolSerializable.Id, blockSymbolVM);
            }

            _canvasSymbolsVM.SymbolsVM.Add(blockSymbolVM);
        }
    }

    private static List<LineSymbolModel> LoadLinesSymbol(DrawnLineSymbolSerializable drawnLineSymbolSerializable)
    {
        var linesSymbolModel = new List<LineSymbolModel>();

        var linesSymbolSerializable = drawnLineSymbolSerializable.LinesSymbolSerializable;

        if (linesSymbolSerializable != null)
        {
            foreach (var lineSymbolSerializable in linesSymbolSerializable)
            {
                var lineSymbolModel = FactorySymbolSerializable.CreateLineSymbolModel(lineSymbolSerializable);

                linesSymbolModel.Add(lineSymbolModel);
            }
        }

        return linesSymbolModel;
    }

    private void LoadDrawnLinesSymbol(ProjectSerializable projectSerializable)
    {
        _canvasSymbolsVM.BlockByDrawnLines.Clear();

        var drawnLinesSymbolSerializable = projectSerializable.DrawnLinesSymbolSerializable;

        foreach (var drawnLineSymbolSerializable in drawnLinesSymbolSerializable)
        {
            var linesSymbolModel = LoadLinesSymbol(drawnLineSymbolSerializable);

            var symbolOutgoingLine = drawnLineSymbolSerializable.SymbolOutgoingLine;
            var symbolIncomingLine = drawnLineSymbolSerializable.SymbolIncomingLine;

            if (symbolOutgoingLine == null)
            {
                continue;
            }

            if (symbolOutgoingLine.Id == null)
            {
                continue;
            }

            if (symbolIncomingLine == null)
            {
                continue;
            }

            if (symbolIncomingLine.Id == null)
            {
                continue;
            }

            var symbolOutgoingLineVM = _blockSymbolsVMById[symbolOutgoingLine.Id];
            var symbolIncomingLineVM = _blockSymbolsVMById[symbolIncomingLine.Id];

            var outgoingConnectionPoint = symbolOutgoingLineVM.GetConnectionPoint(drawnLineSymbolSerializable.OutgoingPosition);
            var incomingConnectionPoint = symbolIncomingLineVM.GetConnectionPoint(drawnLineSymbolSerializable.IncomingPosition);

            outgoingConnectionPoint.IsHasConnectingLine = true;
            incomingConnectionPoint.IsHasConnectingLine = true;

            var drawnLineSymbolVM = new DrawnLineSymbolVM(_canvasSymbolsVM, linesSymbolModel)
            {
                SymbolOutgoingLine = symbolOutgoingLineVM,
                SymbolIncomingLine = symbolIncomingLineVM,
                OutgoingConnectionPoint = outgoingConnectionPoint,
                IncomingConnectionPoint = incomingConnectionPoint,
                OutgoingPosition = outgoingConnectionPoint.Position,
                IncomingPosition = incomingConnectionPoint.Position,
                Text = drawnLineSymbolSerializable.Text,
                Color = drawnLineSymbolSerializable.Color,
            };

            drawnLineSymbolVM.RedrawAllLines();
            
            if (!_canvasSymbolsVM.BlockByDrawnLines.ContainsKey(symbolOutgoingLineVM))
            {
                var drawnsLineSymbolVM = new List<DrawnLineSymbolVM>
                {
                    drawnLineSymbolVM   
                };

                _canvasSymbolsVM.BlockByDrawnLines.Add(symbolOutgoingLineVM, drawnsLineSymbolVM);
            }
            else
            {
                _canvasSymbolsVM.BlockByDrawnLines[symbolOutgoingLineVM].Add(drawnLineSymbolVM);
            }

            if (!_canvasSymbolsVM.BlockByDrawnLines.ContainsKey(symbolIncomingLineVM))
            {
                var drawnsLineSymbolVM = new List<DrawnLineSymbolVM>
                {
                    drawnLineSymbolVM
                };

                _canvasSymbolsVM.BlockByDrawnLines.Add(symbolIncomingLineVM, drawnsLineSymbolVM);
            }
            else
            {
                _canvasSymbolsVM.BlockByDrawnLines[symbolIncomingLineVM].Add(drawnLineSymbolVM);
            }

            _canvasSymbolsVM.SymbolsVM.Add(drawnLineSymbolVM);
        }
    }
}