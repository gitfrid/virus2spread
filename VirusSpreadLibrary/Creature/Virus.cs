using VirusSpreadLibrary.Creature.Rates;
using VirusSpreadLibrary.Grid;

namespace VirusSpreadLibrary.Creature;

public class Virus
{
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
    public void Reproduce()
    {
        //
    }
    public void MoveToNewCoordinate(Grid.Grid GridField)
    {
        VirMoveDistanceProfile VirMoveProfile = new VirMoveDistanceProfile();
        VirMoveData.NewGridCoordinate = VirMoveProfile.GetNewCoordinateToMove(VirMoveData.OldGridCoordinate);
        GridCellPopulation = GridField.CelAddCreature(VirMoveData);
    }

}
