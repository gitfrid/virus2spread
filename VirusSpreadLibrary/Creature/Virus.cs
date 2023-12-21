using VirusSpreadLibrary.AppProperties;
using VirusSpreadLibrary.Creature.Rates;

namespace VirusSpreadLibrary.Creature;

public class Virus
{
    private readonly Random rnd = new ();
 
    private readonly VirMoveDistanceProfile virMoveProfile = new();
    
    public Virus()
    {
        VirMoveData = new MoveData();
        VirMoveData.CreatureType = Enum.CreatureType.Virus;
        VirusState = new VirusState();
     }
    public int Age { get; set; }
    public double VirusReproduceRateByAge { get; set; }
    public double VirusBrokenRateByAge { get; set; }
    public VirusState VirusState { get; set; }
    public bool IsBroken { get; set; }
    public MoveData VirMoveData { get; set; }

    public bool DoMove()
    {
        return (AppSettings.Config.VirusMoveActivityRnd != 0) &&
                (1 == rnd.Next(1, 1+(int)(AppSettings.Config.VirusMoveActivityRnd)));
    }
    public bool DoMoveHome()
    {
        return (AppSettings.Config.VirusMoveHomeActivityRnd != 0) &&
                (1 == rnd.Next(1, 1+(int)(AppSettings.Config.VirusMoveHomeActivityRnd)));
    }

    public void Reproduce()
    {
        //
    }

    public void MoveToNewCoordinate(Grid.Grid Grid)
    {
        // get new random endpoint to move to
        // depending on the spcified range in settings of persMoveProfile and PersonMoveGlobal var
        if (AppSettings.Config.VirusMoveGlobal)
        {
            // calculate next move from EndCoordinate of the last iteration, in the spcified range - moves over whole grid
            VirMoveData.EndGridCoordinate = virMoveProfile.GetEndCoordinateToMove(VirMoveData.StartGidCoordinate);
        }
        else
        {
            // calculate next move always from the Home Coordinate in the specified range - moves only within the range
            VirMoveData.EndGridCoordinate = virMoveProfile.GetEndCoordinateToMove(VirMoveData.HomeGridCoordinate);
        }

        // do move to endpoint
        SpreadModel.SetGridCellState.VirusMoveState(this, Grid);


        // save current endpoint as the new startpoint
        // to use in next iteration if VirusMoveGlobal is true
        VirMoveData.StartGidCoordinate = VirMoveData.EndGridCoordinate;
    }
    public void MoveToHomeCoordinate(Grid.Grid Grid)
    {
        VirMoveData.EndGridCoordinate = VirMoveData.HomeGridCoordinate;
        // do move to endpoint
        SpreadModel.SetGridCellState.VirusMoveState(this, Grid);

        // save current endpoint as the new startpoint
        VirMoveData.StartGidCoordinate = VirMoveData.EndGridCoordinate;
    }

}
