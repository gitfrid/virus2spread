
namespace VirusSpreadLibrary.Creature;

public class PersonList
{
    public  List<Person> Persons { get; set; }
    public PersonList()
    {
        Persons = new List<Person>();
    }
    public void SetInitialPopulation(long InitialPersonPopulation, Grid.Grid GridField)
    {
        Persons = new List<Person>();
        Random rnd = new Random();
        int maxX = GridField.ReturnMaxX();
        int maxY = GridField.ReturnMaxY();

        // create initial person list at random grid coordinates

        for (int i = 0; i < InitialPersonPopulation; i++)
        {
            Person person = new Person {};
            // use as new startpoit - if PersonMoveGlobal is true
            person.PersMoveData.StartGidCoordinate = new(rnd.Next(0, maxX), rnd.Next(0, maxY));

            // otherwise use always the home coordinate as same startpoit
            person.PersMoveData.HomeGridCoordinate = person.PersMoveData.StartGidCoordinate;
            Persons.Add(person);
        }
    }

}
