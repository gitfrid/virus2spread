
using VirusSpreadLibrary.AppProperties;
using VirusSpreadLibrary.Creature.Rates;
using VirusSpreadLibrary.Grid;
using Point = System.Drawing.Point;

namespace VirusSpreadLibrary.Creature;

public class Person
{
    private Random rnd = new Random();
    public int Age { get; set; }
    public double PersonBirthRateByAge { get; set; }
    public double PersonDeathProbabilityByAge { get; set; }
    public PersonState PersonState { get; set; }
    public bool IsDead { get; set; }
    public MoveData PersMoveData { get; set; }
    public CellPopulation GridCellPopulation { get; set; }

    public Person()
    {
        PersMoveData = new MoveData();
        PersMoveData.CreatureType = Enum.CreatureType.Person;
        GridCellPopulation = new CellPopulation();
        PersonState = new PersonState();
    }
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
    public CellPopulation MoveToNewCoordinate(Grid.Grid GridField)
    {
        PersMoveDistanceProfile PersMoveProfile = new();

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
        GridCellPopulation = GridField.AddCreatureToCell(PersMoveData);

        // save current endpoint as the new startpoint
        // to use in next iteration if VirusMoveGlobal is true
        PersMoveData.StartGidCoordinate = PersMoveData.EndGridCoordinate;

        return GridCellPopulation;
    }
    public CellPopulation MoveToHomeCoordinate(Grid.Grid GridField)
    {
        PersMoveData.EndGridCoordinate = PersMoveData.HomeGridCoordinate;
        GridCellPopulation = GridField.AddCreatureToCell(PersMoveData);
        return GridCellPopulation;
    }
    
}
