using Serilog;
using VirusSpreadLibrary.Creature;
using VirusSpreadLibrary.AppProperties;
using VirusSpreadLibrary.Grid;
using Microsoft.Maui.Graphics;


namespace VirusSpreadLibrary.SpreadModel;

public class Simulation
{

    private readonly Grid.Grid grid;
    private readonly PersonList personList = new ();
    private readonly VirusList virusList = new ();
    private int iteration;
    private bool stopIteration ;
    public Simulation()
    {
        MaxX = AppSettings.Config.GridMaxX;
        MaxY = AppSettings.Config.GridMaxY;
        grid = new Grid.Grid();
    }

    public int MaxX { get; set; }
    public int MaxY { get; set; }
    public int Iteration { get => iteration; }

    public void Initialize()
    {
        MaxX = AppSettings.Config.GridMaxX;
        MaxY = AppSettings.Config.GridMaxY;
        grid.SetNewEmptyGrid(MaxX, MaxY);
        personList.SetInitialPopulation(AppSettings.Config.InitialPersonPopulation, grid);
        virusList.SetInitialPopulation(AppSettings.Config.InitialVirusPopulation, grid);
    }
    public void StartIteration()
    {
        stopIteration = false;
    }
    public void StopIteration()
    {
        stopIteration = true;
    }
    public void NextIteration()
    {
        if (stopIteration == true) { return; }

        Log.Logger = Logging.getinstance();        
        Log.Logger.Information("Nr: {A} iteration", iteration);
        iteration++;

        foreach (Person person in personList.Persons)
        {
            if (person.DoMove())
            {
                if (person.DoMoveHome())
                {
                    person.MoveToHomeCoordinate(grid);
                }
                else 
                {
                    person.MoveToNewCoordinate(grid);
                }                
            }                
        };

        // Parallel.ForEach(VirusList.Viruses, virus =>}); -> takes longer
        foreach (Virus virus in virusList.Viruses)
        {
            if (virus.DoMove())
            {
                if (virus.DoMoveHome())
                {
                    virus.MoveToHomeCoordinate(grid);
                }
                else
                {
                    virus.MoveToNewCoordinate(grid);
                }
            }

        };
    }

    // first initialize grid!
    public void DrawGrid(ICanvas canvas,float coordinateFactX, float coordinateFactY, float rectangleX, float rectangleY)
    {
        for (int y = 0; y < MaxY; y++)
        {
            for (int x = 0; x < MaxX; x++)
            {
                GridCell Cell = grid.Cells[x, y];
                canvas.FillColor = Cell.CellColor;                
                canvas.FillRectangle(x * coordinateFactX , y * coordinateFactY , rectangleX, rectangleY);   
            }
        }                
    }

}
