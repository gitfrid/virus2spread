
using Point = System.Drawing.Point;

namespace VirusSpreadLibrary.Creature;

public class MoveData
{
    // helper object passed to the grid, to draw a grid cell in certain color,
    // which represents the persons or viruses on that cell
    public Point OldGridCoordinate { get; set; }
    public Point NewGridCoordinate { get; set; }
    public Enum.CreatureType CreatureType { get; set; }

}
