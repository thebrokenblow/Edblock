using SerializationEdblock;
using System.Collections.Generic;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.LineSymbols;
using SerializationEdblock.SymbolsSerializable;
using EdblockViewModel.Symbols.ComponentsParallelActionSymbolVM;
using EdblockModel.SymbolsModel;
using EdblockViewModel.Symbols;
using EdblockModel.SymbolsModel.LineSymbolsModel;
using EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;

namespace EdblockViewModel;

internal class ProjectVM
{
    private readonly CanvasSymbolsVM canvasSymbolsVM;
    private readonly ScaleAllSymbolVM scaleAllSymbolVM;
    private readonly CheckBoxLineGostVM checkBoxLineGostVM;
    private readonly SerializationProject serializationProject = new();
    private readonly FactoryBlockSymbolVM factoryBlockSymbolVM;
    private readonly Dictionary<string, BlockSymbolVM> blockSymbolsVMById = new();
    public ProjectVM(EdblockVM edblockVM)
    {
        canvasSymbolsVM = edblockVM.CanvasSymbolsVM;
        scaleAllSymbolVM = edblockVM.PopupBoxMenuVM.ScaleAllSymbolVM;
        checkBoxLineGostVM = edblockVM.PopupBoxMenuVM.CheckBoxLineGostVM;

        factoryBlockSymbolVM = new(edblockVM);
    }

    public void Save(string filePath)
    {
        var blockSymbolsSerializable = new List<BlockSymbolSerializable>();
        var drawnLinesSymbolSerializable = new List<DrawnLineSymbolSerializable>();
        var parallelActionSymbolsSerializable = new List<ParallelActionSymbolSerializable>();
        var switchCaseSymbolsSerializable = new List<SwitchCaseSymbolsSerializable>();

        var blockSymbolsVM = canvasSymbolsVM.BlockSymbolVM;

        foreach (var blockSymbolVM in blockSymbolsVM)
        {
            if (blockSymbolVM is SwitchCaseSymbolVM switchCaseSymbolVM)
            {
                var switchCaseSymbolModel = (SwitchCaseSymbolModel)switchCaseSymbolVM.BlockSymbolModel;
                var switchCaseSymbolSerializable = FactorySymbolSerializable.Create(switchCaseSymbolModel);

                switchCaseSymbolsSerializable.Add(switchCaseSymbolSerializable);
            }

            if (blockSymbolVM is ParallelActionSymbolVM parallelActionSymbolVM)
            {
                var blockSymbolModel = (ParallelActionSymbolModel)parallelActionSymbolVM.BlockSymbolModel;
                var blockSymbolSerializable = FactorySymbolSerializable.Create(blockSymbolModel);

                parallelActionSymbolsSerializable.Add(blockSymbolSerializable);
            }

            if (blockSymbolVM is BlockSymbolVM and not ParallelActionSymbolVM and not SwitchCaseSymbolVM)
            {
                var blockSymbolModel = blockSymbolVM.BlockSymbolModel;
                var blockSymbolSerializable = FactorySymbolSerializable.Create(blockSymbolModel);

                blockSymbolsSerializable.Add(blockSymbolSerializable);
            }
        }

        var drawnLinesSymbolsVM = canvasSymbolsVM.DrawnLinesSymbolVM;

        foreach(var drawnLineSymbolVM in drawnLinesSymbolsVM)
        {
            var drawnLineSymbolModel = drawnLineSymbolVM.DrawnLineSymbolModel;
            var drawnLineSymbolSerializable = FactorySymbolSerializable.CreateDrawnLineSymbolSerializable(drawnLineSymbolModel);

            drawnLinesSymbolSerializable.Add(drawnLineSymbolSerializable);
        }

        var projectSerializable = new ProjectSerializable()
        {
            IsScaleAllSymbolVM = scaleAllSymbolVM.IsScaleAllSymbolVM,
            IsDrawingLinesAccordingGOST = checkBoxLineGostVM.IsDrawingLinesAccordingGOST,
            BlockSymbolsSerializable = blockSymbolsSerializable,
            DrawnLinesSymbolSerializable = drawnLinesSymbolSerializable,
            SwitchCaseSymbolsSerializable = switchCaseSymbolsSerializable,
            ParallelActionSymbolsSerializable = parallelActionSymbolsSerializable,
        };

        serializationProject.Write(projectSerializable, filePath);
    }

    public async void Load(string filePath)
    {
        var loadedProject = await serializationProject.Read(filePath);

        scaleAllSymbolVM.IsScaleAllSymbolVM = loadedProject.IsScaleAllSymbolVM;
        checkBoxLineGostVM.IsDrawingLinesAccordingGOST = loadedProject.IsDrawingLinesAccordingGOST;

        LoadBlocksSymbols(loadedProject);
        LoadDrawnLinesSymbol(loadedProject);
    }

    private void LoadBlocksSymbols(ProjectSerializable projectSerializable)
    {
        blockSymbolsVMById.Clear();
        canvasSymbolsVM.BlockSymbolVM.Clear();

        var blockSymbolsSerializable = projectSerializable.BlockSymbolsSerializable;

        if (blockSymbolsSerializable is not null)
        {
            foreach (var blockSymbolSerializable in blockSymbolsSerializable)
            {
                var blockSymbolVM = factoryBlockSymbolVM.CreateBlockSymbolVM(blockSymbolSerializable);

                canvasSymbolsVM.BlockSymbolVM.Add(blockSymbolVM);
                blockSymbolsVMById.Add(blockSymbolSerializable.Id, blockSymbolVM);
            }
        }

        var switchCaseSymbolsSerializable = projectSerializable.SwitchCaseSymbolsSerializable;

        if (switchCaseSymbolsSerializable is not null)
        {
            foreach (var switchCaseSymbolSerializable in switchCaseSymbolsSerializable)
            {
                var switchCaseSymbolVM = factoryBlockSymbolVM.CreateBlockSymbolVM(switchCaseSymbolSerializable);

                canvasSymbolsVM.BlockSymbolVM.Add(switchCaseSymbolVM);
                blockSymbolsVMById.Add(switchCaseSymbolSerializable.Id, switchCaseSymbolVM);
            }
        }

        var parallelActionSymbolsSerializable = projectSerializable.ParallelActionSymbolsSerializable;

        if (parallelActionSymbolsSerializable is not null)
        {
            foreach (var parallelActionSymbolSerializable in parallelActionSymbolsSerializable)
            {
                var parallelActionSymbolVM = factoryBlockSymbolVM.CreateBlockSymbolVM(parallelActionSymbolSerializable);

                canvasSymbolsVM.BlockSymbolVM.Add(parallelActionSymbolVM);
                blockSymbolsVMById.Add(parallelActionSymbolSerializable.Id, parallelActionSymbolVM);
            }
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
        canvasSymbolsVM.BlockByDrawnLines.Clear();

        var drawnLinesSymbolSerializable = projectSerializable.DrawnLinesSymbolSerializable;

        if (drawnLinesSymbolSerializable is null)
        {
            return;
        }

        foreach (var drawnLineSymbolSerializable in drawnLinesSymbolSerializable)
        {
            var linesSymbolModel = LoadLinesSymbol(drawnLineSymbolSerializable);

            var symbolOutgoingLine = drawnLineSymbolSerializable.SymbolOutgoingLine;
            var symbolIncomingLine = drawnLineSymbolSerializable.SymbolIncomingLine;

            if (symbolOutgoingLine == null)
            {
                continue;
            }

            if (symbolIncomingLine == null)
            {
                continue;
            }

            var symbolOutgoingLineVM = blockSymbolsVMById[symbolOutgoingLine.Id];
            var symbolIncomingLineVM = blockSymbolsVMById[symbolIncomingLine.Id];

            var outgoingConnectionPoint = symbolOutgoingLineVM.GetConnectionPoint(drawnLineSymbolSerializable.OutgoingPosition);
            var incomingConnectionPoint = symbolIncomingLineVM.GetConnectionPoint(drawnLineSymbolSerializable.IncomingPosition);

            outgoingConnectionPoint.IsHasConnectingLine = true;
            incomingConnectionPoint.IsHasConnectingLine = true;

            var drawnLineSymbolVM = new DrawnLineSymbolVM(canvasSymbolsVM, linesSymbolModel)
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

            if (!canvasSymbolsVM.BlockByDrawnLines.ContainsKey(symbolOutgoingLineVM))
            {
                var drawnsLineSymbolVM = new List<DrawnLineSymbolVM>
                {
                    drawnLineSymbolVM
                };

                canvasSymbolsVM.BlockByDrawnLines.Add(symbolOutgoingLineVM, drawnsLineSymbolVM);
            }
            else
            {
                canvasSymbolsVM.BlockByDrawnLines[symbolOutgoingLineVM].Add(drawnLineSymbolVM);
            }

            if (!canvasSymbolsVM.BlockByDrawnLines.ContainsKey(symbolIncomingLineVM))
            {
                var drawnsLineSymbolVM = new List<DrawnLineSymbolVM>
                {
                    drawnLineSymbolVM
                };

                canvasSymbolsVM.BlockByDrawnLines.Add(symbolIncomingLineVM, drawnsLineSymbolVM);
            }
            else
            {
                canvasSymbolsVM.BlockByDrawnLines[symbolIncomingLineVM].Add(drawnLineSymbolVM);
            }

            canvasSymbolsVM.DrawnLinesSymbolVM.Add(drawnLineSymbolVM);
        }
    }
}