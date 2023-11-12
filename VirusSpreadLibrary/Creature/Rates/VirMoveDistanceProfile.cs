
using VirusSpreadLibrary.Properties;
using Point = System.Drawing.Point;

namespace VirusSpreadLibrary.Creature.Rates;

// motion profile
// simulate the moving behavior of a virus
// random choose one from 10 distance ranges
// get a random distance within the selected distance range
// get a random direction 365° 
// returns the EndGridCoordinate for the move.

public class VirMoveDistanceProfile
{
    private int maxX = 0;
    private int maxY = 0;
   
    private Random rnd = new Random();
    public double MoveActivityRnd { get; set; }
    public VirMoveDistanceProfile()
    {
        maxX = Settings.Default.GridMaxX;
        maxY = Settings.Default.GridMaxY;
    }

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

    public Point GetEndCoordinateToMove(Point StartCoordiante)
    {
        int beta = rnd.Next(0, 91); // get X Y coordinate by the random distance and a random move angel between 0-90
        Point pnt = GetMoveDistanceByIndex(rnd.Next(0, 10));
        int a = rnd.Next(pnt.X, pnt.Y); 
        double b = a / Math.Tan(90 - beta);
        double c = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
        double q = Math.Pow(a, 2) / c;
        double p = c - q;
        int Y = (int)Math.Round(Math.Sqrt(p * q));
        int X = (int)Math.Round(q);
        // choose a random quadrant in coordinate system -> multiply X,Y random with 1 or -1 to  
        X = StartCoordiante.X + (rnd.Next(0, 2) * 2 - 1) * X;
        Y = StartCoordiante.Y + (rnd.Next(0, 2) * 2 - 1) * Y;
        // check for grid boundarys 
        if (Y >= maxY | Y < 0)
        {
            Y = StartCoordiante.Y;
        };
        if (X >= maxX | X < 0)
        {
            X = StartCoordiante.X;
        };

        return new Point(X, Y); ;
    }

}
