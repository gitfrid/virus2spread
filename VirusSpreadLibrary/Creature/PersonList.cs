
namespace VirusSpreadLibrary.Creature;

public class PersonList
{
    public List<Person> Persons = new List<Person>();
    public List<Person> SetInitialPopulation(long InitialVirusPopulation, Grid.Grid GridField)
    {
        Random rnd = new Random();

        // create initial person list at random grid coordinates
        for (int i = 0; i < InitialVirusPopulation; i++)
        {
            Person person = new Person {};
            person.PersMoveData.OldGridCoordinate = new(rnd.Next(0, GridField.ReturnMaxX()), rnd.Next(0, GridField.ReturnMaxX()));
            Persons.Add(person);
        }
        return Persons;
    }
}
