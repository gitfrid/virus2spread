
using VirusSpreadLibrary.Enum;
using VirusSpreadLibrary.Grid;

namespace VirusSpreadLibrary.Creature;

public class Person
{
    public Person(PersonState personState, CellPopulation gridCellPopulation)
    {
        PersonState = personState;
        GridCellPopulation = gridCellPopulation;
    }
    
    public PersonState PersonState;
    public CellPopulation GridCellPopulation { get; set; }
    public CreatureType CreatureType { get; set; }
    public int Age { get; set; }
    public double PersonBirthRateByAge { get; set; }
    public double PersonDeathProbabilityByAge { get; set; }
    public bool IsDead { get; set; }
    public Point Coordinate { get; set; }
    public Point HomeCoordinate { get; set; }
    public double MoveActivityRnd { get; set; }
 
    public int ReinfectionCounter { get; set; }



}
