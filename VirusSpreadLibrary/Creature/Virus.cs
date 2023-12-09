using VirusSpreadLibrary.AppProperties;
using VirusSpreadLibrary.Creature.Rates;
using VirusSpreadLibrary.Grid;

namespace VirusSpreadLibrary.Creature;

public class Virus
{
    private readonly Random rnd = new ();
    public int Age { get; set; }
    public double VirusReproduceRateByAge { get; set; }
    public double VirusBrokenRateByAge { get; set; }
    public VirusState VirusState { get; set; }
    public bool IsBroken { get; set; }
    public MoveData VirMoveData { get; set; }
    public CellPopulation GridCellPopulation { get; set; }
    
    public Virus()
    {
        VirMoveData = new MoveData();
        GridCellPopulation = new CellPopulation();
        VirMoveData.CreatureType = Enum.CreatureType.Virus;
        VirusState = new VirusState();
     }
    private bool DoMove()
    {
        return (AppSettings.Config.VirusMoveActivityRnd != 0) &&
                (1 == rnd.Next(1, (int)(1 / AppSettings.Config.VirusMoveActivityRnd)));
    }
    private bool DoMoveHome()
    {
        return (AppSettings.Config.VirusMoveHomeActivityRnd != 0) &&
                (1 == rnd.Next(1, (int)(1 / AppSettings.Config.VirusMoveHomeActivityRnd)));
    }

    public void Reproduce()
    {
        //
    }
    public void MoveToNewCoordinate(Grid.Grid GridField)
    {
        VirMoveDistanceProfile VirMoveProfile = new();

        // get new random endpoint to move to
        // depending on VirMoveProfile settings and VirusMoveGlobal var 
        if (AppSettings.Config.VirusMoveGlobal)
        {
            if (DoMove()) 
                VirMoveData.EndGridCoordinate = VirMoveProfile.GetEndCoordinateToMove(VirMoveData.StartGidCoordinate);
            if (DoMoveHome())
                VirMoveData.EndGridCoordinate = VirMoveProfile.GetEndCoordinateToMove(VirMoveData.HomeGridCoordinate);
        }
        else
        {
            if (DoMoveHome())
                VirMoveData.EndGridCoordinate = VirMoveProfile.GetEndCoordinateToMove(VirMoveData.HomeGridCoordinate);
        }

        // move to endpoint
        GridCellPopulation = GridField.AddCreatureToCell(VirMoveData);

        // save current endpoint as the new startpoint
        // to use in next iteration if VirusMoveGlobal is true
        VirMoveData.StartGidCoordinate = VirMoveData.EndGridCoordinate;      
    }

}
