using VirusSpreadLibrary.Properties;
using Point = System.Drawing.Point;

namespace VirusSpreadLibrary.Creature.Rates;

// Movement profile
// simulates travel behavior of frequent short and long distance movements of a person
// random select one of the 10 distance ranges
// Determine a random distance within the selected distance range
// Determines a random direction of 365° 
// returns the NewGridCoordinate for the movement

public class PersMoveDistanceProfile
{
    private int maxX = 0;
    private int maxY = 0;
    
    private Random rnd = new Random();
    public double HomeMoveActivity { get; set; }

    public PersMoveDistanceProfile()
    {
        maxX = Settings.Default.GridMaxX;
        maxY = Settings.Default.GridMaxY;
    }

    private Point GetMoveDistanceByIndex(int Index)
    {
        Point[] MoveDistance = new Point[10];
        MoveDistance[0] = new Point(1, 10);
        MoveDistance[1] = new Point(1, 10);
        MoveDistance[2] = new Point(1, 10);
        MoveDistance[3] = new Point(1, 10);
        MoveDistance[4] = new Point(1, 20);
        MoveDistance[5] = new Point(1, 20);
        MoveDistance[6] = new Point(1, 2000);
        MoveDistance[7] = new Point(1, 2000);
        MoveDistance[8] = new Point(1, 5000);
        MoveDistance[9] = new Point(1, 5000);

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
        // choose a random quadrant (direction) in coordinate system -> multiply X,Y random with 1 or -1 to  
        X = OldCoordiante.X + (rnd.Next(0, 2) * 2 - 1) * X;
        Y = OldCoordiante.Y + (rnd.Next(0, 2) * 2 - 1) * Y;
        // check for grid boundaries 
        if (Y >= maxY | Y < 0)
        {
            Y = OldCoordiante.Y;
        };
        if (X >= maxX | X < 0)
        {
            X = OldCoordiante.X;
        };

        return new Point(X, Y); ;
    }

}
