namespace SerializationEdblock;

public class ProjectSerializable
{
    public List<BlockSymbolSerializable> BlocksSymbolSerializable { get; init; }

    public List<DrawnLineSymbolSerializable> DrawnLinesSymbolSerializable { get; init; }

    public ProjectSerializable(List<BlockSymbolSerializable> blocksSymbolSerializable, List<DrawnLineSymbolSerializable> drawnLinesSymbolSerializable)
    {
        BlocksSymbolSerializable = blocksSymbolSerializable;
        DrawnLinesSymbolSerializable = drawnLinesSymbolSerializable;
    }
}