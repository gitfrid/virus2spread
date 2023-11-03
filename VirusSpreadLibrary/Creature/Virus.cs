using VirusSpreadLibrary.Enum;

namespace VirusSpreadLibrary.Creature;

public class Virus
{
    public int Age { get; set; }
    public double VirusReproduceRateByAge { get; set; }
    public double VirusBrokenRateByAge { get; set; }
    public VirusState? VirusState { get; set; }
    public bool IsBroken { get; set; }
    public Grid.CellPopulation? GridCellPopulation { get; set; }

    public MoveData MoveToData  { get; set; }

    public Virus()
    {
        MoveToData = new MoveData();
    }
    public void Reproduce()
    {
        //
    }
    public void MoveToNewCoordinate(out MoveData MoveTo, Grid.CellPopulation Population)
    {       
            MoveTo = MoveToData;
    }




}
