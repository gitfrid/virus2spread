using VirusSpreadLibrary.AppProperties;
using VirusSpreadLibrary.Creature.Rates;
using VirusSpreadLibrary.SpreadModel;


namespace VirusSpreadLibrary.Creature;

public class Person
{
    private readonly Random rnd = new ();
    
    private readonly PersMoveDistanceProfile persMoveProfile = new(); 
    
    private PersonState personState;
    public int Age { get; set; } 
    public double PersonBirthRateByAge { get; set; }
    public double PersonDeathProbabilityByAge { get; set; }
    public bool IsDead { get; set; }

    // move data
    public Enum.CreatureType CreatureType = Enum.CreatureType.Person;
    public Point StartGridCoordinate { get; private set; }
    public Point EndGridCoordinate { get; private set; }
    public Point HomeGridCoordinate { get; private set; }

    public PersonState PersonState
    {
        get => personState;
        set => personState = value;
    }

    public Person()
    {
        personState = new PersonState();
    }
    public bool DoMove()
    {
        // move within PersonMoveActivityRnd percentage, 0=dont 100=always
        int moveActivity = AppSettings.Config.PersonMoveActivityRnd;
        if (moveActivity < 0) { moveActivity = 0; }
        if (moveActivity == 0 || rnd.Next(1, moveActivity + 1) > 1) 
            return false;
        return true;
    }
    public bool DoMoveHome()
    {
        // move home within PersonMoveHomeActivityRnd percentage, 0=dont 100=always
        int moveActivity = AppSettings.Config.PersonMoveHomeActivityRnd;
        if (moveActivity < 0) { moveActivity = 0; }
        if (moveActivity == 0 || rnd.Next(1, moveActivity + 1) > 1) return false;
        return true;
    }
    public bool DoReinfect()
    {
        // reinfection within PersonMoveHomeActivityRnd percentage (decimal between 0-100%)
        // 0 = never true, 50 = from 100 approximate 50 times true, 100 = always true
        double randomProbability = AppSettings.Config.PersonReinfectionRate;
        if (randomProbability  >= 0 && randomProbability <= 100)
        {
            double randomNumber = rnd.NextDouble() * 100;
            if (randomNumber <= randomProbability)
            {
                return true;
            }
            else 
            { 
                return false; 
            }
        }
        else
        {
            // wrong input from AppSettings - should not happen
            throw new PersonReinfectionRateInputException("");  
        }
    }
    public static void ChildBirth()
    {
      //    
    }
    public static void SpreadVirus()
    {
      //
    }
    public void InfectPerson()
    {
        PersonState.SetInfected();
    }
    public void SetPersonHealthState()
    {
        int healthCounter = PersonState.HealthStateCounter;

        if (healthCounter == 0 )
        {
            // person healthy or recoverd
            if (PersonState.InfectionCounter < 1) 
            {
                PersonState.HealthState = PersonState.PersonHealthy;
            }
            else 
            {
                PersonState.HealthState = PersonState.PersonHealthyRecoverd;
            }
            return;
        }

        if (healthCounter <= AppSettings.Config.PersonLatencyPeriod)
        {
            // LatencyPeriod - person infected
            if (PersonState.InfectionCounter < 1)
            {
                PersonState.HealthState = PersonState.PersonInfected;
            }
            else 
            {
                PersonState.HealthState = PersonState.PersonReinfected;
            }
            ++PersonState.InfectionCounter;
        }
        
        if (healthCounter > AppSettings.Config.PersonLatencyPeriod 
            && healthCounter <= AppSettings.Config.PersonInfectiousPeriod + AppSettings.Config.PersonLatencyPeriod)
        {
            // InfectiousPeriod - person infectious
            PersonState.HealthState = PersonState.PersonInfectious;
        }

        if (healthCounter > AppSettings.Config.PersonInfectiousPeriod + AppSettings.Config.PersonLatencyPeriod
            && healthCounter <= AppSettings.Config.PersonReinfectionImmunityPeriod 
            + AppSettings.Config.PersonInfectiousPeriod + AppSettings.Config.PersonLatencyPeriod)
        {
            // ReinfectionImmunityPeriod - person recoverd and immune
            PersonState.HealthState = PersonState.PersonRecoverdImmunePeriodNotInfectious;
        }

        if (healthCounter > AppSettings.Config.PersonReinfectionImmunityPeriod 
            + AppSettings.Config.PersonInfectiousPeriod + AppSettings.Config.PersonLatencyPeriod)
        {
            if (DoReinfect())
            {
                // person is recoverd and is reinfectable
                PersonState.HealthState = PersonState.PersonHealthyRecoverd;
                PersonState.HealthStateCounter = 0; 
            }
            else
            {
                // person is recoverd and is immune within PersonReinfectionRate and is not infectious
                PersonState.HealthState = PersonState.PersonRecoverdImmunePeriodNotInfectious;
                // begin a new recoverd immunity period
                PersonState.HealthStateCounter = AppSettings.Config.PersonInfectiousPeriod + AppSettings.Config.PersonLatencyPeriod +1;
            }
        }
    }

    public void InitializePersonMoveToGrid(Grid.Grid Grid)
    {
        int maxX = Grid.ReturnMaxX();
        int maxY = Grid.ReturnMaxY();

        // random initial move endcoordiante = homeCoordinate
        EndGridCoordinate = new(rnd.Next(0,maxX ), rnd.Next(0, maxY));
        HomeGridCoordinate = EndGridCoordinate;

        do // must be differ from from end coordiante, otherwise creature does not move 
        {
           StartGridCoordinate = new(rnd.Next(0, maxX), rnd.Next(0, maxY));
        } while (StartGridCoordinate == EndGridCoordinate);
        
        // initalize move to the home cell, add the person at (home) endcoordiante
        // but dont remove from the start coordinate, as in the intitialize case there is no person
        SpreadModel.SetGridCellState.AddPersonToNewEndGridCoordinate(this, Grid);
        
        // save as new current coordinate
        StartGridCoordinate = EndGridCoordinate;
    }

    public void MoveToNewCoordinate(Grid.Grid Grid)
    {
        // get new random endpoint to move to
        // depending on the spcified range in settings of persMoveProfile and PersonMoveGlobal var
        if (AppSettings.Config.PersonMoveGlobal)
        {   
            // calculate next move from EndCoordinate of the last iteration, in the spcified range - moves over whole grid
            EndGridCoordinate = persMoveProfile.GetEndCoordinateToMove(StartGridCoordinate);
        } 
        else
        {   
            // calculate next move always from the Home Coordinate in the specified range - moves only within the range
            EndGridCoordinate = persMoveProfile.GetEndCoordinateToMove(HomeGridCoordinate);
        }

        // do move to endpoint end cell on grid and set cell state and population counter
        SpreadModel.SetGridCellState.PersonMoveState(this,Grid);

        // save as new current coordiante
        StartGridCoordinate = EndGridCoordinate;
    }
    public void MoveToHomeCoordinate(Grid.Grid Grid)
    {
        EndGridCoordinate = HomeGridCoordinate;

        // do move to home coordinate
        SpreadModel.SetGridCellState.PersonMoveState(this, Grid);

        // save current endpoint as new current 
        StartGridCoordinate = EndGridCoordinate;
    }

}
