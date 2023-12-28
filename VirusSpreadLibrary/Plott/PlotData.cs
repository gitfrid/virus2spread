using VirusSpreadLibrary.Creature;
using RecordParser.Builders.Writer;
using VirusSpreadLibrary.AppProperties;
using RecordParser.Parsers;
using System.Globalization;
using System.Formats.Asn1;
using System.Collections.Concurrent;
using SkiaSharp;

namespace VirusSpreadLibrary.Plott
{

    public class PlotData
    {
        // -> does this apply to reality?
        // not accounting virus contact duration, virus contact ammount, reinfectins in immunity period, complex movement


        // Initialization of Legend array
        public string[] Legend = ["IterationNumber", "PersonPopulation", "VirusPopulation", "PersonsAge", "VirusesAge",
                "personsHealthy", "personsRecoverd", "personsInfected", "personsReinfected", "personsInfectionCounter",
                "personsInfectious", "personsRecoverdImmuneNotinfectious", "PersonsMoveDistance", "VirusesMoveDistance" ];

        //private long iterationNumber = 0;
        //private long personPopulation = 0;
        //private long virusPopulation = 0;
        //private long personsAge = 0;
        //private long virusesAge = 0;

        private long personsHealthy = 0;
        private long personsRecoverd = 0;
        private long personsInfected = 0;
        private long personsReinfected = 0;
        private long personsInfectionCounter = 0;
        private long personsInfectious = 0;
        private long personsRecoverdImmuneNotinfectious = 0;

        public long IterationNumber { get; set; }
        public long PersonPopulation { get; set; }
        public long VirusPopulation { get; set; }
        public long PersonsAge { get; set; }
        public long VirusesAge { get; set; }
        public long PersonsMoveDistance { get; set; }
        public long VirusesMoveDistance { get; set; }
        public long PersonsInfectionCounter
        {
            get => personsInfectionCounter;
            set => personsInfectionCounter = value;
        }

        // store Y-values to plot ten lines
        readonly long[] yPlotLinesValues = new long[14];

     
        //private ConcurrentQueue<List<long>> queue1 = new ConcurrentQueue<List<long>>();

        // public prop to access the queue
        public PlotQueue PlotDataQueue { get; private set; }

        public PlotData()
        {
            PlotDataQueue = new PlotQueue();
            // init array with 0
            Array.Fill(yPlotLinesValues, 0);
        }

        public void ResetCounter()
        {
            IterationNumber = 0;
            PersonPopulation = 0;
            VirusPopulation = 0;
            PersonsAge = 0;
            VirusesAge = 0;

            personsHealthy = 0;
            personsRecoverd = 0;
            personsInfected = 0;
            personsReinfected = 0;
            personsInfectious = 0;
            personsRecoverdImmuneNotinfectious = 0;
            PersonsInfectionCounter = 0;

            PersonsMoveDistance = 0;
            VirusesMoveDistance = 0;
        }

        public void SetPersonHealthState(PersonState PersonState)
        {

            switch (PersonState.HealthState)
            {
                case PersonState.PersonHealthy:
                    personsHealthy += 1;
                    break;
                case PersonState.PersonRecoverd:
                    personsRecoverd += 1;
                    break;
                case PersonState.PersonInfected:
                    personsInfected += 1;
                    break;
                case PersonState.PersonReinfected:
                    personsReinfected += 1;
                    break;
                case PersonState.PersonInfectious:
                    personsInfectious += 1;
                    break;
                case PersonState.PersonRecoverdImmuneNotinfectious:
                    personsRecoverdImmuneNotinfectious += 1;
                    break;
            }
            personsInfectionCounter += PersonState.InfectionCounter;
        }

        public void WriteToQueue()
        {

            List<long> values = new ();

            //long[] instance = new long[14]{IterationNumber, PersonPopulation, VirusPopulation, PersonsAge, VirusesAge,
            //    personsHealthy, personsRecoverd, personsInfected, personsReinfected, personsInfectionCounter, 
            //    personsInfectious, personsRecoverdImmuneNotinfectious, PersonsMoveDistance, VirusesMoveDistance };

            long[] instance = [0, 0, 0, 0, 0,
                personsHealthy, personsRecoverd, personsInfected, personsReinfected, personsInfectionCounter,
                personsInfectious, personsRecoverdImmuneNotinfectious, PersonsMoveDistance, VirusesMoveDistance ];


            // generate list with 14 rand Y-values to plot line 1-14
            for (int i = 0; i < 14; i++)
            {
                //yPlotLinesValues[i] = instance[i];
                values.Add(instance[i]);
            }

            // enqueue list of 14 rand Y-values into PlotQueue
            PlotDataQueue.EnqueueList(values);
        }

    }
}
