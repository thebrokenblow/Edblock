using EdblockModel.Enum;

namespace EdblockModel.Symbols;

public class ConnectionPointModel //TODO: Этого класса здесь быть не должно, эта логика должна быть в модели
{
    private readonly SideSymbol _position;

    public ConnectionPointModel(SideSymbol position)
    {
        _position = position;
    }

    public bool IsLineOutputAccordingGOST()
    {
        if (_position == SideSymbol.Bottom || _position == SideSymbol.Right)
        {
            return true;
        }

        return false;
    }

    public bool IsLineIncomingAccordingGOST()
    {
        if (_position == SideSymbol.Top || _position == SideSymbol.Left)
        {
            return true;
        }

        return false;
    }
}