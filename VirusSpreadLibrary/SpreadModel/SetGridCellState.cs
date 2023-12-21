using VirusSpreadLibrary.Creature;
using VirusSpreadLibrary.Enum;
using VirusSpreadLibrary.AppProperties;
using VirusSpreadLibrary.Grid;
using System;

namespace VirusSpreadLibrary.SpreadModel;

public static class SetGridCellState
{
    private static GridCell cell = new();
    private static GridCell cellStart = new();

    private readonly static ColorList colorList = new();

    private static void SetNewCellState(GridCell Cell)
    {
        // evaluate new state of grid cell
        int numPersons = Cell.NumPersons();
        int numViruses = Cell.NumViruses();
      
        
        if (numPersons > 0)
        {
            bool personsInfectious = false;
            bool personsInfected = false;
            bool personRecoverdImmuneNotinfectious = false;

            for (int i = 0; i < numPersons; i++)
            {
                if (PersonState.PersonInfectious == Cell.PersonPopulation.Persons[i].PersonState.HealthState)
                {
                    personsInfectious = true;
                }
                if (PersonState.PersonReinfected == Cell.PersonPopulation.Persons[i].PersonState.HealthState || PersonState.PersonInfected == Cell.PersonPopulation.Persons[i].PersonState.HealthState)
                {
                    personsInfected = true;
                }
                
                
                
                if (PersonState.PersonRecoverdImmuneNotinfectious == Cell.PersonPopulation.Persons[i].PersonState.HealthState)
                {
                    personRecoverdImmuneNotinfectious = true;
                }
            }

            Cell.CellState = CellState.PersonsHealthyOrRecoverd;

            if (personRecoverdImmuneNotinfectious)
            {
                Cell.CellState = CellState.PersonsRecoverdImmuneNotInfectious;
            }

            if (personsInfected)
            {
                Cell.CellState = CellState.PersonsInfected;
            }
 
            if (personsInfectious || numViruses > 0)
            {
                Cell.CellState = CellState.PersonsInfectious;
            } 
        }

        if (numViruses < 1 && numPersons < 1)
        {
            Cell.CellState = CellState.EmptyCell;
        }

        if (numViruses > 0 && numPersons < 1) 
        {
            Cell.CellState = CellState.Virus;
        }       
    }

    public static void PersonMoveState(Person MovingPerson, Grid.Grid Grid)
    {

        // add, moving virus or person to the end grid coordiante
        int xEnd = MovingPerson.PersMoveData.EndGridCoordinate.X;
        int yEnd = MovingPerson.PersMoveData.EndGridCoordinate.Y;

        // remove virus or person from start grid coordiante
        // after it has moved to end coordinate
        int xStart = MovingPerson.PersMoveData.StartGidCoordinate.X;
        int yStart = MovingPerson.PersMoveData.StartGidCoordinate.Y;

        cell = Grid.Cells[xEnd, yEnd];

        // exit if not moved
        if (xStart == xEnd & yStart == yEnd)
        {
            return;
        }

        cell.AddPerson(MovingPerson);

        
        // set new end cell sate
        SetNewCellState(cell);

        // if cell contains infectious person or virus, then infect all Persons on this cell
        int numPersons = cell.NumPersons();
        if (cell.CellState == CellState.PersonsInfectious) 
        {
            for (int i = 0; i < numPersons; i++)
            {
                cell.PersonPopulation.Persons[i].InfectPerson();
            }
            SetNewCellState(cell);
        }

        // set new end cell color depending on cell state
        cell.CellColor = colorList.GetCellColor(cell.CellState, cell.NumPersons(), cell.NumViruses());

        // delete person from start grid coordinate
        GridCell cellStart = Grid.Cells[xStart, yStart];
        cellStart.RemovePerson(MovingPerson);

        // set new sart cell sate
        SetNewCellState(cellStart);

        // if TrackMovment true leave old color, else set new sart cell color          
        if (AppSettings.Config.TrackMovment == false)
        {
            cellStart.CellColor = colorList.GetCellColor(cellStart.CellState, cellStart.NumPersons(), cellStart.NumViruses());
        }
    }

    public static void VirusMoveState(Virus MovingVirus, Grid.Grid Grid)
    {

        // add, moving virus or person to the end grid coordiante
        int xEnd = MovingVirus.VirMoveData.EndGridCoordinate.X;
        int yEnd = MovingVirus.VirMoveData.EndGridCoordinate.Y;

        // remove virus or person from start grid coordiante
        // after it has moved to end coordinate
        int xStart = MovingVirus.VirMoveData.StartGidCoordinate.X;
        int yStart = MovingVirus.VirMoveData.StartGidCoordinate.Y;

        cell = Grid.Cells[xEnd, yEnd];

        // exit if not moved
        if (xStart == xEnd & yStart == yEnd)
        {
            return;
        }

        cell.AddVirus(MovingVirus);

        // set new end cell sate
        SetNewCellState(cell);

        // if cell contains infectious person or virus, then infect all Persons on this cell
        int numPersons = cell.NumPersons();
        if (cell.CellState == CellState.PersonsInfectious)
        {
            for (int i = 0; i < numPersons; i++)
            {
                cell.PersonPopulation.Persons[i].InfectPerson();
            }
            SetNewCellState(cell);
        }

        // set new end cell color depending on cell state
        cell.CellColor = colorList.GetCellColor(cell.CellState, cell.NumPersons(), cell.NumViruses());

        // delete virus from start grid coordinate
        GridCell cellStart = Grid.Cells[xStart, yStart];
        cellStart.RemoveVirus(MovingVirus);

        // set new sart cell sate
        SetNewCellState(cellStart);
      
        // if TrackMovment true leave old color, else set new sart cell color          
        if (AppSettings.Config.TrackMovment == false)
        {
            cellStart.CellColor = colorList.GetCellColor(cellStart.CellState, cellStart.NumPersons(), cellStart.NumViruses());
        }
    }
}
