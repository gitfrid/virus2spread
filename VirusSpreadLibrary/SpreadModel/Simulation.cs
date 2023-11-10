using Serilog;
using VirusSpreadLibrary.Creature;
using VirusSpreadLibrary.Properties;
using VirusSpreadLibrary.Grid;
using Microsoft.Maui.Graphics;
using OpenTK;

namespace VirusSpreadLibrary.SpreadModel;

public class Simulation
{

    public  static Grid.Grid GridField = new Grid.Grid();
    public static PersonList PersonList = new PersonList();
    public static VirusList VirusList = new VirusList();
    public static int maxX;
    public static int maxY;
    public static int iteration = 0;
    //private static Random rand = new Random();
    public void StartIterate()
    {
        Log.Logger = Logging.getinstance();
        //int iteration = 0;

        maxX = Settings.Default.GridMaxX;
        maxY = Settings.Default.GridMaxY;


        GridField.SetNewEmptyGrid(maxX,maxY);
        PersonList.SetInitialPopulation(Settings.Default.InitialPersonPopulation, GridField);
        VirusList.SetInitialPopulation(Settings.Default.InitialVirusPopulation, GridField);

        //while (iteration < Settings.Default.maxIterations)
        //{
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
        

        //}

    }

    public static void NextIterate()
    {
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


    public static void DrawGrid(ICanvas canvas, float width, float height)
    {
        //canvas.FillColor = Col.WithHue(0.6f);
        
        float cellWidthPx = width / GridField.ReturnMaxX();
        float cellHeightPx = height / GridField.ReturnMaxY();

        float borderFrac = .1f;
        float xPad = borderFrac * cellWidthPx;
        float yPad = borderFrac * cellHeightPx;

        //int a = rand.Next(254);
        //byte r = (byte)a;
        //a = rand.Next(254);
        //byte b = (byte)a;
        //a = rand.Next(254);
        //byte g = (byte)a;

        //Microsoft.Maui.Graphics.Color dColor = new Microsoft.Maui.Graphics.Color();
        //dColor = Microsoft.Maui.Graphics.Color.FromRgb(r, b, g);
        //canvas.FillColor = dColor.WithHue(0.6f);


        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {                
                GridCell Cell = GridField.GridField[x,y];
                canvas.FillColor = ColorExtensions.ToMauiColor(Cell.PixelColor);
                var vx = x * cellWidthPx + xPad;
                var vy = y * cellHeightPx + yPad;
                var vw = cellWidthPx - xPad * 2;
                var vh =cellHeightPx - yPad * 2;
                canvas.FillRectangle(vx, vy, vw, vh);   
            }
        }                
    }

    public static void InitGridCanvas(ICanvas canvas, int width, int height)
    {
        canvas.FillRectangle(0, 0, (float)width, (float)height);
    }


}
