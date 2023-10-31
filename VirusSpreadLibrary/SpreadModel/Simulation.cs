using Serilog;


namespace VirusSpreadLibrary.SpreadModel;

public class Simulation
{
    int gridX;
    int gridY;
    long maxIterations;
    

    public Simulation(int GridMaxX, int GridMaxY, long MaxIterations)
    {
        gridX = GridMaxX;
        gridY = GridMaxY;
        maxIterations = MaxIterations;
    }
    public void StartIterate()
    {

        Log.Logger = Logging.getinstance();
        int iteration = 0;
        
        Grid.Grid GridField = new Grid.Grid(gridX, gridY);

        while (iteration < maxIterations)
        {
            Log.Logger.Information("Nr: {A} iteration" , iteration);

            iteration++;
        }
    }

}
