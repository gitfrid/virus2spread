using VirusSpreadLibrary.Enum;

namespace VirusSpreadLibrary.Creature;

public class Virus
{
    public Guid ID { get; set; }
    public CreatureType CreatureType { get; set; }
    public int Age { get; set; }
    public double VirusReproduceRateByAge { get; set; }
    public double VirusBrokenRateByAge { get; set; }
    public VirusState VirusState { get; set; }
    public Virus(VirusState virusState)
    {
        VirusState = virusState;
    }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public bool IsBroken { get; set; }

}
