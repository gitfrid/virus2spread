
namespace VirusSpreadLibrary.Creature;

public class PersonState
{
    public bool moving { get; set; } = false;
    public bool healthy { get; set; } = false;
    public bool infected { get; set; } = false;
    public bool contagious { get; set; } = false;
    public bool recoverd { get; set; } = false;

}
