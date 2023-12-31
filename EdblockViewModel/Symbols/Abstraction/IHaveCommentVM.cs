using EdblockViewModel.Symbols.CommentSymbolVMComponents;

namespace EdblockViewModel.Symbols.Abstraction;

public interface IHaveCommentVM
{
    public CommentSymbolVM? CommentSymbolVM { get; set; }
}
