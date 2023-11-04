using Serilog;
using VirusSpreadLibrary.Creature;
using VirusSpreadLibrary.Properties;
namespace VirusSpreadLibrary.SpreadModel;

public class Simulation
{   
    public void StartIterate()
    {
        
        Log.Logger = Logging.getinstance();
        int iteration = 0;

        Grid.Grid GridField = new(Settings.Default.GridMaxX, Settings.Default.GridMaxY);

        VirusList virs = new VirusList();
        List<Virus> viruses = virs.SetInitialPopulation(Settings.Default.InitialPersonPopulation, GridField);
        

        while (iteration < Settings.Default.maxIterations)
        {
            Log.Logger.Information("Nr: {A} iteration", iteration);
            iteration++;

            //Parallel.ForEach(Persons, person =>
            //{
            //    person.MoveToNewCoordinate(GridField);
            //});
            //Parallel.ForEach(viruses, virus =>
            //{
            //    virus.MoveToNewCoordinate(GridField);
            //});

            foreach (var virus in viruses) 
            {
                virus.MoveToNewCoordinate(GridField);
            }

        }
    }
}
