using VirusSpreadLibrary.Creature;

namespace VirusSpreadLibrary.Grid
{
    public class CellPersons
    {
        private int numPersons;
        public List<Person> Persons { get; set; }
        public int NumPersons 
        {
            get => numPersons;
        }
        public CellPersons()
        {
            Persons = new List<Person>();
        }
        public void Add(Person AddPerson)
        {
            Persons.Add(AddPerson);
            numPersons++;
        }
        public void Remove(Person RemovePerson)
        {
            Persons.Remove(RemovePerson);
            //if (NumPersons  == 0) { MessageBox.Show("ist null!"); }
            if (NumPersons > 0)
            {
               numPersons--;
            }                
        }
    }
}
