
namespace VirusSpreadLibrary.Creature;

public class VirusList
{
    public List<Virus> Viruses = new List<Virus>();
    public List<Virus> SetInitialPopulation(long InitialVirusPopulation, Grid.Grid GridField)
    {
        Random rnd = new Random();

        // create initial person list at random grid coordinates
        for (int i = 0; i < InitialVirusPopulation; i++)
        {
            Virus virus = new Virus { };
            virus.VirMoveData.OldGridCoordinate = new(rnd.Next(0, GridField.ReturnMaxX()), rnd.Next(0, GridField.ReturnMaxY()));
            Viruses.Add(virus);
        }
        return Viruses;
    }
}
