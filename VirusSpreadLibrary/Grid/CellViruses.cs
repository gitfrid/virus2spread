using VirusSpreadLibrary.Creature;

namespace VirusSpreadLibrary.Grid
{
    public class CellViruses
    {
        private int numViruses;
        public int NumViruses 
        { 
            get => numViruses; 
        
        }
        public List<Virus> Viruses { get; set; }
        public CellViruses()
        {
            Viruses = new List<Virus>();
        }
        public void Add(Virus AddVirus)
        {
            Viruses.Add(AddVirus);
            ++numViruses;
        }
        public void Remove(Virus RemoveVirus)
        {
            Viruses.Remove(RemoveVirus);
            if (NumViruses > 0)
            {
               --numViruses;
            }
        }
    }
}
