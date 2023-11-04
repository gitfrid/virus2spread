
using VirusSpreadLibrary.Properties;
using Point = System.Drawing.Point;

namespace VirusSpreadLibrary.Creature.Rates;

// motion profile
// simulate the moving behavior of a virus
// random choose one from 10 distance ranges
// get a random distance within the selected distance range
// get a random direction 365° 
// returns the NewGridCoordinate for the move.

public class VirMoveDistanceProfile
{
    private Random rnd = new Random();
    public double MoveActivityRnd { get; set; }
    private Point GetMoveDistanceByIndex(int Index)
    {
        Point[] MoveDistance = new Point[10];
        MoveDistance[0] = new Point(1, 10);
        MoveDistance[1] = new Point(1, 10);
        MoveDistance[2] = new Point(1, 10);
        MoveDistance[3] = new Point(1, 10);
        MoveDistance[4] = new Point(1, 10);
        MoveDistance[5] = new Point(1, 10);
        MoveDistance[6] = new Point(1, 10);
        MoveDistance[7] = new Point(1, 10);
        MoveDistance[8] = new Point(1, 10);
        MoveDistance[9] = new Point(1, 10);

        return MoveDistance[Index];
    }

    public Point GetNewCoordinateToMove(Point OldCoordiante)
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
        X = OldCoordiante.X + (rnd.Next(0, 2) * 2 - 1) * X;
        Y = OldCoordiante.Y + (rnd.Next(0, 2) * 2 - 1) * Y;
        // check for grid boundarys 
        if (Y > Settings.Default.GridMaxY | Y < 0)
        {
            Y = OldCoordiante.Y;
        };
        if (X > Settings.Default.GridMaxX | X < 0)
        {
            X = OldCoordiante.X;
        };

        return new Point(X, Y); ;
    }

}
