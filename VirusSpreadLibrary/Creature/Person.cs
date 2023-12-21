using VirusSpreadLibrary.AppProperties;
using VirusSpreadLibrary.Creature.Rates;
using VirusSpreadLibrary.Enum;
using VirusSpreadLibrary.Grid;
using Point = System.Drawing.Point;

namespace VirusSpreadLibrary.Creature;

public class Person
{
    private readonly Random rnd = new ();
    
    private readonly PersMoveDistanceProfile persMoveProfile = new();   
    public Person()
    {
        PersMoveData = new()
        {
            CreatureType = Enum.CreatureType.Person
        };
        PersonState = new PersonState();
    }

    public int Age { get; set; } 
    public double PersonBirthRateByAge { get; set; }
    public double PersonDeathProbabilityByAge { get; set; }
    public PersonState PersonState { get; set; }
    public bool IsDead { get; set; }
    public MoveData PersMoveData { get; set; }
    
    public bool DoMove()
    {
        return (AppSettings.Config.PersonMoveActivityRnd != 0) &&
                (1 == rnd.Next(1, 1+(int)(AppSettings.Config.PersonMoveActivityRnd)));
    }
    public bool DoMoveHome()
    {
        return (AppSettings.Config.PersonMoveHomeActivityRnd != 0) &&
                (1 == rnd.Next(1, 1+(int)(AppSettings.Config.PersonMoveHomeActivityRnd)));
    }
    public void ChildBirth()
    {
     //    
    }
    public void SpreadVirus()
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
                PersonState.HealthState = PersonState.PersonRecoverd;
            }
            return;
        }

        if (healthCounter <= AppSettings.Config.PersonLatencyPeriod && PersonState.InfectionCounter < 1)
        {
            // LatencyPeriod - person infected
            PersonState.HealthState = PersonState.PersonInfected;
            ++PersonState.InfectionCounter;
        }
        
        if (healthCounter <= AppSettings.Config.PersonLatencyPeriod && PersonState.InfectionCounter > 0 )
        {
            // LatencyPeriod - person reinfected
            PersonState.HealthState = PersonState.PersonReinfected;
            ++PersonState.InfectionCounter;
        }

        if (healthCounter > AppSettings.Config.PersonLatencyPeriod 
            && healthCounter < AppSettings.Config.PersonInfectiousPeriod + AppSettings.Config.PersonLatencyPeriod)
        {
            // InfectiousPeriod - person infectious
            PersonState.HealthState = PersonState.PersonInfectious;
        }

        if (healthCounter > AppSettings.Config.PersonInfectiousPeriod + AppSettings.Config.PersonLatencyPeriod
            && healthCounter < AppSettings.Config.PersonReinfectionImmunityPeriod + AppSettings.Config.PersonInfectiousPeriod + AppSettings.Config.PersonLatencyPeriod)
        {
            // ReinfectionImmunityPeriod - person recoverd and immune
            PersonState.HealthState = PersonState.PersonRecoverdImmuneNotinfectious;
        }

        if (healthCounter > AppSettings.Config.PersonReinfectionImmunityPeriod + AppSettings.Config.PersonInfectiousPeriod + AppSettings.Config.PersonLatencyPeriod)
        {

            // new reinfection cycle random within PersonReinfectionRate
            int rate = 101 - (int)AppSettings.Config.PersonReinfectionRate;
            if (rate < 0) {rate = 0;}
            if (rate > 100) { rate = 100; }
            if ( 1 == rnd.Next(1,rate)) 
            {
                // person is recoverd and is reinfectable
                PersonState.HealthState = PersonState.PersonRecoverd;
                PersonState.HealthStateCounter = 0; 
            }
            else
            {
                // person is recoverd and is immune within PersonReinfectionRate
                PersonState.HealthState = PersonState.PersonRecoverdImmuneNotinfectious;
                // begin a new recoverd immunity period
                PersonState.HealthStateCounter = AppSettings.Config.PersonInfectiousPeriod + AppSettings.Config.PersonLatencyPeriod +1;
            }
        }
    }
    public void MoveToNewCoordinate(Grid.Grid Grid)
    {
        // get new random endpoint to move to
        // depending on the spcified range in settings of persMoveProfile and PersonMoveGlobal var
        if (AppSettings.Config.PersonMoveGlobal)
        {   
            // calculate next move from EndCoordinate of the last iteration, in the spcified range - moves over whole grid
            PersMoveData.EndGridCoordinate = persMoveProfile.GetEndCoordinateToMove(PersMoveData.StartGidCoordinate);
        } 
        else
        {   
            // calculate next move always from the Home Coordinate in the specified range - moves only within the range
            PersMoveData.EndGridCoordinate = persMoveProfile.GetEndCoordinateToMove(PersMoveData.HomeGridCoordinate);
        }

        // do move to endpoint
        SpreadModel.SetGridCellState.PersonMoveState(this,Grid);

        // save current endpoint as the new startpoint
        // to use in next iteration if VirusMoveGlobal is true
        PersMoveData.StartGidCoordinate = PersMoveData.EndGridCoordinate;

    }
    public void MoveToHomeCoordinate(Grid.Grid Grid)
    {
        PersMoveData.EndGridCoordinate = PersMoveData.HomeGridCoordinate;

        // do move to endpoint
        SpreadModel.SetGridCellState.PersonMoveState(this, Grid);

        // save current endpoint as the new startpoint
        PersMoveData.StartGidCoordinate = PersMoveData.EndGridCoordinate;
    }

}
