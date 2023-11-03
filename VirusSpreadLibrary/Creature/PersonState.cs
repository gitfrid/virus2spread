
namespace VirusSpreadLibrary.Creature;

public class PersonState
{
    public bool Healthy { get; set; } = false;
    public bool Infected { get; set; } = false;
    public int ReinfectionNumber { get; set; } = 0;
    public bool Contagious { get; set; } = false;
    public bool Recoverd { get; set; } = false;
    public bool Moving { get; set; } = false;
    public bool GiveBirth { get; set; }    

}
