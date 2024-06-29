using EdblockViewModel.Symbols.Abstractions;
using System.Collections.Generic;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles.Interfaces;

public interface IBuilderScaleRectangles
{
    public ScalableBlockSymbolVM ScalableBlockSymbolVM { get; set; }
    public IBuilderScaleRectangles AddMiddleTopRectangle();
    public IBuilderScaleRectangles AddRightTopRectangle();
    public IBuilderScaleRectangles AddRightMiddleRectangle();
    public IBuilderScaleRectangles AddRightBottomRectangle();
    public IBuilderScaleRectangles AddMiddleBottomRectangle();
    public IBuilderScaleRectangles AddLeftBottomRectangle();
    public IBuilderScaleRectangles AddLeftMiddleRectangle();
    public IBuilderScaleRectangles AddLeftTopRectangle();
    public List<ScaleRectangle> Build();
}