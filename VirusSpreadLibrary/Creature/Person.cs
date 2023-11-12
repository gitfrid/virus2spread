
using VirusSpreadLibrary.Creature.Rates;
using VirusSpreadLibrary.Grid;
using Point = System.Drawing.Point;

namespace VirusSpreadLibrary.Creature;

public class Person
{
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
        PersMoveDistanceProfile PersMoveProfile = new PersMoveDistanceProfile();

        // get new random endpoint to move to
        // depending on PersMoveProfile settings and PersonMoveGlobal var
        if (Properties.Settings.Default.PersonMoveGlobal)
        {
            PersMoveData.EndGridCoordinate = PersMoveProfile.GetEndCoordinateToMove(PersMoveData.StartGidCoordinate);
        } 
        else 
        {
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
