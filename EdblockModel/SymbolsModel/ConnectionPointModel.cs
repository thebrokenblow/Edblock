using EdblockModel.SymbolsModel.Enum;

namespace EdblockModel.Symbols;

public class ConnectionPointModel
{
    private readonly PositionConnectionPoint _position;

    public ConnectionPointModel(PositionConnectionPoint position)
    {
        _position = position;
    }

    public bool IsLineOutputAccordingGOST()
    {
        if (_position == PositionConnectionPoint.Bottom || _position == PositionConnectionPoint.Right)
        {
            return true;
        }

        return false;
    }

    public bool IsLineIncomingAccordingGOST()
    {
        if (_position == PositionConnectionPoint.Top || _position == PositionConnectionPoint.Left)
        {
            return true;
        }

        return false;
    }
}