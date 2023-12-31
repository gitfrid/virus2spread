
namespace VirusSpreadLibrary.Creature;

public class PersonState
{
    private int healthStateCounter = 0;
    public const int PersonHealthy = 0;
    public const int PersonHealthyRecoverd = 1;
    public const int PersonInfected = 2;
    public const int PersonReinfected = 3;
    public const int PersonInfectious = 4;
    public const int PersonRecoverdImmunePeriodNotInfectious = 5; // -> does this apply to reality?

    public int HealthStateCounter
    {
        get
        {
            return healthStateCounter;
        }
        set
        {
            if (healthStateCounter != 0) 
            {
                healthStateCounter = value;
            }            
        }
    }
    public void SetInfected() 
    {
        if (healthStateCounter == 0)
        { 
            healthStateCounter++; 
        }
    }
    public int HealthState { get; set; } = 0; 
    public int InfectionCounter { get; set; } = 0;
}
