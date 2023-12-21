using VirusSpreadLibrary.Creature;

namespace VirusSpreadLibrary.Grid
{
    public class CellViruses
    {
        public int NumViruses { get; set; }
        public List<Virus> Viruses { get; set; }
        public CellViruses()
        {
            Viruses = new List<Virus>();
        }
        public void Add(Virus AddVirus)
        {
            Viruses.Add(AddVirus);
            ++NumViruses;
        }
        public void Remove(Virus RemoveVirus)
        {
            Viruses.Remove(RemoveVirus);
            --NumViruses;
        }
    }
}
