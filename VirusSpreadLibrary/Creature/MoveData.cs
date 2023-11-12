
using Point = System.Drawing.Point;

namespace VirusSpreadLibrary.Creature;

public class MoveData
{
    // helper object passed to the grid, to draw a grid cell in certain color,
    // coler represent the persons or viruses status on that cell
    public Point StartGidCoordinate { get; set; }
    public Point EndGridCoordinate { get; set; }
    public Point HomeGridCoordinate { get; set; }
    public Enum.CreatureType CreatureType { get; set; }

}
