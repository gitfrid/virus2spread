using Serilog;
using VirusSpreadLibrary.Creature;
using VirusSpreadLibrary.Properties;
using VirusSpreadLibrary.Grid;
using Microsoft.Maui.Graphics;


namespace VirusSpreadLibrary.SpreadModel;

public class Simulation
{

    public Grid.Grid GridField;
    public PersonList PersonList = new PersonList();
    public VirusList VirusList = new VirusList();
    public int iteration;

    public bool stopIteration ;
    public int MaxX { get; set; }
    public int MaxY { get; set; }

    public Simulation()
    {
        MaxX = Settings.Default.GridMaxX;
        MaxY = Settings.Default.GridMaxY;
        GridField = new Grid.Grid();
    }
    public void StartIteration()
    {
        stopIteration = false;
        MaxX = Settings.Default.GridMaxX;
        MaxY = Settings.Default.GridMaxY;

        GridField = new Grid.Grid();
        GridField.SetNewEmptyGrid(MaxX,MaxY);
        PersonList.SetInitialPopulation(Settings.Default.InitialPersonPopulation, GridField);
        VirusList.SetInitialPopulation(Settings.Default.InitialVirusPopulation, GridField);

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


    public void DrawGrid(ICanvas canvas,float coordinateFactX, float coordinateFactY, float rectangleX, float rectangleY)
    {
        for (int y = 0; y < MaxY; y++)
        {
            for (int x = 0; x < MaxX; x++)
            {                
                GridCell Cell = GridField.GridField[x,y];
                canvas.FillColor = Cell.PixelColor;                
                canvas.FillRectangle(x * coordinateFactX , y * coordinateFactY , rectangleX, rectangleY);   
            }
        }                
    }



}
