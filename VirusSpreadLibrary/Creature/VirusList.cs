
using System;

namespace VirusSpreadLibrary.Creature;

public class VirusList
{
    public List<Virus> Viruses { get; set; }
    public VirusList()
    {
        Viruses = new List<Virus>();
    }
    public void SetInitialPopulation(long InitialVirusPopulation, Grid.Grid GridField)
    {
        Viruses = new List<Virus>();
        Random rnd = new();
        int maxX = GridField.ReturnMaxX();
        int maxY = GridField.ReturnMaxY();

        // create initial virus list at random grid coordinates
        for (int i = 0; i < InitialVirusPopulation; i++)
        {
            Virus virus = new() { };
            // use as new startpoit - if VirusMoveGlobal is true
            virus.VirMoveData.StartGidCoordinate = new(rnd.Next(0, maxX), rnd.Next(0, maxY));
            Viruses.Add(virus);

            // otherwise use always the home coordinate as same startpoit
            virus.VirMoveData.HomeGridCoordinate = virus.VirMoveData.StartGidCoordinate;
            Viruses.Add(virus);

        }
    }
}
