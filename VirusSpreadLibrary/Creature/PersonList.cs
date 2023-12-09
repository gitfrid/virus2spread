
using VirusSpreadLibrary.AppProperties;

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
        Random rnd = new();
        int maxX = GridField.ReturnMaxX();
        int maxY = GridField.ReturnMaxY();

        // to initialize population always move
        double tempPersonMoveActivityRnd = AppSettings.Config.PersonMoveActivityRnd;
        double tempPersonMoveHomeActivityRnd = AppSettings.Config.PersonMoveHomeActivityRnd;
        AppSettings.Config.PersonMoveActivityRnd = 1;
        AppSettings.Config.PersonMoveHomeActivityRnd = 0;

        // create initial person list at random grid coordinates
        for (int i = 0; i < InitialPersonPopulation; i++)
        {
            Person person = new() { };
            // random initial startpoit 
            person.PersMoveData.StartGidCoordinate = new(rnd.Next(0, maxX), rnd.Next(0, maxY));

            // if PersonMoveGlobal is false always use the home coordinate as same startpoit
            person.PersMoveData.HomeGridCoordinate = person.PersMoveData.StartGidCoordinate;
            Persons.Add(person);
        }

        // after initialize restore old MoveActivity values 
        AppSettings.Config.PersonMoveActivityRnd = tempPersonMoveActivityRnd;
        AppSettings.Config.PersonMoveHomeActivityRnd = tempPersonMoveHomeActivityRnd;
    }

}
