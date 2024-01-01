using Serilog;
using VirusSpreadLibrary.Creature;
using VirusSpreadLibrary.AppProperties;
using VirusSpreadLibrary.Grid;
using Microsoft.Maui.Graphics;
using VirusSpreadLibrary.Plott;



namespace VirusSpreadLibrary.SpreadModel;

public class Simulation
{

    private readonly Grid.Grid grid;
    private readonly PersonList personList = new ();
    private readonly VirusList virusList = new ();
    private int iteration;
    
    readonly private PlotData plotData = new ();
    // public prop to access the queue
    
    public PlotData PlotData 
    { 
        get => plotData;
    }

    private bool stopIteration ;
    public Simulation()
    {
        stopIteration = true;
        MaxX = AppSettings.Config.GridMaxX;
        MaxY = AppSettings.Config.GridMaxY;
        grid = new Grid.Grid();
        grid.SetNewEmptyGrid(MaxX, MaxY);
        personList.SetInitialPopulation(AppSettings.Config.InitialPersonPopulation, grid);
        virusList.SetInitialPopulation(AppSettings.Config.InitialVirusPopulation, grid);    
    }

    public int MaxX { get; set; }
    public int MaxY { get; set; }
    public int Iteration { get => iteration; }

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

        Log.Logger = Logging.GetInstance();        
        Log.Logger.Information("Nr: {A} iteration", iteration);
       
        iteration++;
        plotData.IterationNumber = iteration;

        long personAgeCum = 0;
        double personsMoveDistanceCum = 0;
        plotData.PersonPopulation = personList.Persons.Count;
        foreach (Person person in personList.Persons)
        {
            person.Age++;
            personAgeCum += person.Age;

            // increase health state counter if is infected
            if (person.PersonState.HealthStateCounter != 0)
            {
                person.PersonState.HealthStateCounter++;
                person.SetPersonHealthState();
            }           
            // move person and change health state if get infected
            if (person.DoMove())
            {
                System.Drawing.Point startPoint = person.StartGridCoordinate;
                if (person.DoMoveHome())
                {
                    person.MoveToHomeCoordinate(grid);
                }
                else 
                {
                    person.MoveToNewCoordinate(grid);
                }
                // calc move distance
                System.Drawing.Point endPoint = person.EndGridCoordinate;
                int dx = endPoint.X - startPoint.X;
                int dy = endPoint.Y - startPoint.Y;
                double SE = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
                personsMoveDistanceCum += SE;
            }
            // set new health state for plotting
            plotData.SetPersonHealthState(person.PersonState);
        };



        // Parallel.ForEach(VirusList.Viruses, virus =>}); -> takes longer
        long virusAgeCum = 0;
        double virusesMoveDistanceCum = 0;
        plotData.VirusPopulation = virusList.Viruses.Count;
        foreach (Virus virus in virusList.Viruses)
        {
            virus.Age++;
            virusAgeCum += virus.Age;
            
            if (virus.DoMove())
            {
                System.Drawing.Point startPoint = virus.StartGridCoordinate;

                if (virus.DoMoveHome())
                {
                    virus.MoveToHomeCoordinate(grid);
                }
                else
                {
                    virus.MoveToNewCoordinate(grid);
                }
                System.Drawing.Point endPoint = virus.EndGridCoordinate;
                int dx = endPoint.X - startPoint.X;
                int dy = endPoint.Y - startPoint.Y;
                double SE = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
                virusesMoveDistanceCum += SE;         
            }                      
        };

        if (plotData.PersonPopulation > 0)
        {
            plotData.PersonsAge = personAgeCum / plotData.PersonPopulation;
            plotData.PersonsMoveDistance = personsMoveDistanceCum / plotData.PersonPopulation;
            plotData.PersonsInfectionCounter /= plotData.PersonPopulation;
        }

        if (plotData.VirusPopulation > 0) 
        {
            plotData.VirusesAge = virusAgeCum / plotData.VirusPopulation; //<- cumulated age
            plotData.VirusesMoveDistance = virusesMoveDistanceCum / plotData.VirusPopulation; //<- cumulated move distance
        }
        // write data to queue for plotting and reset queue
        plotData.WriteToQueue();
        plotData.ResetCounter();
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
