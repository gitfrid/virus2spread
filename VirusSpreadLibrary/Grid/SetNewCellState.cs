using Serilog;
using VirusSpreadLibrary.Creature;
using VirusSpreadLibrary.Enum;

namespace VirusSpreadLibrary.Grid;

public class SetNewCellState
{
    public CellPopulation SetNewState(MoveData CreatureData, Grid GridField) 
    {
        int x = CreatureData.NewGridCoordinate.X;
        int y = CreatureData.NewGridCoordinate.Y;

        GridCell cell = GridField.GridField[x, y];
        CreatureType creatureType = CreatureData.CreatureType;
        int numPersons = cell.Population.NumPersons;
        int numViruses = cell.Population.NumViruses;
        ColorList colorList = new ColorList();
        CellState cellState = new CellState();


        if (creatureType == CreatureType.Person)   
        {
            numPersons++;     
        } 
        else if (creatureType == CreatureType.Virus)
        {
            numViruses++;
        }

        // evaluate grid cell state
        switch ((numPersons, numViruses))
        {
            case ( < 1, < 1 ):
                cellState = CellState.EmptyCell;
                break;
            case ( < 1, > 0):
                cellState = CellState.Virus;
                break;
            case ( > 0, < 1 ):
                cellState = CellState.PersonHealthy;
                break;
            case ( > 0, > 0):
                cellState = CellState.PersonInfected;
                break;
        }

        // set Pixel colcor depending on cell state, and set the actual cell population number
        cell.Population.NumPersons = numPersons; 
        cell.Population.NumViruses = numViruses;
        cell.PixelColor = colorList.GetCellColor(cellState, cell.Population);

        return cell.Population;
    }



}
