using VirusSpreadLibrary.Creature;
using VirusSpreadLibrary.Enum;
using VirusSpreadLibrary.AppProperties;

namespace VirusSpreadLibrary.Grid;

public class SetCellState
{
    public CellPopulation SetNewState(MoveData CreatureData, Grid GridField) 
    {
        
        // add, moving virus or person to the end grid coordiante
        int xEnd = CreatureData.EndGridCoordinate.X;
        int yEnd = CreatureData.EndGridCoordinate.Y;

        // remove virus or person from start grid coordiante
        // after it has moved to end coordinate
        int xStart = CreatureData.StartGidCoordinate.X;
        int yStart = CreatureData.StartGidCoordinate.Y;
       
        GridCell cell = GridField.GridField[xEnd, yEnd];

        // exit if not moved
        if ((xStart == xEnd) & (yStart == yEnd))
        {
            return cell.Population;
        }

        CreatureType creatureType = CreatureData.CreatureType;
        int numPersons = cell.Population.NumPersons;
        int numViruses = cell.Population.NumViruses;
        ColorList colorList = new();
        CellState cellState = new();

        if (creatureType == CreatureType.Person)   
        {
            numPersons++;     
        } 
        else if (creatureType == CreatureType.Virus)
        {
            numViruses++;
        }

        // evaluate new state of end grid cell 
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

        // set new pixel color and population of end cell
        // depending on cell state
        cell.Population.NumPersons = numPersons; 
        cell.Population.NumViruses = numViruses;
        cell.PixelColor = colorList.GetCellColor(cellState, cell.Population);

        // delete virus or person from start grid coordinate
        GridCell cellStart = GridField.GridField[xStart, yStart];
        int numPersonsStart = cellStart.Population.NumPersons;
        int numVirusesStart = cellStart.Population.NumViruses;

        if (creatureType == CreatureType.Person)
        {
            numPersonsStart--;
        }
        else if (creatureType == CreatureType.Virus)
        {
            numVirusesStart--;
        }

        if (numPersonsStart < 0) { numPersonsStart = 0; }
        if (numVirusesStart < 0) { numVirusesStart = 0; }

        // evaluate new state of start grid cell
        switch ((numPersonsStart, numVirusesStart))
        {
            case ( < 1, < 1):
                cellState = CellState.EmptyCell;
                break;
            case ( < 1, > 0):
                cellState = CellState.Virus;
                break;
            case ( > 0, < 1):
                cellState = CellState.PersonHealthy;
                break;
            case ( > 0, > 0):
                cellState = CellState.PersonInfected;
                break;
        }

        // set new population number of start cell
        cellStart.Population.NumPersons = numPersonsStart;
        cellStart.Population.NumViruses = numVirusesStart;       

        // leave old start pixel color, if TrackMovment true          
        if ( AppSettings.Config.TrackMovment == false)
        {
            cellStart.PixelColor = colorList.GetCellColor(cellState, cell.Population);
        }

        return cell.Population;
    }


}
