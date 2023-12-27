using Serilog;
using VirusSpreadLibrary.Creature;
using VirusSpreadLibrary.AppProperties;
using VirusSpreadLibrary.Grid;
using Microsoft.Maui.Graphics;
using System;


namespace VirusSpreadLibrary.SpreadModel;

public class Simulation
{

    private readonly Grid.Grid grid;
    private readonly PersonList personList = new ();
    private readonly VirusList virusList = new ();
    private int iteration;
    private PlotDataCsv plotData;

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
        plotData = new();
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
            person.Age++;
            
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

                plotData.IterationNumber = iteration;
                ++plotData.PersonPopulation;
                plotData.PersonsAge += person.Age;
                plotData.SetPersonHealthState(person.PersonState);

                int dx = endPoint.X - startPoint.X;
                int dy = endPoint.Y - startPoint.Y;
                double SE = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
                plotData.PersonsMoveDistance += Convert.ToInt64(SE);
            }
            plotData.IterationNumber = iteration;
            ++plotData.PersonPopulation;
            plotData.PersonsAge += person.Age;
            plotData.SetPersonHealthState(person.PersonState);
        };

        // Parallel.ForEach(VirusList.Viruses, virus =>}); -> takes longer
        foreach (Virus virus in virusList.Viruses)
        {
            virus.Age++;

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
                plotData.VirusesMoveDistance += Convert.ToInt64(SE);
            }
            ++plotData.VirusPopulation;
            plotData.VirusesAge += virus.Age;
        };
        plotData.WriteToCsv();
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
