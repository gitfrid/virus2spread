using VirusSpreadLibrary.Properties;

namespace VirusSpreadLibrary.Creature.Rates;

public class VirMoveDistanceProfile
{
    private Random rnd = new Random();
    public double HomeMoveActivity { get; set; }
    private Point GetVirusMoveDistanceByIndex(int Index)
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

    public Point MoveTo(Point Pnt)
    {
        int beta = rnd.Next(0, 91); // get X Y coordinate by the random distance and a random move angel between 0-90
        Point pnt = GetVirusMoveDistanceByIndex(rnd.Next(0, 11));
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
        // check for grid boundarys 
        if (Y > Settings.Default.GridMaxY | Y < 0)
        {
            Y = Pnt.Y;
        };
        if (X > Settings.Default.GridMaxX | X < 0)
        {
            X = Pnt.X;
        };

        return new Point(Y, X); ;
    }

}
