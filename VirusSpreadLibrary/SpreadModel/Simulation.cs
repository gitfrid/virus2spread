using Serilog;
using VirusSpreadLibrary.Creature;
using VirusSpreadLibrary.AppProperties;
using VirusSpreadLibrary.Grid;
using Microsoft.Maui.Graphics;
using System;
using VirusSpreadLibrary.Plott;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


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
        plotData.IterationNumber = iteration;

        long personAgeMean = 0;
        long personsMoveDistanceMean = 0;
        plotData.PersonPopulation = personList.Persons.Count;
        foreach (Person person in personList.Persons)
        {
            person.Age++;
            personAgeMean += person.Age;

            // get health state if is infected
            if (person.PersonState.HealthStateCounter != 0)
            {
                person.PersonState.HealthStateCounter++;
                person.SetPersonHealthState();
            }           
            // move person and change health state if get infected
            if (person.DoMove())
            {
                System.Drawing.Point startPoint = person.PersMoveData.StartGidCoordinate;
                if (person.DoMoveHome())
                {
                    person.MoveToHomeCoordinate(grid);
                }
                else 
                {
                    person.MoveToNewCoordinate(grid);
                }
                System.Drawing.Point endPoint = person.PersMoveData.EndGridCoordinate;
                int dx = endPoint.X - startPoint.X;
                int dy = endPoint.Y - startPoint.Y;
                double SE = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
                personsMoveDistanceMean += Convert.ToInt64(SE);
            }
            plotData.SetPersonHealthState(person.PersonState);
        };



        // Parallel.ForEach(VirusList.Viruses, virus =>}); -> takes longer
        long virusAgeMean = 0;
        long virusesMoveDistanceMean = 0;
        plotData.VirusPopulation = virusList.Viruses.Count;
        foreach (Virus virus in virusList.Viruses)
        {
            virus.Age++;
            virusAgeMean += virus.Age;
            
            if (virus.DoMove())
            {
                System.Drawing.Point startPoint = virus.VirMoveData.StartGidCoordinate;

                if (virus.DoMoveHome())
                {
                    virus.MoveToHomeCoordinate(grid);
                }
                else
                {
                    virus.MoveToNewCoordinate(grid);
                }
                System.Drawing.Point endPoint = virus.VirMoveData.EndGridCoordinate;
                int dx = endPoint.X - startPoint.X;
                int dy = endPoint.Y - startPoint.Y;
                double SE = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
                virusesMoveDistanceMean += Convert.ToInt64(SE);                
            }            
            
        };
        
        if (plotData.VirusPopulation > 0) 
        {
            plotData.VirusesAge = (virusAgeMean / plotData.VirusPopulation);
            plotData.VirusesMoveDistance = (virusesMoveDistanceMean / plotData.VirusPopulation);
        }

        if (plotData.PersonPopulation > 0)
        {
            plotData.PersonsAge = (personAgeMean / plotData.PersonPopulation);
            plotData.PersonsMoveDistance = (personsMoveDistanceMean / plotData.PersonPopulation);
            plotData.PersonsInfectionCounter /= plotData.PersonPopulation;
        }

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
