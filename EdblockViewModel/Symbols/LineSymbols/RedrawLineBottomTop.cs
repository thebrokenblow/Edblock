using System;
using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.Symbols.LineSymbols;

internal class RedrawLineBottomTop
{
    private readonly BlockSymbol? _symbolaIncomingLine;
    private readonly BlockSymbol? _symbolOutgoingLine;
    private readonly DrawnLineSymbolVM _drawnLineSymbolVM;
    private readonly PositionConnectionPoint _positionOutgoing;
    private readonly PositionConnectionPoint _positionIncoming;
    private const int baseLineOffset = 20;

    public RedrawLineBottomTop(DrawnLineSymbolVM drawnLineSymbolVM)
    {
        _drawnLineSymbolVM = drawnLineSymbolVM;
        _positionOutgoing = drawnLineSymbolVM.PositionOutgoingConnectionPoint;
        _positionIncoming = drawnLineSymbolVM.PositionIncomingConnectionPoint;
        _symbolaIncomingLine = drawnLineSymbolVM.SymbolaIncomingLine;
        _symbolOutgoingLine = drawnLineSymbolVM.SymbolOutgoingLine;
    }

    public interface ICoordinateDecorator
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class InversionXCoordinateDecorator : ICoordinateDecorator
    {
        public int X
        {
            get
            {
                return _coordinateDecorator.X * -1;
            }
            set
            {
                _coordinateDecorator.X = value * -1;
            }
        }

        public int Y
        {
            get
            {
                return _coordinateDecorator.Y;
            }
            set
            {
                _coordinateDecorator.Y = value;
            }
        }

        private readonly ICoordinateDecorator _coordinateDecorator;

        public InversionXCoordinateDecorator(ICoordinateDecorator coordinateDecorator)
        {
            _coordinateDecorator = coordinateDecorator;
        }
    }

    public class InversionYCoordinateDecorator : ICoordinateDecorator
    {
        public int X
        {
            get
            {
                return _coordinateDecorator.X;
            }
            set
            {
                _coordinateDecorator.X = value;
            }
        }

        public int Y
        {
            get
            {
                return _coordinateDecorator.Y * -1;
            }
            set
            {
                _coordinateDecorator.Y = value * -1;
            }
        }

        private readonly ICoordinateDecorator _coordinateDecorator;

        public InversionYCoordinateDecorator(ICoordinateDecorator coordinateDecorator)
        {
            _coordinateDecorator = coordinateDecorator;
        }
    }

    public class SwapCoordinateDecorator : ICoordinateDecorator
    {
        public int X 
        {   
            get
            {
                return _coordinateDecorator.Y;
            }
            set
            {
                _coordinateDecorator.Y = value; 
            }
        }
        public int Y
        {
            get
            {
                return _coordinateDecorator.X;
            }
            set
            {
                _coordinateDecorator.X = value;
            }
        }

        private readonly ICoordinateDecorator _coordinateDecorator;

        public SwapCoordinateDecorator(ICoordinateDecorator coordinateDecorator)
        {
            _coordinateDecorator = coordinateDecorator;
        }
    }

    public class BuildCoordinateDecorator
    {
        private bool IsSetSwapCoordinate = false;
        private bool IsInversionXCoordinate = false;
        private bool IsInversionYCoordinate = false;

        public BuildCoordinateDecorator SetSwap()
        {
            IsSetSwapCoordinate = true;

            return this;
        }

        public BuildCoordinateDecorator SetInversionXCoordinate()
        {
            IsInversionXCoordinate = true;

            return this;
        }

        public BuildCoordinateDecorator SetInversionYCoordinate()
        {
            IsInversionYCoordinate = true;

            return this;
        }

        public ICoordinateDecorator Create(ICoordinateDecorator coordinate)
        {
            if (IsSetSwapCoordinate && IsInversionXCoordinate && IsInversionYCoordinate)
            {
                ICoordinateDecorator inversionXCoordinateDecorator = new InversionXCoordinateDecorator(coordinate);
                ICoordinateDecorator InversionYCoordinateDecorator = new InversionYCoordinateDecorator(inversionXCoordinateDecorator);
                ICoordinateDecorator swapCoordinateDecorator = new SwapCoordinateDecorator(InversionYCoordinateDecorator);

                return swapCoordinateDecorator;
            }

            if (!IsSetSwapCoordinate && IsInversionXCoordinate && IsInversionYCoordinate)
            {
                ICoordinateDecorator inversionXCoordinateDecorator = new InversionXCoordinateDecorator(coordinate);
                ICoordinateDecorator InversionYCoordinateDecorator = new InversionYCoordinateDecorator(inversionXCoordinateDecorator);

                return InversionYCoordinateDecorator;
            }

            if (!IsSetSwapCoordinate && !IsInversionXCoordinate && IsInversionYCoordinate)
            {
                ICoordinateDecorator InversionYCoordinateDecorator = new InversionYCoordinateDecorator(coordinate);

                return InversionYCoordinateDecorator;
            }

            if (!IsSetSwapCoordinate && !IsInversionXCoordinate && !IsInversionYCoordinate)
            {
                return coordinate;
            }


            if (IsSetSwapCoordinate && !IsInversionXCoordinate && IsInversionYCoordinate)
            {
                ICoordinateDecorator InversionYCoordinateDecorator = new InversionYCoordinateDecorator(coordinate);
                ICoordinateDecorator swapCoordinateDecorator = new SwapCoordinateDecorator(InversionYCoordinateDecorator);

                return swapCoordinateDecorator;
            }

            if (IsSetSwapCoordinate && !IsInversionXCoordinate && !IsInversionYCoordinate)
            {
                ICoordinateDecorator swapCoordinateDecorator = new SwapCoordinateDecorator(coordinate);

                return swapCoordinateDecorator;
            }


            if (!IsSetSwapCoordinate && IsInversionXCoordinate && !IsInversionYCoordinate)
            {
                ICoordinateDecorator InversionXCoordinate = new InversionXCoordinateDecorator(coordinate);

                return InversionXCoordinate;
            }

            if (IsSetSwapCoordinate && IsInversionXCoordinate && !IsInversionYCoordinate)
            {
                ICoordinateDecorator InversionXCoordinateDecorator = new InversionXCoordinateDecorator(coordinate);
                ICoordinateDecorator swapCoordinateDecorator = new SwapCoordinateDecorator(InversionXCoordinateDecorator);

                return swapCoordinateDecorator;
            }

            throw new Exception("");
        }
    }

    public class Coordinate : ICoordinateDecorator
    {
        public Coordinate() {}

        public Coordinate((int x, int y) borderCoordinateSymbol)
        {
            X = borderCoordinateSymbol.x;
            Y = borderCoordinateSymbol.y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }

    public void Redraw()
    {
        if (_symbolOutgoingLine == null || _symbolaIncomingLine == null)
        {
            return;
        }

        var borderCoordinateSymbolOutgoing = _symbolOutgoingLine.GetBorderCoordinate(_positionOutgoing);
        var borderCoordinateSymbolaIncoming = _symbolaIncomingLine.GetBorderCoordinate(_positionIncoming);

        if ((_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Bottom) ||
            (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Top))
        {
            if (borderCoordinateSymbolOutgoing.x == borderCoordinateSymbolaIncoming.x)
            {
                SetCoordnateOneLine(borderCoordinateSymbolOutgoing, borderCoordinateSymbolaIncoming);
            }
            else if (borderCoordinateSymbolOutgoing.y < borderCoordinateSymbolaIncoming.y)
            {
                SetCoordnateTreeLine1(borderCoordinateSymbolOutgoing, borderCoordinateSymbolaIncoming);
            }
            else
            {
                SetCoordnateFive1(borderCoordinateSymbolOutgoing, borderCoordinateSymbolaIncoming);
            }
        }
        else if ((_positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Right) || 
            (_positionOutgoing == PositionConnectionPoint.Right && _positionIncoming == PositionConnectionPoint.Left))
        {
            if (borderCoordinateSymbolOutgoing.y == borderCoordinateSymbolaIncoming.y)
            {
                SetCoordnateOneLine(borderCoordinateSymbolOutgoing, borderCoordinateSymbolaIncoming);
            }
            else if (borderCoordinateSymbolOutgoing.x < borderCoordinateSymbolaIncoming.x)
            {
                SetCoordnateTreeLine2(borderCoordinateSymbolOutgoing, borderCoordinateSymbolaIncoming);
            }
            else
            {
                SetCoordnateFive2(borderCoordinateSymbolOutgoing, borderCoordinateSymbolaIncoming);
            }
        }        
    }

    private void SetCoordnateOneLine((int x, int y) borderCoordinateSymbolOutgoing, (int x, int y) borderCoordinateSymbolaIncoming)
    {
        var firstLine = _drawnLineSymbolVM.LineSymbols[^1];

        firstLine.X1 = borderCoordinateSymbolOutgoing.x;
        firstLine.Y1 = borderCoordinateSymbolOutgoing.y;
        
        firstLine.X2 = borderCoordinateSymbolaIncoming.x;
        firstLine.Y2 = borderCoordinateSymbolaIncoming.y;
    }

    private void SetCoordnateTreeLine1((int x, int y) coordinateBorderSymbolOutgoing1, (int x, int y) coordinateBorderSymbolIncoming1)
    {
        SetCoordnateTreeLine(coordinateBorderSymbolOutgoing1, coordinateBorderSymbolIncoming1, new BuildCoordinateDecorator());
    }

    private void SetCoordnateTreeLine2((int x, int y) borderCoordinateSymbolOutgoing, (int x, int y) borderCoordinateSymbolaIncoming)
    {
        SetCoordnateTreeLine(borderCoordinateSymbolOutgoing, borderCoordinateSymbolaIncoming, new BuildCoordinateDecorator().SetSwap());
    }

    private void SetCoordnateFive1((int x, int y) coordinateBorderSymbolOutgoing1, (int x, int y) coordinateBorderSymbolIncoming1)
    {
        SetCoordnateFive(coordinateBorderSymbolOutgoing1, coordinateBorderSymbolIncoming1, new BuildCoordinateDecorator());
    }

    private void SetCoordnateFive2((int x, int y) borderCoordinateSymbolOutgoing, (int x, int y) borderCoordinateSymbolaIncoming)
    {
        SetCoordnateFive(borderCoordinateSymbolOutgoing, borderCoordinateSymbolaIncoming, new BuildCoordinateDecorator().SetSwap());
    }

    private void SetCoordnateTreeLine((int x, int y) coordinateBorderSymbolOutgoing1, (int x, int y) coordinateBorderSymbolIncoming1, BuildCoordinateDecorator buildCoordinateDecorator)
    {
        var coordinateBorderSymbolOutgoing = buildCoordinateDecorator.Create(new Coordinate(coordinateBorderSymbolOutgoing1));
        var coordinateBorderSymbolIncoming = buildCoordinateDecorator.Create(new Coordinate(coordinateBorderSymbolIncoming1));

        var firstLine1Coordinate = new Coordinate();
        var firstLine1 = buildCoordinateDecorator.Create(firstLine1Coordinate);

        var firstLine2Coordinate = new Coordinate();
        var firstLine2 = buildCoordinateDecorator.Create(firstLine2Coordinate);

        var secondLine1Coordinate = new Coordinate();
        var secondLine2Coordinate = new Coordinate();

        var secondLine1 = buildCoordinateDecorator.Create(secondLine1Coordinate);
        var secondLine2 = buildCoordinateDecorator.Create(secondLine2Coordinate);

        var thirdLine1Coordinate = new Coordinate();
        var thirdLine2Coordinate = new Coordinate();

        var thirdLine1 = buildCoordinateDecorator.Create(thirdLine1Coordinate);
        var thirdLine2 = buildCoordinateDecorator.Create(thirdLine2Coordinate);

        firstLine1.X = coordinateBorderSymbolOutgoing.X;
        firstLine1.Y = coordinateBorderSymbolOutgoing.Y;

        firstLine2.X = coordinateBorderSymbolOutgoing.X;
        firstLine2.Y = coordinateBorderSymbolOutgoing.Y + (coordinateBorderSymbolIncoming.Y - coordinateBorderSymbolOutgoing.Y) / 2;


        secondLine1.X = coordinateBorderSymbolOutgoing.X;
        secondLine1.Y = coordinateBorderSymbolOutgoing.Y + (coordinateBorderSymbolIncoming.Y - coordinateBorderSymbolOutgoing.Y) / 2;

        secondLine2.X = coordinateBorderSymbolIncoming.X;
        secondLine2.Y = coordinateBorderSymbolOutgoing.Y + (coordinateBorderSymbolIncoming.Y - coordinateBorderSymbolOutgoing.Y) / 2;


        thirdLine1.X = coordinateBorderSymbolIncoming.X;
        thirdLine1.Y = coordinateBorderSymbolOutgoing.Y + (coordinateBorderSymbolIncoming.Y - coordinateBorderSymbolOutgoing.Y) / 2;

        thirdLine2.X = coordinateBorderSymbolIncoming.X;
        thirdLine2.Y = coordinateBorderSymbolIncoming.Y;

        var firstLine = _drawnLineSymbolVM.LineSymbols[^3];

        firstLine.X1 = firstLine1Coordinate.X;
        firstLine.Y1 = firstLine1Coordinate.Y;

        firstLine.X2 = firstLine2Coordinate.X;
        firstLine.Y2 = firstLine2Coordinate.Y;

        var secondLine = _drawnLineSymbolVM.LineSymbols[^2];

        secondLine.X1 = secondLine1Coordinate.X;
        secondLine.Y1 = secondLine1Coordinate.Y;

        secondLine.X2 = secondLine2Coordinate.X;
        secondLine.Y2 = secondLine2Coordinate.Y;

        var thirdLine = _drawnLineSymbolVM.LineSymbols[^1];

        thirdLine.X1 = thirdLine1Coordinate.X;
        thirdLine.Y1 = thirdLine1Coordinate.Y;

        thirdLine.X2 = thirdLine2Coordinate.X;
        thirdLine.Y2 = thirdLine2Coordinate.Y;
    }

    private void SetCoordnateFive((int x, int y) coordinateBorderSymbolOutgoing1, (int x, int y) coordinateBorderSymbolIncoming1, BuildCoordinateDecorator buildCoordinateDecorator)
    {
        var coordinateBorderSymbolOutgoing = buildCoordinateDecorator.Create(new Coordinate(coordinateBorderSymbolOutgoing1));
        var coordinateBorderSymbolIncoming = buildCoordinateDecorator.Create(new Coordinate(coordinateBorderSymbolIncoming1));

        var firstLine1Coordinate = new Coordinate();
        var firstLine2Coordinate = new Coordinate();

        var firstLine1 = buildCoordinateDecorator.Create(firstLine1Coordinate);
        var firstLine2 = buildCoordinateDecorator.Create(firstLine2Coordinate);

        var secondLine1Coordinate = new Coordinate();
        var secondLine2Coordinate = new Coordinate();

        var secondLine1 = buildCoordinateDecorator.Create(secondLine1Coordinate);
        var secondLine2 = buildCoordinateDecorator.Create(secondLine2Coordinate);

        var thirdLine1Coordinate = new Coordinate();
        var thirdLine2Coordinate = new Coordinate();

        var thirdLine1 = buildCoordinateDecorator.Create(thirdLine1Coordinate);
        var thirdLine2 = buildCoordinateDecorator.Create(thirdLine2Coordinate);

        var fourthLine1Coordinate = new Coordinate();
        var fourthLine2Coordinate = new Coordinate();

        var fourthLine1 = buildCoordinateDecorator.Create(fourthLine1Coordinate);
        var fourthLine2 = buildCoordinateDecorator.Create(fourthLine2Coordinate);

        var fifthLine1Coordinate = new Coordinate();
        var fifthLine2Coordinate = new Coordinate();

        var fifthLine1 = buildCoordinateDecorator.Create(fifthLine1Coordinate);
        var fifthLine2 = buildCoordinateDecorator.Create(fifthLine2Coordinate);

        firstLine1.X = coordinateBorderSymbolOutgoing.X;
        firstLine1.Y = coordinateBorderSymbolOutgoing.Y;

        firstLine2.X = coordinateBorderSymbolOutgoing.X;
        firstLine2.Y = coordinateBorderSymbolOutgoing.Y + baseLineOffset;

        secondLine1.X = coordinateBorderSymbolOutgoing.X;
        secondLine1.Y = coordinateBorderSymbolOutgoing.Y + baseLineOffset;

        secondLine2.X = coordinateBorderSymbolOutgoing.X + (coordinateBorderSymbolIncoming.X - coordinateBorderSymbolOutgoing.X) / 2;
        secondLine2.Y = coordinateBorderSymbolOutgoing.Y + baseLineOffset;

        thirdLine1.X = coordinateBorderSymbolOutgoing.X + (coordinateBorderSymbolIncoming.X - coordinateBorderSymbolOutgoing.X) / 2;
        thirdLine1.Y = coordinateBorderSymbolOutgoing.Y + baseLineOffset;

        thirdLine2.X = coordinateBorderSymbolOutgoing.X + (coordinateBorderSymbolIncoming.X - coordinateBorderSymbolOutgoing.X) / 2;
        thirdLine2.Y = coordinateBorderSymbolIncoming.Y - baseLineOffset;

        fourthLine1.X = coordinateBorderSymbolOutgoing.X + (coordinateBorderSymbolIncoming.X - coordinateBorderSymbolOutgoing.X) / 2;
        fourthLine1.Y = coordinateBorderSymbolIncoming.Y - baseLineOffset;

        fourthLine2.X = coordinateBorderSymbolIncoming.X;
        fourthLine2.Y = coordinateBorderSymbolIncoming.Y - baseLineOffset;

        fifthLine1.X = coordinateBorderSymbolIncoming.X;
        fifthLine1.Y = coordinateBorderSymbolIncoming.Y - baseLineOffset;

        fifthLine2.X = coordinateBorderSymbolIncoming.X;
        fifthLine2.Y = coordinateBorderSymbolIncoming.Y;

        var firstLine = _drawnLineSymbolVM.LineSymbols[^5];

        firstLine.X1 = firstLine1Coordinate.X;
        firstLine.Y1 = firstLine1Coordinate.Y;

        firstLine.X2 = firstLine2Coordinate.X;
        firstLine.Y2 = firstLine2Coordinate.Y;

        var secondLine = _drawnLineSymbolVM.LineSymbols[^4];

        secondLine.X1 = secondLine1Coordinate.X;
        secondLine.Y1 = secondLine1Coordinate.Y;

        secondLine.X2 = secondLine2Coordinate.X;
        secondLine.Y2 = secondLine2Coordinate.Y;

        var thirdLine = _drawnLineSymbolVM.LineSymbols[^3];

        thirdLine.X1 = thirdLine1Coordinate.X;
        thirdLine.Y1 = thirdLine1Coordinate.Y;

        thirdLine.X2 = thirdLine2Coordinate.X;
        thirdLine.Y2 = thirdLine2Coordinate.Y;

        var fourthLine = _drawnLineSymbolVM.LineSymbols[^2];

        fourthLine.X1 = fourthLine1Coordinate.X;
        fourthLine.Y1 = fourthLine1Coordinate.Y;

        fourthLine.X2 = fourthLine2Coordinate.X;
        fourthLine.Y2 = fourthLine2Coordinate.Y;

        var fifthLine = _drawnLineSymbolVM.LineSymbols[^1];

        fifthLine.X1 = fifthLine1Coordinate.X;
        fifthLine.Y1 = fifthLine1Coordinate.Y;

        fifthLine.X2 = fifthLine2Coordinate.X;
        fifthLine.Y2 = fifthLine2Coordinate.Y;
    }
}