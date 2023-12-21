using VirusSpreadLibrary.Creature;

namespace VirusSpreadLibrary.Grid
{
    public class CellPersons
    {
        public List<Person> Persons { get; set; }
        public int NumPersons { get; set; }
        public CellPersons()
        {
            Persons = new List<Person>();
        }
        public void Add(Person AddPerson)
        {
            Persons.Add(AddPerson);
            NumPersons++;
        }
        public void Remove(Person RemovePerson)
        {
            Persons.Remove(RemovePerson);
            NumPersons--;
        }
    }
}
