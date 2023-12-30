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
        //0"IterationNumber",
        //1"PersonPopulation",
        //2"VirusPopulation",
        //3"PersonsAge",
        //4"VirusesAge",
        //5"personsHealthy",
        //6"personsRecoverd",
        //7"personsInfected",
        //8"personsReinfected",
        //9"personsInfectionCounter",
        //10"personsInfectious",
        //11"personsRecoverdImmuneNotinfectious",
        //12"PersonsMoveDistance",
        //13"VirusesMoveDistance"


        // Initialization of Legend array
        public readonly string[] Legend = ["IterationNumber",
            "PersonPopulation",
            "VirusPopulation",
            "PersonsAge",
            "VirusesAge",
            "personsHealthy",
            "personsRecoverd",
            "personsInfected",
            "personsReinfected",
            "personsInfectionCounter",
            "personsInfectious",
            "personsRecoverdImmuneNotinfectious",
            "PersonsMoveDistance",
            "VirusesMoveDistance"];

        private readonly PlotQueue plotDataQueue;
        private readonly PlotQueuePhaseChart plotPhaseChartDataQueue;

        private bool stopPhaseChartQueue = true;


        // store Y-values to plot ten lines
        readonly double[] yPlotLinesValues = new double[14];

        public double IterationNumber
        {
            get => yPlotLinesValues[0];
            set => yPlotLinesValues[0] = value;
        }

        public double PersonPopulation
        {
            get => yPlotLinesValues[1];
            set => yPlotLinesValues[1] = value;
        }
        public double VirusPopulation
        {
            get => yPlotLinesValues[2];
            set => yPlotLinesValues[2] = value;
        }
        public double PersonsAge
        {
            get => yPlotLinesValues[3];
            set => yPlotLinesValues[3] = value;
        }
        public double VirusesAge
        {
            get => yPlotLinesValues[4];
            set => yPlotLinesValues[4] = value;
        }
        public double PersonsHealthy
        {
            get => yPlotLinesValues[5];
            set => yPlotLinesValues[5] = value;
        }
        public double PersonsRecoverd
        {
            get => yPlotLinesValues[6];
            set => yPlotLinesValues[6] = value;
        }
        public double PersonsInfected
        {
            get => yPlotLinesValues[7];
            set => yPlotLinesValues[7] = value;
        }

        public double PersonsReinfected
        {
            get => yPlotLinesValues[8];
            set => yPlotLinesValues[8] = value;
        }
        public double PersonsInfectionCounter
        {
            get => yPlotLinesValues[9];
            set => yPlotLinesValues[9] = value;
        }

        public double PersonsInfectious
        {
            get => yPlotLinesValues[10];
            set => yPlotLinesValues[10] = value;
        }

        public double PersonsRecoverdImmuneNotinfectious
        {
            get => yPlotLinesValues[11];
            set => yPlotLinesValues[11] = value;
        }

        public double PersonsMoveDistance
        {
            get => yPlotLinesValues[12];
            set => yPlotLinesValues[12] = value;
        }
        public double VirusesMoveDistance
        {
            get => yPlotLinesValues[13];
            set => yPlotLinesValues[13] = value;
        }

        public bool StopPhaseChartQueue
        {
            get => stopPhaseChartQueue;
            set
            {
                stopPhaseChartQueue = value;
                if (stopPhaseChartQueue == true) 
                {
                    PlotPhaseChartDataQueue.ClearQueue();
                }
            }
        }

        //private ConcurrentQueue<List<double>> queue1 = new ConcurrentQueue<List<double>>();

        public PlotData()
        {
            plotDataQueue = new PlotQueue();
            plotPhaseChartDataQueue = new PlotQueuePhaseChart();

            // init array with 0
            Array.Fill(yPlotLinesValues, 0);
        }
        //public PlotQueue PlotDataQueue { get; private set; }

        public void ResetCounter()
        {
            Array.Fill(yPlotLinesValues, 0);
        }

        // public prop to access the queue
        public PlotQueue PlotDataQueue
        {
            get => plotDataQueue;
        }
        public PlotQueuePhaseChart PlotPhaseChartDataQueue
        {
            get => plotPhaseChartDataQueue;
        }
        public void SetPersonHealthState(PersonState PersonState)
        {
            switch (PersonState.HealthState)
            {
                case PersonState.PersonHealthy:
                    PersonsHealthy += 1;
                    break;
                case PersonState.PersonRecoverd:
                    PersonsRecoverd += 1;
                    break;
                case PersonState.PersonInfected:
                    PersonsInfected += 1;
                    break;
                case PersonState.PersonReinfected:
                    PersonsReinfected += 1;
                    break;
                case PersonState.PersonInfectious:
                    PersonsInfectious += 1;
                    break;
                case PersonState.PersonRecoverdImmuneNotinfectious:
                    PersonsRecoverdImmuneNotinfectious += 1;
                    break;
            }
            PersonsInfectionCounter += PersonState.InfectionCounter;
        }

        public void WriteToQueue()
        {
            List<double> values = new();
            List<double> values2 = new();

            // generate list with 14 rand Y-values to plot line 1-14
            for (int i = 0; i < 14; i++)
            {
                //yPlotLinesValues[i] = instance[i];
                values.Add(yPlotLinesValues[i]);

                // phaseChart xy values depending on selected index in x y phaseChart listbox
                if (stopPhaseChartQueue == false)
                {
                    if (i == AppSettings.Config.PhaseChartXSelectedIndex)
                    {
                        values2.Add(yPlotLinesValues[i]);
                    }
                    if (i == AppSettings.Config.PhaseChartYSelectedIndex)
                    {
                        values2.Add(yPlotLinesValues[i]);
                    }
                }
            }

            // enqueue list of 14 rand Y-values into PlotQueue
            PlotDataQueue.EnqueueList(values);

            // enqueue list of X and Y-values into PhasChartPlotQueue
            if (stopPhaseChartQueue == false)
            {
                PlotPhaseChartDataQueue.EnqueueList(values2);
            }

        }
        
    }
}
