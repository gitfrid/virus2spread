
using VirusSpreadLibrary.Enum;
using VirusSpreadLibrary.Grid;

namespace VirusSpreadLibrary.Creature;

public class Person
{
    public int Age { get; set; }
    public double PersonBirthRateByAge { get; set; }
    public double PersonDeathProbabilityByAge { get; set; }
    public PersonState? PersonState { get; set; }   
    public bool IsDead { get; set; }
    public Point HomeCoordinate { get; set; }
    public CellPopulation? GridCellPopulation { get; set; }
    public MoveData? MoveToData { get; set; }

    public void Childbirth()
    {
     //    
    }
    public void SpreadVirus()
    {
        //
    }
    public void MoveToNewCoordinate(out MoveData? MoveData, Grid.CellPopulation Population)
    {
        MoveData = MoveToData;
    }
    public void MoveToHomeCoordinate(out MoveData? MoveData, Grid.CellPopulation Population)
    {
        MoveData = MoveToData;
    }


}
