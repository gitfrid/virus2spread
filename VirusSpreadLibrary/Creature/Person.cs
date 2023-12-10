using VirusSpreadLibrary.AppProperties;
using VirusSpreadLibrary.Creature.Rates;
using VirusSpreadLibrary.Grid;
using Point = System.Drawing.Point;

namespace VirusSpreadLibrary.Creature;

public class Person
{
    private readonly Random rnd = new ();
    
    private readonly PersMoveDistanceProfile persMoveProfile = new();   
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

    public bool DoMove()
    {
        return (AppSettings.Config.PersonMoveActivityRnd != 0) &&
                (1 == rnd.Next(1, 1+(int)(AppSettings.Config.PersonMoveActivityRnd)));
    }
    public bool DoMoveHome()
    {
        return (AppSettings.Config.PersonMoveHomeActivityRnd != 0) &&
                (1 == rnd.Next(1, 1+(int)(AppSettings.Config.PersonMoveHomeActivityRnd)));
    }
    public void ChildBirth()
    {
     //    
    }
    public void SpreadVirus()
    {
        //
    }
    public void MoveToNewCoordinate(Grid.Grid Grid)
    {
        // get new random endpoint to move to
        // depending on the spcified range in settings of persMoveProfile and PersonMoveGlobal var
        if (AppSettings.Config.PersonMoveGlobal)
        {   
            // calculate next move from EndCoordinate of the last iteration, in the spcified range - moves over whole grid
            PersMoveData.EndGridCoordinate = persMoveProfile.GetEndCoordinateToMove(PersMoveData.StartGidCoordinate);
        } 
        else
        {   
            // calculate next move always from the Home Coordinate in the specified range - moves only within the range
            PersMoveData.EndGridCoordinate = persMoveProfile.GetEndCoordinateToMove(PersMoveData.HomeGridCoordinate);
        }

        // do move to endpoint
        Grid.AddCreatureToCell(PersMoveData);
        
        // save current endpoint as the new startpoint
        // to use in next iteration if VirusMoveGlobal is true
        PersMoveData.StartGidCoordinate = PersMoveData.EndGridCoordinate;
    }
    public void MoveToHomeCoordinate(Grid.Grid Grid)
    {
        PersMoveData.EndGridCoordinate = PersMoveData.HomeGridCoordinate;
        Grid.AddCreatureToCell(PersMoveData);
        // save current endpoint as the new startpoint
        PersMoveData.StartGidCoordinate = PersMoveData.EndGridCoordinate;
    }
    
}
