﻿
namespace VirusSpreadLibrary.Creature;

public class PersonList
{
    public List<Person> Persons = new List<Person>();
    public List<Person> SetInitialPopulation(long InitialPersonPopulation, Grid.Grid GridField)
    {
        Random rnd = new Random();
        int maxX = GridField.ReturnMaxX();
        int maxY = GridField.ReturnMaxY();

        // create initial person list at random grid coordinates
        for (int i = 0; i < InitialPersonPopulation; i++)
        {
            Person person = new Person {};
            person.PersMoveData.OldGridCoordinate = new(rnd.Next(0, maxX), rnd.Next(0, maxY));
            person.HomeCoordinate = person.PersMoveData.OldGridCoordinate;

            Persons.Add(person);
        }
        return Persons;
    }
}
