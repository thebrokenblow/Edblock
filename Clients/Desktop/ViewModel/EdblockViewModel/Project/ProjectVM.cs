using System.Collections.Generic;
using EdblockViewModel.Symbols.ComponentsParallelActionSymbolVM;
using EdblockModel.SymbolsModel;
using EdblockModel.SymbolsModel.LineSymbolsModel;
using EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;
using Edblock.SymbolsSerialization;
using Edblock.SymbolsSerialization.Symbols;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Project.Interfaces;

namespace EdblockViewModel.Project;

public class ProjectVM(
    ICanvasSymbolsComponentVM canvasSymbolsComponentVM,
    IScaleAllSymbolComponentVM scaleAllSymbolComponentVM,
    ILineStateStandardComponentVM lineStateStandardComponentVM,
    IFactoryBlockSymbolVM factoryBlockSymbolVM) : IProjectVM
{
    private readonly SerializationProject serializationProject = new();
    private readonly Dictionary<string, BlockSymbolVM> blockSymbolsVMById = [];

    public void SaveProject(string filePath)
    {
        var blockSymbolsSerializable = new List<BlockSymbolSerializable>();
        var drawnLinesSymbolSerializable = new List<DrawnLineSymbolSerializable>();
        var parallelActionSymbolsSerializable = new List<ParallelActionSymbolSerializable>();
        var switchCaseSymbolsSerializable = new List<SwitchCaseSymbolsSerializable>();

        var blockSymbolsVM = canvasSymbolsComponentVM.ListCanvasSymbolsComponentVM.BlockSymbolsVM;

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

        var projectSerializable = new ProjectSerializable()
        {
            WidthCanvas = canvasSymbolsComponentVM.Width,
            HeightCanvas = canvasSymbolsComponentVM.Height,
            IsScaleAllSymbolVM = scaleAllSymbolComponentVM.IsScaleAllSymbol,
            IsDrawingLinesAccordingGOST = lineStateStandardComponentVM.IsDrawingLinesAccordingGOST,
            BlockSymbolsSerializable = blockSymbolsSerializable,
            DrawnLinesSymbolSerializable = drawnLinesSymbolSerializable,
            SwitchCaseSymbolsSerializable = switchCaseSymbolsSerializable,
            ParallelActionSymbolsSerializable = parallelActionSymbolsSerializable,
        };

        serializationProject.Write(projectSerializable, filePath);
    }

    public async void LoadProject(string filePath)
    {
        //Начало вращение спиннера
        var loadedProject = await serializationProject.Read(filePath);
        //Конец вращение спиннера

        canvasSymbolsComponentVM.Width = loadedProject.WidthCanvas;
        canvasSymbolsComponentVM.Height = loadedProject.HeightCanvas;
        scaleAllSymbolComponentVM.IsScaleAllSymbol = loadedProject.IsScaleAllSymbolVM;
        lineStateStandardComponentVM.IsDrawingLinesAccordingGOST = loadedProject.IsDrawingLinesAccordingGOST;

        LoadBlocksSymbols(loadedProject);
    }

    private void LoadBlocksSymbols(ProjectSerializable projectSerializable)
    {
        blockSymbolsVMById.Clear();
        canvasSymbolsComponentVM.ListCanvasSymbolsComponentVM.BlockSymbolsVM.Clear();

        var blockSymbolsSerializable = projectSerializable.BlockSymbolsSerializable;

        if (blockSymbolsSerializable is not null)
        {
            foreach (var blockSymbolSerializable in blockSymbolsSerializable)
            {
                var blockSymbolVM = factoryBlockSymbolVM.CreateBlockSymbolVM(blockSymbolSerializable);

                canvasSymbolsComponentVM.ListCanvasSymbolsComponentVM.LoadBlockSymbol(blockSymbolVM);
                blockSymbolsVMById.Add(blockSymbolSerializable.Id, blockSymbolVM);
            }
        }

        var switchCaseSymbolsSerializable = projectSerializable.SwitchCaseSymbolsSerializable;

        if (switchCaseSymbolsSerializable is not null)
        {
            foreach (var switchCaseSymbolSerializable in switchCaseSymbolsSerializable)
            {
                var switchCaseSymbolVM = factoryBlockSymbolVM.CreateBlockSymbolVM(switchCaseSymbolSerializable);

                canvasSymbolsComponentVM.ListCanvasSymbolsComponentVM.LoadBlockSymbol(switchCaseSymbolVM);
                blockSymbolsVMById.Add(switchCaseSymbolSerializable.Id, switchCaseSymbolVM);
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
}