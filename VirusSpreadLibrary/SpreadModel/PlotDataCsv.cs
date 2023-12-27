using VirusSpreadLibrary.Creature;
using RecordParser.Builders.Writer;
using VirusSpreadLibrary.AppProperties;
using RecordParser.Parsers;
using System.Globalization;
using System.Formats.Asn1;

namespace VirusSpreadLibrary.SpreadModel
{

    public static class CsvWriter 
    {
        // var csvWriter = null;
        //public CsvWriter()
        //{
        //    .csvWriter = new VariableLengthWriterSequentialBuilder<(int iterationNumber, int personPopulation, int virusPopulation, int personsAge, int virusesAge,
        //        int personsHealthy, int personsRecoverd, int personsInfected, int personsReinfected, int personsInfectionCounter, int personsInfectious, int personsRecoverdImmuneNotinfectious,
        //        long PersonsMoveDistance, long VirusesMoveDistance)>()
        //        .Map(x => x.iterationNumber)
        //        .Map(x => x.personPopulation)
        //        .Map(x => x.virusPopulation)
        //        .Map(x => x.personsAge)
        //        .Map(x => x.virusesAge)
        //        .Map(x => x.personsHealthy)
        //        .Map(x => x.personsRecoverd)
        //        .Map(x => x.personsInfected)
        //        .Map(x => x.personsReinfected)
        //        .Map(x => x.personsInfectionCounter)
        //        .Map(x => x.personsInfectious)
        //        .Map(x => x.personsRecoverdImmuneNotinfectious)
        //        .Map(x => x.PersonsMoveDistance)
        //        .Map(x => x.VirusesMoveDistance)
        //        .Build(" ; ");
        //}

    }

    public class PlotDataCsv
    {
        // -> does this apply to reality?
        // not accounting virus contact duration, virus contact ammount, reinfectins in immunity period, complex movement

        private int iterationNumber = 0;
        private int personPopulation = 0;
        private int virusPopulation = 0;
        private int personsAge = 0;
        private int virusesAge = 0;

        private int personsHealthy = 0;
        private int personsRecoverd = 0;
        private int personsInfected = 0;
        private int personsReinfected = 0;
        private int personsInfectionCounter = 0;
        private int personsInfectious = 0;
        private int personsRecoverdImmuneNotinfectious = 0;
        public int IterationNumber { get; set; }
        public int PersonPopulation { get; set; }
        public int VirusPopulation { get; set; }
        public long PersonsAge { get; set; }
        public long VirusesAge { get; set; }
        public long PersonsMoveDistance { get; set; }
        public long VirusesMoveDistance { get; set; }
     
        private string outputFilePath = AppSettings.Config.CsvFilePath;


        public void SetPersonHealthState(PersonState PersonState)
        {
            personsHealthy += PersonState.PersonHealthy;
            personsRecoverd += PersonState.PersonRecoverd;
            personsInfected += PersonState.PersonInfected;
            personsReinfected += PersonState.PersonReinfected;
            personsInfectionCounter += PersonState.InfectionCounter;
            personsInfectious += PersonState.PersonInfectious;
            personsRecoverdImmuneNotinfectious += PersonState.PersonRecoverdImmuneNotinfectious;
        }

        public void WriteToCsv()
        {
            ////CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;



            //var instance = (iterationNumber, personPopulation, virusPopulation, personsAge, virusesAge,
            //    personsHealthy, personsRecoverd, personsInfected, personsReinfected, personsInfectionCounter, personsInfectious, personsRecoverdImmuneNotinfectious,
            //    PersonsMoveDistance, VirusesMoveDistance);

            //Span<char> destination = new char[100];
            //var success = writer.TryFormat(instance, destination, out var charsWritten);
            
            
        }

    }
}
