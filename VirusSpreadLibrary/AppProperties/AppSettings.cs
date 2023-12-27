﻿using System.ComponentModel;
using System.Drawing.Design;
using System.Xml.Serialization;
using Polenter.Serialization;
using VirusSpreadLibrary.AppProperties.PropertyGridExt;


namespace VirusSpreadLibrary.AppProperties;

// This contaier class holds all the app configuration settings
// similar like the built in visual Studio properties.settings 
// 
// it serialize-deserialize all properties of this contaier class 
// into bin\..\AppProperties.XML using the Nuget Package: SharpSerializer.Core
//
// property of the objects are then shown at runtime in the property grid UI 
// together with a property description 
// by binding - as selected object - to the propertygrid in the MainForm
//
// Workaround as Visual Studio properties.settings Designer could not save a description for properties
// it deletes all manually inserted descriptions, when a new property is saved
// It also can't handle complex property classes with .Net 6/7 - only with .Net FW
//

// depends on Nuget: SharpSerializer.Core 1.0.0 !!
//
// !! Using "SharpSerializer for the .Net Desktop Framework" !!
// !! did not work stable with .Net 6/7 !!
//
// Class must also have a constructor for xml de/serialize 
// therfore unsightly workaround for color and font was required




[Serializable]
[DefaultPropertyAttribute("AppVersion")]

public  class AppSettings
{


    private string about = "";
    //private readonly string appVersion = Application.ProductVersion[..Application.ProductVersion.ToString().IndexOf('+')];
    private readonly string appVersion = "1.0.1";
    private int gridMaxX = 100;
    private int gridMaxY = 100;
    private long initialPersonPopulation = 20;
    private long initialVirusPopulation = 10;
    private long maxIterations = 1000;
    private Point form_Config_WindowLocation = new(0, 0);
    private Size form_Config_WindowSize = new(2056, 1010);
    private bool virusMoveGlobal = true;
    private bool personMoveGlobal = true;
    private int gridFormTimer = 1;
    private bool trackMovment = false;
    private bool populationDensityColoring = false;
    private bool showHelperSettings = false;


    private Color virusColor = Color.WhiteSmoke;
    private string xmlVirusColor = "WhiteSmoke";
    private Color personsHealthyOrRecoverdColor = Color.Blue;
    private string xmlPersonsHealthyOrRecoverdColor = "Blue";
    private Color personsInfectedColor = Color.DeepSkyBlue;
    private string xmlPersonsInfectedColor = "DeepSkyBlue";
    private Color emptyCellColor = Color.White;
    private string xmlEmptyCellColor = "White";
    private Color personsInfectiousColor = Color.Violet;
    private string xmlPersonsInfectiousColor = "Violet";
    private Color personsRecoverdImmuneNotInfectiousColor = Color.Plum;
    private string xmlPersonsRecoverdImmuneNotInfectiousColor = "Plum";


    private String configFilePath = string.Concat(Path.GetDirectoryName(Application.ExecutablePath),
                                    "\\", Path.GetFileNameWithoutExtension(Application.ExecutablePath), ".XML");

    private String csvFilePath = string.Concat(Path.GetDirectoryName(Application.ExecutablePath),
                                    "\\", Path.GetFileNameWithoutExtension(Application.ExecutablePath), ".CSV");

    private DoubleSeriesClass personMoveRate = new();
    private string personMoveRateFrom = "";
    private string personMoveRateTo = "";
    private int personMoveActivityRnd = 1;
    private int personMoveHomeActivityRnd = 2;
    private int personLatencyPeriod = 2;
    private int personInfectiousPeriod = 9;
    private int personReinfectionImmunityPeriod = 155;
    private int personReinfectionRate = 11;

    private DoubleSeriesClass virusMoveRate = new();
    private string virusMoveRateFrom = "";
    private string virusMoveRateTo = "";
    private double virusMoveActivityRnd = 1;
    private double virusMoveHomeActivityRnd = 0;


    private string? dummy;

    // setting helper for de/serializing the AppSettings
    // and to save and load from xml file
    private Setting setting = new();

    // global static config object
    // did not work in constructor or as property!
    #pragma warning disable CA2211
    public static AppSettings Config = new();
    #pragma warning restore CA2211
    public AppSettings()
    {
        
        PersonMoveRate.DoubleSeriesFrom = new DoubleSeries([1,1,1,1,1,1,1,1,1,1]);
        PersonMoveRate.DoubleSeriesTo = new DoubleSeries([2,2,2,2,2,2,2,2,2,2]);
        VirusMoveRate.DoubleSeriesFrom = new DoubleSeries([1,1,1,1,1,1,1,1,1,1]);
        VirusMoveRate.DoubleSeriesTo = new DoubleSeries([2,2,2,2,2,2,2,2,2,2]);
        // in main Form you can fill it like this
        // AppSettings.Config.VirusMoveRate.DoubleSeriesFrom = new DoubleSeries([1,1,1,1,1,1,1,1,1,1]);
        // AppSettings.Config.VirusMoveRate.DoubleSeriesTo = new DoubleSeries([2,2,2,2,2,2,2,2,2,2]);
    }

    [ExcludeFromSerialization]
    [Browsable(false)]
    public Setting Setting
    {
        get => setting;
        set => setting = value;
    }


    #region properties settings


    //-> category in grid
    [CategoryAttribute("Internal Settings")]     
    // -> grid value is editable
    [ReadOnlyAttribute(false)]
    // -> show description in grid  
    [Description("Hide internal used properties in PropertyGrid")]
    public bool ShowHelperSettings
    {
        get => showHelperSettings;
        set => showHelperSettings = value;
    }

    [CategoryAttribute("Internal Settings")]
    [Description("Internal use - to save actual MainForm size and position")] 
    [Browsable(false)] //-> hide from Grid if Property showHelperSettings is false
    public Point Form_Config_WindowLocation
    {
        get => form_Config_WindowLocation;
        set => form_Config_WindowLocation = value;
    }
    
    [CategoryAttribute("Internal Settings")]
    [Description("Internal use - to save actual MainForm size and position")]
    [Browsable(false)]
    public Size Form_Config_WindowSize
    {
        get => form_Config_WindowSize;
        set => form_Config_WindowSize = value;
    }

    [CategoryAttribute("App Info"), ReadOnlyAttribute(true)]
    [Description("Dedicated to a lighthouse in stormy seas \r\nMIT License")]
    [ExcludeFromSerialization]  // -> XmlSerializer will not serialize the object
    public string About
    {
        get => about;
        set => about = value;
    }

    [CategoryAttribute("App Info"), ReadOnlyAttribute(true)]
    public string AppVersion
    {
        get => appVersion;
        set => dummy = value;  // don't overrwrite with value from xml
    }

    [CategoryAttribute("Grid Settings")]
    [Description("Width of the Grid Filed - pixel")]
    public int GridMaxX
    {
        get => gridMaxX;
        set => gridMaxX = value;
    }

    [CategoryAttribute("Grid Settings")]
    [Description("Higth of the Grid Filed - pixel")]
    public int GridMaxY
    {
        get => gridMaxY;
        set => gridMaxY = value;
    }

    [CategoryAttribute("Grid Settings")]
    [Description("Timer in milli seconds : standard 1 ms - bigger values slows down the iterations and the redraw of grid field form")]
    // return ((int)(this["GridFormTimer"])) this["GridFormTimer"] = value;
    public int GridFormTimer
    {
        get => gridFormTimer;
        set => gridFormTimer = value;
    }


    [CategoryAttribute("Grid Settings")]
    [Description("default false: If true allows movement tracking over the whole time - all iterations, as the person or virus at the old grid coordinate are not deleted after movment")]
    public bool TrackMovment
    {
        get => trackMovment;
        set => trackMovment = value;
    }

    [CategoryAttribute("Grid Settings")]
    [Description("default false: slows down iterations. True -> shading cell color depending on population count. Higher population numbers are shown in darker color")]
    public bool PopulationDensityColoring
    {
        get => populationDensityColoring;
        set => populationDensityColoring = value;
    }

    [CategoryAttribute("Person Settings")]
    [Description("Start poulation for Persons - long")]
    public long InitialPersonPopulation
    {
        get => initialPersonPopulation;
        set => initialPersonPopulation = value;
    }

    [CategoryAttribute("Person Settings")]
    [Description("allow person global moovment - true : movement within the distance limit only from the home coordinate. false: movement within the distance limit from the new current coordinate, therefore over the entire grid field possible")]
    public bool PersonMoveGlobal
    {
        get => personMoveGlobal;
        set => personMoveGlobal = value;
    }

    [CategoryAttribute("Person Settings")]
    [Description("integer 0 to X % \r\n0 = don't move, 1 = move every iteration, 2 = move random average every 2nd iteration, X = move back home random average every Xd iteration")]
    public int PersonMoveActivityRnd
    {
        get => personMoveActivityRnd;
        set 
        {
            if (value >= 0 )personMoveActivityRnd = value;
            else personMoveActivityRnd = 1;
        } 
    }

    [CategoryAttribute("Person Settings")]
    [Description("integer 0 to X % \r\n0 = don't move back home, 1 = move back home every iteration, 2 = move back home random average every 2nd iteration, X = move back home random average every Xd iteration")]
    public int PersonMoveHomeActivityRnd
    {
        get => personMoveHomeActivityRnd;
        set
        {
            //(value >= 0 && value <= 1) -> use for doubles between 0-1
            if (value >= 0 ) personMoveHomeActivityRnd = value; 
            else personMoveHomeActivityRnd = 1;
        }
    }

    [CategoryAttribute("Person Settings")]
    [Description("Period from infection until a person is contagious.\r\nDefault 2 days (iterations)")]
    public int PersonLatencyPeriod
    {
        get => personLatencyPeriod;
        set => personLatencyPeriod = value;
    }

    [CategoryAttribute("Person Settings")]
    [Description("Period during which a person is infectious.\r\nDefault 9 days (iterations)")]
    public int PersonInfectiousPeriod
    {
        get => personInfectiousPeriod;
        set => personInfectiousPeriod = value;
    }

    [CategoryAttribute("Person Settings")]
    [Description("Period during which a person is immune after after the recovery. \r\nDefault  5 Month = 155 day (Iterations)")]
    public int PersonReinfectionImmunityPeriod
    {
        get => personReinfectionImmunityPeriod;
        set => personReinfectionImmunityPeriod = value;
    }

    [CategoryAttribute("Person Settings")]
    [Description("Percentage of reinfections after the immunity phase following an illness \r\nDefault = 11 %")]
    public int PersonReinfectionRate
    {
        get => personReinfectionRate;
        set => personReinfectionRate = value;
    }
    
    [CategoryAttribute("Virus Settings")]
    [Description("Start poulation for Viruses - long")]
    public long InitialVirusPopulation
    {
        get => initialVirusPopulation;
        set => initialVirusPopulation = value;
    }

    [CategoryAttribute("Virus Settings")]
    [Description("allow virus global movement - true : movement within the distance limit only from the home coordinate. false: movement within the distance limit from the new current coordinate, therefore over the entire grid field possible")]
    public bool VirusMoveGlobal
    {
        get => virusMoveGlobal;
        set => virusMoveGlobal = value;
    }

    [CategoryAttribute("Virus Settings")]
    [Description("integer 0 to X % \r\n0 = don't move, 1 = move every iteration, 2 = move random average every 2nd iteration, X = move back home random average every Xd iteration")]
    public double VirusMoveActivityRnd
    {
        get => virusMoveActivityRnd;
        set
        {
            if (value >= 0 ) virusMoveActivityRnd = value;
            else virusMoveActivityRnd = 1;
        }
    }

    [CategoryAttribute("Virus Settings")]
    [Description("integer 0 to X % \r\n0 = don't move back home, 1 = move back home every iteration, 2 = move back home random average every 2nd iteration, X = move back home random average every Xd iteration")]
    public double VirusMoveHomeActivityRnd
    {
        get => virusMoveHomeActivityRnd;
        set
        {
            if (value >= 0 ) virusMoveHomeActivityRnd = value;
            else virusMoveHomeActivityRnd = 1;
        }
    }

    [CategoryAttribute("App Settings")]
    [Description("Number of maximal iterations for the Simulation - long")]
    public long MaxIterations
    {
        get => maxIterations;
        set => maxIterations = value;
    }

    [CategoryAttribute("Move Rate"), ReadOnlyAttribute(false)]
    [DescriptionAttribute()]  
    [ExcludeFromSerialization]
    [Browsable(false)]
    public DoubleSeriesClass PersonMoveRate
    {
        get => personMoveRate;
        set => personMoveRate = value;
    }

    [CategoryAttribute("Move Rate Person"), ReadOnlyAttribute(false)]
    [DescriptionAttribute("motion profile - see below:\r\n\r\nused to simulate frequent short and rare long distance moves of people\r\nrandom chooses one range from the distance range serie\r\n"+
                           "then get a random distance within the selected distance range\r\nget a random direction 365° \r\nreturn the NewGridCoordinate to move to" +
                          "\r\n\r\nPersonMoveRateFrom \r\nholds a series of the upper maximum ranges a person can move \r\nif the person moves a random range from the range serie is choosen\r\n"+
                           "this can be used to simulate tavel behavior for example rear far and frequent low distance movment of persons\r\n")]
    public string PersonMoveRateFrom
    {
        get
        {
            personMoveRateFrom = personMoveRate.DoubleSeriesFrom.ToString()!;
            if (personMoveRateFrom is null) 
            { 
                return ""; 
            }
            else return personMoveRateFrom;
        }
        set
        {
            personMoveRateFrom = value;
            personMoveRate.DoubleSeriesFrom = DoubleSeries.Parse(value);
        }
    }
    
    [CategoryAttribute("Move Rate Person"), ReadOnlyAttribute(false)]
    [DescriptionAttribute("series for the lower minimum range a person can move")]
    public string? PersonMoveRateTo
    {
        get
        {
            personMoveRateTo = personMoveRate.DoubleSeriesTo.ToString()!;
            return personMoveRateTo;
        }
        set 
        {
            personMoveRateTo = value!;
            personMoveRate.DoubleSeriesTo = DoubleSeries.Parse(value!); 
        }
        
    }

    [CategoryAttribute("Move Rate"), ReadOnlyAttribute(false)]
    [ExcludeFromSerialization]
    [Browsable(false)]
    public DoubleSeriesClass VirusMoveRate
    {
        get => virusMoveRate;
        set => virusMoveRate = value;
    }

    [CategoryAttribute("Move Rate Virus"), ReadOnlyAttribute(false)]
    [DescriptionAttribute("motion profile - see below:\r\n\r\nused to simulate moving behavior\r\nrandom chooses one range from the distance range serie\r\n"+
                         "then get a random distance within the selected choosed range\r\nget a random direction 365° \r\nreturn the NewGridCoordinate for to move to" +
                         "\r\n\r\nVirusMoveRateFrom \r\nholds a series of the lower limit ranges a virus can move \r\nrandom a range from the range serie will be chosen\r\n" +
                          "this can be used to simulate spread behavior of a virus for example in a airborn scenario\r\n")]
    public string VirusMoveRateFrom
    {
        get
        {
            virusMoveRateFrom = virusMoveRate.DoubleSeriesFrom.ToString()!;
            if (virusMoveRateFrom is null)
            {
                return "";
            }
            else return virusMoveRateFrom;
        }
        set
        {
            virusMoveRateFrom = value;
            virusMoveRate.DoubleSeriesFrom = DoubleSeries.Parse(value);
        }
    }

    
    [CategoryAttribute("Move Rate Virus"), ReadOnlyAttribute(false)]
    [DescriptionAttribute("series of the uper limit maximum ranges a person can move")]
    public string? VirusMoveRateTo
    {
        get
        {
            virusMoveRateTo = virusMoveRate.DoubleSeriesTo.ToString()!;
            return virusMoveRateTo;
        }
        set
        {
            virusMoveRateTo = value!;
            virusMoveRate.DoubleSeriesTo = DoubleSeries.Parse(value!);
        }

    }

    [CategoryAttribute("Global Settings")]
    [Editor(typeof(UIFilenameEditor), typeof(UITypeEditor))]
    [UIFilenameEditor.SaveFileAttribute] // -> default is openFile
    [DescriptionAttribute("path to the application xml configuration file, to read and store configuration settings")]
    public string ConfigFilePath
    {
        get 
        {            
            return Setting.GetLastConfigFilePath(configFilePath);
        }
        set 
        {
            Setting.SetLastConfigFilePath(value);
            configFilePath = Setting.GetLastConfigFilePath(configFilePath);
        } 
    }

    [CategoryAttribute("Global Settings")]
    [Editor(typeof(UIFilenameEditor), typeof(UITypeEditor))]
    [UIFilenameEditor.SaveFileAttribute] // -> default is openFile
    [DescriptionAttribute("path to csv file to write the plot data, data are then read by the plot app to plot the data diagram")]
    public string CsvFilePath
    {
        get
        {
            return csvFilePath;
        }
        set
        {
            csvFilePath = value;
        }
    }

    [Browsable(true)]
    [CategoryAttribute("Color Settings")]
    [DescriptionAttribute("Default: WhiteSmoke")]
    [ExcludeFromSerialization]
    public Color VirusColor
    {
        get
        {
            return (Color)virusColor;
        }
        set
        {
            virusColor = value;
        }
    }

    [CategoryAttribute("Color Settings")]
    [Browsable(false)]
    [XmlElement("VirusColor")]
    public string XmlVirusColor
    {
        get
        {
            xmlVirusColor = setting.ToXmlColor(virusColor);
            return xmlVirusColor;
        }
        set
        {
            xmlVirusColor = value;
            virusColor = setting.FromXmlColor(value);
        }
    }

    [Browsable(true)]
    [CategoryAttribute("Color Settings")]
    [DescriptionAttribute("Default: Blue")]
    [ExcludeFromSerialization]
    public Color PersonsHealthyOrRecoverdColor
    {
        get
        {
            return (Color)personsHealthyOrRecoverdColor;
        }
        set
        {
            personsHealthyOrRecoverdColor = value;
        }
    }

    [CategoryAttribute("Color Settings")]
    [Browsable(false)]
    [XmlElement("PersonsHealthyOrRecoverdColor")]
    public string XmlPersonsHealthyOrRecoverdColor
    {
        get
        {
            xmlPersonsHealthyOrRecoverdColor = setting.ToXmlColor(personsHealthyOrRecoverdColor);
            return xmlPersonsHealthyOrRecoverdColor;
        }
        set
        {
            xmlPersonsHealthyOrRecoverdColor = value;
            personsHealthyOrRecoverdColor = setting.FromXmlColor(value);
        }
    }

    [Browsable(true)]
    [CategoryAttribute("Color Settings")]
    [DescriptionAttribute("Default: DeepSkyBlue")]
    [ExcludeFromSerialization]
    public Color PersonInfectedColor
    {
        get
        {
            return (Color)personsInfectedColor;
        }
        set
        {
            personsInfectedColor = value;
        }
    }

    [CategoryAttribute("Color Settings")]
    [Browsable(false)]
    [XmlElement("PersonsInfectedColor")]
    public string XmlPersonsInfectedColor
    {
        get
        {
            xmlPersonsInfectedColor = setting.ToXmlColor(personsInfectedColor);
            return xmlPersonsInfectedColor;
        }
        set
        {
            xmlPersonsInfectedColor = value;
            personsInfectedColor = setting.FromXmlColor(value);
        }
    }       

    [Browsable(true)]
    [CategoryAttribute("Color Settings")]
    [DescriptionAttribute("Default: ")]
    [ExcludeFromSerialization]
    public Color PersonInfectiousColor
    {
        get
        {
            return (Color)personsInfectiousColor;
        }
        set
        {
            personsInfectiousColor = value;
        }
    }

    [CategoryAttribute("Color Settings")]
    [Browsable(false)]
    [XmlElement("PersonsInfectiousColor")]
    public string XmlPersonsInfectiousColor
    {
        get
        {
            xmlPersonsInfectiousColor = setting.ToXmlColor(personsInfectiousColor);
            return xmlPersonsInfectiousColor;
        }
        set
        {
            xmlPersonsInfectiousColor = value;
            personsInfectiousColor = setting.FromXmlColor(value);
        }
    }

    [Browsable(true)]
    [CategoryAttribute("Color Settings")]
    [DescriptionAttribute("Default: ")]
    [ExcludeFromSerialization]
    public Color PersonsRecoverdImmuneNotInfectiousColor
    {
        get
        {
            return (Color)personsRecoverdImmuneNotInfectiousColor;
        }
        set
        {
            personsRecoverdImmuneNotInfectiousColor = value;
        }
    }

    [CategoryAttribute("Color Settings")]
    [Browsable(false)]
    [XmlElement("PersonsRecoverdImmuneNotInfectiousColor")]
    public string XmlPersonsRecoverdImmuneNotInfectiousColor
    {
        get
        {
            xmlPersonsRecoverdImmuneNotInfectiousColor = setting.ToXmlColor(personsRecoverdImmuneNotInfectiousColor);
            return xmlPersonsRecoverdImmuneNotInfectiousColor;
        }
        set
        {
            xmlPersonsRecoverdImmuneNotInfectiousColor = value;
            personsRecoverdImmuneNotInfectiousColor = setting.FromXmlColor(value);
        }
    }


    [Browsable(true)]
    [CategoryAttribute("Color Settings")]
    [DescriptionAttribute("Default: Black: chang does not work yet, because skglControl has transparent (black) background")]
    [ExcludeFromSerialization]
    public Color EmptyCellColor
    {
        get
        {
            return (Color)emptyCellColor;
        }
        set
        {
            emptyCellColor = value;
        }
    }

    [CategoryAttribute("Color Settings")]
    [Browsable(false)]
    [XmlElement("EmptyCellColor")]
    public string XmlEmptyCellColor
    {
        get
        {
            xmlEmptyCellColor = setting.ToXmlColor(emptyCellColor);
            return xmlEmptyCellColor;
        }
        set
        {
            xmlEmptyCellColor = value;
            emptyCellColor = setting.FromXmlColor(value);
        }
    }

} // APP Settings

#endregion  properties settings


// for debug tests -> serialize and deserializes only this class 
//
// run it from a Form like this:
//
// GenericDictionary Case = new GenericDictionary();
// Setting.SerializeTheClass(Case);          // <- serialize to xml and deserialize from xml file
// PropertyGrid1.SelectedObject = Case;      // <- assing the obj to the grid
public class GenDictionary : ClassSerializer
{
    //private GenDictionary genDictionary;
    private Dictionary<int, string> genDictionary = [];
    public GenDictionary()
    {
        genDictionary.Add(5, "five");
        genDictionary.Add(10, "ten");
        genDictionary.Add(20, "twenty");            
        this.Source = genDictionary;
    }

    public Dictionary<int, string> GetSetGenDictionary
    {
        get => genDictionary;
        set => genDictionary = value;
    }

    //check if result and source are identical
    public override void ResultReview(object result)
    {

        var s = (Dictionary<int, string>?)Source;
        var r = (Dictionary<int, string>)result;
        if (s != null)
        {
            AreEqual(s[5], r[5]);
            AreEqual(s[10], r[10]);
            AreEqual(s[20], r[20]);
        }
    }
}