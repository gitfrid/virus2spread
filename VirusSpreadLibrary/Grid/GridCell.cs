
using VirusSpreadLibrary.Creature;

namespace VirusSpreadLibrary.Grid;
public class GridCell
{
    private Microsoft.Maui.Graphics.Color? cellColor;   
    private CellViruses virusPopulation;
    private CellPersons personPopulation;
    private int cellState = 7;

    public GridCell()
    {
        // set Cell Population and Color!
        virusPopulation = new CellViruses();
        personPopulation = new CellPersons();
        cellColor = new Microsoft.Maui.Graphics.Color();         
    }
    public Microsoft.Maui.Graphics.Color CellColor
    {
        get => cellColor!;
        set => cellColor = value;
    }
    public CellViruses VirusPopulation
    {
        get => virusPopulation;
        set => virusPopulation = value;
    }
    public CellPersons PersonPopulation
    {
        get => personPopulation;
        set => personPopulation = value;
    }
    public int CellState
    {
        get => cellState;
        set => cellState = value;
    }
    public void AddVirus(Virus AddVirus )
    {
        VirusPopulation.Add(AddVirus);
    }
    public void AddPerson(Person AddPerson)
    {
        PersonPopulation.Add(AddPerson);
    }

    public void RemoveVirus(Virus RemoveVirus)
    {
        VirusPopulation.Remove(RemoveVirus);
    }
    public void RemovePerson(Person RemovePerson)
    {
        PersonPopulation.Remove(RemovePerson);
    }

    public int NumViruses()
    {
        if (VirusPopulation == null)
        {
            return 0;
        }
        else
        {
            return VirusPopulation.NumViruses;
        }
    }
    public int NumPersons()
    {
        if (PersonPopulation == null) 
        { 
            return 0;
        }
        else 
        {
            return PersonPopulation.NumPersons;
        }        
    }

}
