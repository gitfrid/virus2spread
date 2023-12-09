using VirusSpreadLibrary.AppProperties;
using VirusSpreadLibrary.Creature.Rates;
using VirusSpreadLibrary.Grid;
using Point = System.Drawing.Point;

namespace VirusSpreadLibrary.Creature;

public class Person
{
    private readonly Random rnd = new ();
    
    private readonly PersMoveDistanceProfile PersMoveProfile = new();   
    public Person()
    {
        PersMoveData = new()
        {
            CreatureType = Enum.CreatureType.Person
        };
        GridCellPopulation = new CellPopulation();
        PersonState = new PersonState();
    }
    public int Age { get; set; }
    public double PersonBirthRateByAge { get; set; }
    public double PersonDeathProbabilityByAge { get; set; }
    public PersonState PersonState { get; set; }
    public bool IsDead { get; set; }
    public MoveData PersMoveData { get; set; }
    public CellPopulation GridCellPopulation { get; set; }

    private bool DoMove()
    {
        return (AppSettings.Config.PersonMoveActivityRnd != 0) &&
                (1 == rnd.Next(1, (int)(1 / AppSettings.Config.PersonMoveActivityRnd)));
    }
    private bool DoMoveHome()
    {
        return (AppSettings.Config.PersonMoveHomeActivityRnd != 0) &&
                (1 == rnd.Next(1, (int)(1 / AppSettings.Config.PersonMoveHomeActivityRnd)));
    }
    public void ChildBirth()
    {
     //    
    }
    public void SpreadVirus()
    {
        //
    }
    public CellPopulation MoveToNewCoordinate(Grid.Grid Grid)
    {
        // get new random endpoint to move to
        // depending on PersMoveProfile settings and PersonMoveGlobal var
        if (AppSettings.Config.PersonMoveGlobal)
        {
            if (DoMove())          
                PersMoveData.EndGridCoordinate = PersMoveProfile.GetEndCoordinateToMove(PersMoveData.StartGidCoordinate);
            if(DoMoveHome())
                PersMoveData.EndGridCoordinate = PersMoveProfile.GetEndCoordinateToMove(PersMoveData.HomeGridCoordinate);
        } 
        else 
        {
            if (DoMoveHome()) 
                PersMoveData.EndGridCoordinate = PersMoveProfile.GetEndCoordinateToMove(PersMoveData.HomeGridCoordinate);
        }
        // move to endpoint
        GridCellPopulation = Grid.AddCreatureToCell(PersMoveData);
        // save current endpoint as the new startpoint
        // to use in next iteration if VirusMoveGlobal is true
        PersMoveData.StartGidCoordinate = PersMoveData.EndGridCoordinate;

        return GridCellPopulation;
    }
    public CellPopulation MoveToHomeCoordinate(Grid.Grid Grid)
    {
        PersMoveData.EndGridCoordinate = PersMoveData.HomeGridCoordinate;
        GridCellPopulation = Grid.AddCreatureToCell(PersMoveData);
        return GridCellPopulation;
    }
    
}
