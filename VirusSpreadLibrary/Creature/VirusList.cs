
using System;
using VirusSpreadLibrary.AppProperties;

namespace VirusSpreadLibrary.Creature;

public class VirusList
{
    public List<Virus> Viruses { get; set; }
    public VirusList()
    {
        Viruses = new List<Virus>();
    }
    public void SetInitialPopulation(long InitialVirusPopulation, Grid.Grid Grid)
    {
        Viruses = new List<Virus>();
        Random rnd = new();
        int maxX = Grid.ReturnMaxX();
        int maxY = Grid.ReturnMaxY();
        
        // to initialize population always move
        double tempVirusMoveActivityRnd = AppSettings.Config.VirusMoveActivityRnd;
        double tempVirusMoveHomeActivityRnd = AppSettings.Config.VirusMoveHomeActivityRnd;
        AppSettings.Config.VirusMoveActivityRnd = 1;
        AppSettings.Config.VirusMoveHomeActivityRnd = 0;

        // create initial virus list at random grid coordinates
        for (int i = 0; i < InitialVirusPopulation; i++)
        {
            Virus virus = new() { };
            // rand initial startpoit 
            virus.VirMoveData.StartGidCoordinate = new(rnd.Next(0, maxX), rnd.Next(0, maxY));
            Viruses.Add(virus);

            // if VirusMoveGlobal is false always use the home coordinate as same startpoit
            virus.VirMoveData.HomeGridCoordinate = virus.VirMoveData.StartGidCoordinate;
            Viruses.Add(virus);
        }

        // after initialize restore old MoveActivity values 
        AppSettings.Config.VirusMoveActivityRnd = tempVirusMoveActivityRnd;
        AppSettings.Config.VirusMoveHomeActivityRnd = tempVirusMoveHomeActivityRnd; ;
    }
}
