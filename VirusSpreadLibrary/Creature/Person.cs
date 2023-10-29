using VirusSpreadLibrary.Enum;

namespace VirusSpreadLibrary.Creature;

public class Person
{
    public Guid ID { get; set; }
    public CreatureType CreatureType { get; set; }
    public int Age { get; set; }
    public double PersonReproduceRateByAge { get; set; }
    public double PersonBrokenRateByAge { get; set; }
    public PersonState PersonState { get; set; }
    public Person(PersonState personState)        
    {
        PersonState = personState;
    }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public bool IsDead { get; set; }
}
