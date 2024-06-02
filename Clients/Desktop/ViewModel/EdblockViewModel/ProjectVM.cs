using System.Collections.Generic;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.ComponentsParallelActionSymbolVM;
using EdblockModel.SymbolsModel;
using EdblockViewModel.Symbols;
using EdblockModel.SymbolsModel.LineSymbolsModel;
using EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using EdblockViewModel.PagesVM;
using Edblock.SymbolsSerialization;
using Edblock.SymbolsSerialization.Symbols;
using EdblockViewModel.ComponentsVM.CanvasSymbols;

namespace EdblockViewModel;

internal class ProjectVM(EditorVM editorVM)
{
    private readonly CanvasSymbolsVM canvasSymbolsVM = editorVM.CanvasSymbolsVM;
    private readonly ScaleAllSymbolVM scaleAllSymbolVM = editorVM.PopupBoxMenuVM.ScaleAllSymbolVM;
    private readonly LineStateStandardVM checkBoxLineGostVM = editorVM.PopupBoxMenuVM.CheckBoxLineGostVM;
    private readonly SerializationProject serializationProject = new();
    private readonly FactoryBlockSymbolVM factoryBlockSymbolVM = new(editorVM);
    private readonly Dictionary<string, BlockSymbolVM> blockSymbolsVMById = [];

    public void Save(string filePath)
    {
        var blockSymbolsSerializable = new List<BlockSymbolSerializable>();
        var drawnLinesSymbolSerializable = new List<DrawnLineSymbolSerializable>();
        var parallelActionSymbolsSerializable = new List<ParallelActionSymbolSerializable>();
        var switchCaseSymbolsSerializable = new List<SwitchCaseSymbolsSerializable>();

        var blockSymbolsVM = canvasSymbolsVM.ListCanvasSymbolsVM.BlockSymbolsVM;

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
            IsScaleAllSymbolVM = scaleAllSymbolVM.IsScaleAllSymbol,
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

        scaleAllSymbolVM.IsScaleAllSymbol = loadedProject.IsScaleAllSymbolVM;
        checkBoxLineGostVM.IsDrawingLinesAccordingGOST = loadedProject.IsDrawingLinesAccordingGOST;

        LoadBlocksSymbols(loadedProject);
    }

    private void LoadBlocksSymbols(ProjectSerializable projectSerializable)
    {
        blockSymbolsVMById.Clear();
        canvasSymbolsVM.ListCanvasSymbolsVM.BlockSymbolsVM.Clear();

        var blockSymbolsSerializable = projectSerializable.BlockSymbolsSerializable;

        if (blockSymbolsSerializable is not null)
        {
            foreach (var blockSymbolSerializable in blockSymbolsSerializable)
            {
                var blockSymbolVM = factoryBlockSymbolVM.CreateBlockSymbolVM(blockSymbolSerializable);

                canvasSymbolsVM.ListCanvasSymbolsVM.AddBlockSymbol(blockSymbolVM);
                blockSymbolsVMById.Add(blockSymbolSerializable.Id, blockSymbolVM);
            }
        }

        var switchCaseSymbolsSerializable = projectSerializable.SwitchCaseSymbolsSerializable;

        if (switchCaseSymbolsSerializable is not null)
        {
            foreach (var switchCaseSymbolSerializable in switchCaseSymbolsSerializable)
            {
                var switchCaseSymbolVM = factoryBlockSymbolVM.CreateBlockSymbolVM(switchCaseSymbolSerializable);

                canvasSymbolsVM.ListCanvasSymbolsVM.AddBlockSymbol(switchCaseSymbolVM);
                blockSymbolsVMById.Add(switchCaseSymbolSerializable.Id, switchCaseSymbolVM);
            }
        }

        var parallelActionSymbolsSerializable = projectSerializable.ParallelActionSymbolsSerializable;

        if (parallelActionSymbolsSerializable is not null)
        {
            foreach (var parallelActionSymbolSerializable in parallelActionSymbolsSerializable)
            {
                var parallelActionSymbolVM = factoryBlockSymbolVM.CreateBlockSymbolVM(parallelActionSymbolSerializable);

                canvasSymbolsVM.ListCanvasSymbolsVM.AddBlockSymbol(parallelActionSymbolVM);
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
}