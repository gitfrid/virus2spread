
using Point = System.Drawing.Point;

namespace VirusSpreadLibrary.Creature;

public class MoveData
{
    // object passed to the grid, to draw a grid cell in certain color, which represent the creatures on that cell
    public Point OldGridCoordinate { get; set; }
    public Point NewGridCoordinate { get; set; }
    public Enum.CreatureType Creature { get; set; }

}
