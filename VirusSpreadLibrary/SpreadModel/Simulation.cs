using Serilog;
using VirusSpreadLibrary.Creature;
using VirusSpreadLibrary.Properties;
using VirusSpreadLibrary.Grid;

namespace VirusSpreadLibrary.SpreadModel;

public class Simulation
{

    public Grid.Grid GridField = new Grid.Grid();
    public PersonList PersonList = new PersonList();
    public VirusList VirusList = new VirusList();

    public void StartIterate()
    {
        Log.Logger = Logging.getinstance();
        int iteration = 0;

        GridField.SetNewEmptyGrid(Settings.Default.GridMaxX, Settings.Default.GridMaxY);
        PersonList.SetInitialPopulation(Settings.Default.InitialPersonPopulation, GridField);
        VirusList.SetInitialPopulation(Settings.Default.InitialPersonPopulation, GridField);

        while (iteration < Settings.Default.maxIterations)
        {
            Log.Logger.Information("Nr: {A} iteration", iteration);
            iteration++;
            
            foreach (Person person in PersonList.Persons)
            {
                person.MoveToNewCoordinate(GridField);
            };

            // Parallel.ForEach(VirusList.Viruses, virus =>}); -> takes longer
            foreach (Virus virus in VirusList.Viruses)
            {
                virus.MoveToNewCoordinate(GridField);
            };           
        }
        
    }

}
