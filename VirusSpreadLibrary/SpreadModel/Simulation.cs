﻿using Serilog;
using VirusSpreadLibrary.Creature;
using VirusSpreadLibrary.AppProperties;
using VirusSpreadLibrary.Grid;
using Microsoft.Maui.Graphics;


namespace VirusSpreadLibrary.SpreadModel;

public class Simulation
{

    public Grid.Grid GridField;
    public PersonList PersonList = new ();
    public VirusList VirusList = new ();
    public int iteration;

    public bool stopIteration ;
    public int MaxX { get; set; }
    public int MaxY { get; set; }
    public int Iteration { get => iteration; }

    public Simulation()
    {
        MaxX = AppSettings.Config.GridMaxX;
        MaxY = AppSettings.Config.GridMaxY;
        GridField = new Grid.Grid();
    }
    public void StartIteration()
    {
        stopIteration = false;
        MaxX = AppSettings.Config.GridMaxX;
        MaxY = AppSettings.Config.GridMaxY;

        GridField = new Grid.Grid();
        GridField.SetNewEmptyGrid(MaxX,MaxY);
        PersonList.SetInitialPopulation(AppSettings.Config.InitialPersonPopulation, GridField);
        VirusList.SetInitialPopulation(AppSettings.Config.InitialVirusPopulation, GridField);

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
