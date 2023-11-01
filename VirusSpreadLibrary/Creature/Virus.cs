using VirusSpreadLibrary.Enum;

namespace VirusSpreadLibrary.Creature;

public class Virus
{
    public Virus(VirusState virusState)
    {
        VirusState = virusState;
    }
    public VirusState VirusState { get; set; }
    public CreatureType CreatureType { get; set; }
    public int Age { get; set; }
    public double VirusReproduceRateByAge { get; set; }
    public double VirusBrokenRateByAge { get; set; }
    public bool IsBroken { get; set; }
    public Point GridCoordinate { get; set; }
    
}
