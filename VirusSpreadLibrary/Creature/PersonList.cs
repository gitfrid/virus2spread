﻿
using VirusSpreadLibrary.AppProperties;

namespace VirusSpreadLibrary.Creature;

public class PersonList
{
    public  List<Person> Persons { get; set; }
    public PersonList()
    {
        Persons = new List<Person>();
    }
    public void SetInitialPopulation(long InitialPersonPopulation, Grid.Grid Grid)
    {
        Persons = new List<Person>();
        Random rnd = new();
        int maxX = Grid.ReturnMaxX();
        int maxY = Grid.ReturnMaxY();

        // to initialize population always move
        int tempPersonMoveActivityRnd = AppSettings.Config.PersonMoveActivityRnd;
        int tempPersonMoveHomeActivityRnd = AppSettings.Config.PersonMoveHomeActivityRnd;
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