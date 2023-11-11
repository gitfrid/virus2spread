
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
    public Point HomeCoordinate { get; set; }
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
        PersMoveData.NewGridCoordinate = PersMoveProfile.GetNewCoordinateToMove(PersMoveData.OldGridCoordinate);
        GridCellPopulation = GridField.AddCreatureToCell(PersMoveData);
        return GridCellPopulation;
    }
    public CellPopulation MoveToHomeCoordinate(Grid.Grid GridField)
    {
        PersMoveData.NewGridCoordinate = HomeCoordinate;
        GridCellPopulation = GridField.AddCreatureToCell(PersMoveData);
        return GridCellPopulation;
    }
}
