
using VirusSpreadLibrary.AppProperties;
using VirusSpreadLibrary.AppProperties.PropertyGridExt;
using Point = System.Drawing.Point;

namespace VirusSpreadLibrary.Creature.Rates;

// motion profile
// simulate the moving behavior of a virus
// random choose one from 10 distance ranges
// get a random distance within the selected distance range
// get a random direction 365° 
// returns the EndGridCoordinate for the move.

public class VirusInvalidIndexException : Exception
{
    public VirusInvalidIndexException()
    {
    }
    public VirusInvalidIndexException(string StringToPass) : base(
        (String.Format("VirusMoveRateFrom and VirusMoveRateTo must have the same number of entries.\r\n" +
        "VirusMoveRateFrom values must be smaller as the related VirusMoveRateTo value!\r\n\r\n" +
        "VirusMoveRates will be reset to the default values!\r\n\r\n" +
        "Please enter the desired correct values in APP-Settings: Category -> Move Rate Virus\r\n{0}", StringToPass)))
    { }
    public VirusInvalidIndexException(string message, Exception inner) : base(message, inner) { }
}

public class VirMoveDistanceProfile
{
    private int maxX = 0;
    private int maxY = 0;
   
    private Random rnd = new Random();

    private readonly Point[] moveDistance = [];
    public double MoveActivityRnd { get; set; }
    public VirMoveDistanceProfile()
    {
        maxX = AppSettings.Config.GridMaxX;
        maxY = AppSettings.Config.GridMaxY;
        DoubleSeries seriesFrom = AppSettings.Config.VirusMoveRate.DoubleSeriesFrom;
        DoubleSeries seriesTo = AppSettings.Config.VirusMoveRate.DoubleSeriesTo;
        if (seriesFrom.DoubleArray.Length == seriesTo.DoubleArray.Length)
        {
            moveDistance = new Point[seriesFrom.DoubleArray.Length];
            for (int i = 0; i < seriesFrom.DoubleArray.Length; i++)
            {
                moveDistance[i] = new Point(((int)seriesFrom.DoubleArray[i]), ((int)seriesTo.DoubleArray[i]));
            }
        }
        else
        {
            AppSettings.Config.VirusMoveRate.DoubleSeriesFrom = new DoubleSeries([1, 1, 1, 1, 1, 1, 1, 1, 1, 1]);
            AppSettings.Config.VirusMoveRate.DoubleSeriesTo = new DoubleSeries([2, 2, 2, 2, 2, 2, 2, 2, 2, 2]);
            throw new VirusInvalidIndexException("");
        }
    }
    private Point GetMoveDistanceByIndex(int Index)
    {
        return moveDistance[Index];
    }
    public Point GetEndCoordinateToMove(Point StartCoordiante)
    {
        int beta = rnd.Next(0, 91); // get X Y coordinate by the random distance and a random move angel between 0-90
        Point pnt = GetMoveDistanceByIndex(rnd.Next(0,moveDistance.Length));
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
