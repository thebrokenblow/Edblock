using System;
using System.Linq;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using EdblockViewModel.Symbols;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Core;
using EdblockViewModel.Symbols.ComponentsCommentSymbolVM;
using EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;
using EdblockViewModel.Symbols.ComponentsParallelActionSymbolVM;
using EdblockViewModel.Components.ListSymbols.Interfaces;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;
using Prism.Commands;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles.Interfaces;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

namespace EdblockViewModel.Components.ListSymbols;

public class ListSymbolsComponentVM : ObservableObject, IListSymbolsComponentVM, INotifyDataErrorInfo
{
    private const int minLinesHorizontalCondition = 2;
    private const int maxLinesHorizontalCondition = 20;

    private const int defaultLinesHorizontalCondition = 3;
    private int? correctLinesHorizontalCondition = defaultLinesHorizontalCondition;
    private string linesHorizontalCondition = defaultLinesHorizontalCondition.ToString();
    public string LinesHorizontalCondition
    {
        get => linesHorizontalCondition;
        set
        {
            linesHorizontalCondition = value;

            errorsViewModel.ClearErrors(nameof(LinesHorizontalCondition));

            if (int.TryParse(linesHorizontalCondition.ToString(), out int validateLinesHorizontalCondition))
            {
                correctLinesHorizontalCondition = validateLinesHorizontalCondition;
            }
            else
            {
                errorsViewModel.AddError(nameof(LinesHorizontalCondition), $"Вы ввели не число");
                correctLinesHorizontalCondition = null;
                return;
            }

            if (correctLinesHorizontalCondition > maxLinesHorizontalCondition)
            {
                errorsViewModel.AddError(nameof(LinesHorizontalCondition), $"Количество линий не должно быть больше { maxLinesHorizontalCondition }");
            }

            if (correctLinesHorizontalCondition < minLinesHorizontalCondition)
            {
                errorsViewModel.AddError(nameof(LinesHorizontalCondition), $"Количество линий должно быть не меньше { minLinesHorizontalCondition }");
            }
        }
    }

    private const int minLinesVerticalCondition = 2;
    private const int maxLinesVerticalCondition = 20;

    private const int defaultLinesVerticalCondition = 3;
    private int? correctLinesVerticalCondition = defaultLinesVerticalCondition;
    private string linesVerticalCondition = defaultLinesVerticalCondition.ToString();
    public string LinesVerticalCondition
    {
        get => linesVerticalCondition;
        set
        {
            linesVerticalCondition = value;

            errorsViewModel.ClearErrors(nameof(LinesVerticalCondition));

            if (int.TryParse(linesVerticalCondition.ToString(), out int validateLinesVerticalCondition))
            {
                correctLinesVerticalCondition = validateLinesVerticalCondition;
            }
            else
            {
                errorsViewModel.AddError(nameof(LinesVerticalCondition), $"Вы ввели не число");
                correctLinesVerticalCondition = null;
                return;
            }

            if (correctLinesVerticalCondition > maxLinesVerticalCondition)
            {
                errorsViewModel.AddError(nameof(LinesVerticalCondition), $"Количество линий не должно быть больше { maxLinesHorizontalCondition }");
            }

            if (correctLinesVerticalCondition < minLinesVerticalCondition)
            {
                errorsViewModel.AddError(nameof(LinesVerticalCondition), $"Количество линий должно быть не меньше { minLinesVerticalCondition }");
            }
        }
    }

    private const int minSymbolsIncomingParallel = 1;
    private const int maxSymbolsIncomingParallel = 20;
    private const int defaultSymbolsIncomingParallel = 1;
    private int? correctSymbolsIncomingParallel = defaultSymbolsIncomingParallel;
    private string symbolsIncomingParallel = defaultSymbolsIncomingParallel.ToString();
    public string SymbolsIncomingParallel
    {
        get => symbolsIncomingParallel;
        set
        {
            symbolsIncomingParallel = value;

            errorsViewModel.ClearErrors(nameof(SymbolsIncomingParallel));

            if (int.TryParse(symbolsIncomingParallel.ToString(), out int validateSymbolsIncomingParallel))
            {
                correctSymbolsIncomingParallel = validateSymbolsIncomingParallel;
            }
            else
            {
                errorsViewModel.AddError(nameof(SymbolsIncomingParallel), $"Вы ввели не число");
                correctSymbolsIncomingParallel = null;
                return;
            }

            if (correctSymbolsIncomingParallel > maxSymbolsIncomingParallel)
            {
                errorsViewModel.AddError(nameof(SymbolsIncomingParallel), $"Количество входов линий не должно быть больше { maxSymbolsIncomingParallel }");
            }

            if (correctSymbolsIncomingParallel < minSymbolsIncomingParallel)
            {
                errorsViewModel.AddError(nameof(SymbolsIncomingParallel), $"Количество входов линий не должно быть меньше { minSymbolsIncomingParallel }");
            }
        }
    }

    private const int minSymbolsOutgoingParallel = 1;
    private const int maxSymbolsOutgoingParallel = 20;
    private const int defaultSymbolsOutgoingParallel = 1;
    private int? correctSymbolsOutgoingParallel = defaultSymbolsOutgoingParallel;
    private string symbolsOutgoingParallel = defaultSymbolsOutgoingParallel.ToString();
    public string SymbolsOutgoingParallel
    {
        get => symbolsOutgoingParallel;
        set
        {
            symbolsOutgoingParallel = value;

            errorsViewModel.ClearErrors(nameof(SymbolsOutgoingParallel));

            if (int.TryParse(symbolsOutgoingParallel.ToString(), out int validateSymbolsOutgoingParallel))
            {
                correctSymbolsOutgoingParallel = validateSymbolsOutgoingParallel;
            }
            else
            {
                errorsViewModel.AddError(nameof(SymbolsOutgoingParallel), $"Вы ввели не число");
                correctSymbolsOutgoingParallel = null;
                return;
            }

            if (correctSymbolsOutgoingParallel > maxSymbolsOutgoingParallel)
            {
                errorsViewModel.AddError(nameof(SymbolsOutgoingParallel), $"Количество выходов линий не должно быть больше { maxSymbolsIncomingParallel }");
            }

            if (correctSymbolsOutgoingParallel < minSymbolsOutgoingParallel)
            {
                errorsViewModel.AddError(nameof(SymbolsOutgoingParallel), $"Количество выходов линий не должно быть меньше { minSymbolsIncomingParallel }");
            }
        }
    }

    public DelegateCommand<string> CreateBlockSymbolCommand { get; }
    public DelegateCommand CreateHorizontalConditionSymbolCommand { get; }
    public DelegateCommand CreateVerticalConditionSymbolCommand { get; }
    public DelegateCommand CreateParallelActionSymbolCommand { get; }


    private readonly Func<Type, BlockSymbolVM> _factoryBlockSymbolVM;

    private readonly Dictionary<string, Type> nameByTypeBlockSymbol = new()
    {
        { "ActionSymbolVM", typeof(ActionSymbolVM) },
        { "ConditionSymbolVM", typeof(ConditionSymbolVM) },
        { "CycleForSymbolVM", typeof(CycleForSymbolVM) },
        { "CycleWhileBeginSymbolVM", typeof(CycleWhileBeginSymbolVM) },
        { "CycleWhileEndSymbolVM", typeof(CycleWhileEndSymbolVM) },
        { "InputOutputSymbolVM", typeof(InputOutputSymbolVM) },
        { "LinkSymbolVM", typeof(LinkSymbolVM) },
        { "StartEndSymbolVM", typeof(StartEndSymbolVM) },
        { "SubroutineSymbolVM", typeof(SubroutineSymbolVM) },
        { "CommentSymbolVM", typeof(CommentSymbolVM) },
    };

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
    public bool HasErrors => errorsViewModel.HasErrors;

    private readonly ErrorsViewModel errorsViewModel = new();

    private readonly IBuilderScaleRectangles _builderScaleRectangles;
    private readonly ICanvasSymbolsComponentVM _canvasSymbolsComponentVM;
    private readonly IListCanvasSymbolsComponentVM _listCanvasSymbolsComponentVM;
    private readonly ITopSettingsMenuComponentVM _topSettingsMenuComponentVM;
    private readonly IPopupBoxMenuComponentVM _popupBoxMenuComponentVM;
    public ListSymbolsComponentVM(
        IBuilderScaleRectangles builderScaleRectangles,
        ICanvasSymbolsComponentVM canvasSymbolsComponentVM,
        IListCanvasSymbolsComponentVM listCanvasSymbolsComponentVM,
        ITopSettingsMenuComponentVM topSettingsMenuComponentVM,
        IPopupBoxMenuComponentVM popupBoxMenuComponentVM, 
        Func<Type, BlockSymbolVM> factoryBlockSymbol)
    {
        _builderScaleRectangles = builderScaleRectangles;
        _canvasSymbolsComponentVM = canvasSymbolsComponentVM;
        _listCanvasSymbolsComponentVM = listCanvasSymbolsComponentVM;
        _topSettingsMenuComponentVM = topSettingsMenuComponentVM;
        _popupBoxMenuComponentVM = popupBoxMenuComponentVM;

        _factoryBlockSymbolVM = factoryBlockSymbol;

        CreateBlockSymbolCommand = new(CreateBlockSymbol);
        CreateHorizontalConditionSymbolCommand = new(CreateHorizontalConditionSymbol);
        CreateVerticalConditionSymbolCommand = new(CreateVerticalConditionSymbol);
        CreateParallelActionSymbolCommand = new(CreateParallelActionSymbol);
    }

    public void CreateBlockSymbol(string nameBlockSymbol)
    {
        var typeBlockSymbol = nameByTypeBlockSymbol[nameBlockSymbol];
        var blockSymbolVM = _factoryBlockSymbolVM.Invoke(typeBlockSymbol);

        _listCanvasSymbolsComponentVM.AddBlockSymbol(blockSymbolVM);
    }

    private void CreateHorizontalConditionSymbol()
    {
        if (correctLinesHorizontalCondition is null)
        {
            return;
        }

        if (correctLinesHorizontalCondition > maxLinesHorizontalCondition || 
            correctLinesHorizontalCondition < minLinesHorizontalCondition)
        {
            return;
        }

        var builderScaleRectangles = new BuilderScaleRectangles(_canvasSymbolsComponentVM, _popupBoxMenuComponentVM.ScaleAllSymbolComponentVM);
        var horizontalConditionSymbol = new HorizontalConditionSymbolVM(
            builderScaleRectangles,
            _canvasSymbolsComponentVM,
            _listCanvasSymbolsComponentVM,
            _topSettingsMenuComponentVM,
            _popupBoxMenuComponentVM,
            correctLinesHorizontalCondition.Value);

        _listCanvasSymbolsComponentVM.AddBlockSymbol(horizontalConditionSymbol);
    }

    private void CreateVerticalConditionSymbol()
    {
        if (correctLinesVerticalCondition is null)
        {
            return;
        }

        if (correctLinesVerticalCondition > maxLinesVerticalCondition || 
            correctLinesVerticalCondition < minLinesHorizontalCondition)
        {
            return;
        }

        var verticalConditionSymbolVM = new VerticalConditionSymbolVM(
            _builderScaleRectangles,
            _canvasSymbolsComponentVM,
            _listCanvasSymbolsComponentVM,
            _topSettingsMenuComponentVM,
            _popupBoxMenuComponentVM,
            correctLinesVerticalCondition.Value);

        _listCanvasSymbolsComponentVM.AddBlockSymbol(verticalConditionSymbolVM);
    }

    private void CreateParallelActionSymbol()
    {
        if (correctSymbolsIncomingParallel is null || 
            correctSymbolsOutgoingParallel is null)
        {
            return;    
        }

        if (correctSymbolsIncomingParallel > maxSymbolsIncomingParallel || 
            correctSymbolsIncomingParallel < minSymbolsIncomingParallel)
        {
            return;
        }

        if (correctSymbolsOutgoingParallel > maxSymbolsOutgoingParallel || 
            correctSymbolsOutgoingParallel < minSymbolsOutgoingParallel)
        {
            return;
        }

        var parallelActionSymbolVM = new ParallelActionSymbolVM(
            _canvasSymbolsComponentVM,
            _listCanvasSymbolsComponentVM,
            _topSettingsMenuComponentVM,
            _popupBoxMenuComponentVM,
            correctSymbolsIncomingParallel.Value,
            correctSymbolsOutgoingParallel.Value);

        _listCanvasSymbolsComponentVM.AddBlockSymbol(parallelActionSymbolVM);
    }

    public IEnumerable GetErrors(string? propertyName)
    {
        if (propertyName is null)
        {
            return Enumerable.Empty<string>();
        }

        return errorsViewModel.GetErrors(propertyName);
    }
}