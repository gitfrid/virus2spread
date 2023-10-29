namespace VirusSpreadLibrary.SpreadModel;

public class Simulation
{
    int _gridX ;
    int _gridY ;
    int _maxIterations ;

    public Simulation(int GridMaxX, int GridMaxY, int MaxIterations)
    {
        _gridX = GridMaxX;
        _gridY = GridMaxY;
        _maxIterations = MaxIterations;
    }
    public void StartIterate()
    {
               
        int iteration = 0;

        Grid.Grid GridField = new Grid.Grid(_gridX, _gridY);

        while (iteration < _maxIterations)
        {
            Console.WriteLine(iteration);
            iteration++;
        }
    }

}
