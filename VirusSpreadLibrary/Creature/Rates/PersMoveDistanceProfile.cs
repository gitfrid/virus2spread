using VirusSpreadLibrary.Grid;
using VirusSpreadLibrary.Properties;

namespace VirusSpreadLibrary.Creature.Rates;


public class PersMoveDistanceProfile
{
    private int maxX = 0;
    private int maxY = 0;
    public PersMoveDistanceProfile(Grid.Grid Grid)
    {
        maxX = Grid.ReturnMaxX();
        maxY = Grid.ReturnMaxY();
    }

    private Random rnd = new Random();
    public double HomeMoveActivity { get; set; }
    private Point GetMoveDistanceByIndex(int Index)
    {
        Point[] MoveDistance = new Point[10];
        MoveDistance[0] = new Point(1, 1);
        MoveDistance[1] = new Point(1, 1);
        MoveDistance[2] = new Point(1, 1);
        MoveDistance[3] = new Point(1, 1);
        MoveDistance[4] = new Point(1, 1);
        MoveDistance[5] = new Point(1, 1);
        MoveDistance[6] = new Point(1, 1);
        MoveDistance[7] = new Point(1, 1);
        MoveDistance[8] = new Point(1, 1);
        MoveDistance[9] = new Point(1, 1);

        return MoveDistance[Index];
    }

    public Point GetNewCoordinateMoveTo(Point Pnt)
    {
        int beta = rnd.Next(0, 91); // get X Y coordinate by the random distance and a random move angel between 0-90
        Point pnt = GetMoveDistanceByIndex(rnd.Next(0, 11));
        int a = rnd.Next(pnt.X, pnt.X);
        var b = a / Math.Tan(90 - beta);
        var c = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
        var q = Math.Pow(a, 2) / c;
        var p = c - q;
        int Y = (int)Math.Round(Math.Sqrt(p * q));
        int X = (int)Math.Round(q);
        // choose a random quadrant in coordinate system -> multiply X,Y random with 1 or -1 to  
        X = Pnt.X + (rnd.Next(0, 2) * 2 - 1) * X;
        Y = Pnt.Y + (rnd.Next(0, 2) * 2 - 1) * Y;
        // check for grid boundaries 
        if (Y > maxY | Y < 0)
        {
            Y = Pnt.Y;
        };
        if (X > maxX | X < 0)
        {
            X = Pnt.X;
        };

        return new Point(Y, X); ;
    }

}
