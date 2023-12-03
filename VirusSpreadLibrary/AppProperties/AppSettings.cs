using System.ComponentModel;
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

    private string appVersion = Application.ProductVersion.ToString();    
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
    private bool showHelperSettings = false;

    //private XmlFont xmlWindowFont = new();
    private Color colorSelection = new();
    private string xmlColorSelection = "";
    private String configFilePath = string.Concat(Path.GetDirectoryName(Application.ExecutablePath),
                                    "\\", Path.GetFileNameWithoutExtension(Application.ExecutablePath), ".XML");

    private Color toolbarColor = SystemColors.Control;
    private string xmlToolbarColor = "";

    private readonly List<Color> colorList = [];

    private DoubleSeriesClass personMoveRate = new();
    private string personMoveRateFrom = "";
    private string personMoveRateTo = "";

    private DoubleSeriesClass virusMoveRate = new();
    private string virusMoveRateFrom = "";
    private string virusMoveRateTo = "";

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
    public Setting Setting
    {
        get => setting;
        set => setting = value;
    }


    #region properties settings


    //-> category in grid
    [CategoryAttribute("Internal Settings")] 
    // -> hide in grid if showHelperSettings is flase
    [Browsable(false)]
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
    [Browsable(false)]
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
    public string AppVersion
    {
        get => appVersion;
        set => appVersion = Application.ProductVersion.ToString();
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

    [CategoryAttribute("App Settings")]
    [Description("Number of maximal iterations for the Simulation - long")]
    public long MaxIterations
    {
        get => maxIterations;
        set => maxIterations = value;
    }

    [CategoryAttribute("Move Rate"), ReadOnlyAttribute(false)]
    [ExcludeFromSerialization]
    [Browsable(false)]
    public DoubleSeriesClass PersonMoveRate
    {
        get => personMoveRate;
        set => personMoveRate = value;
    }

    [CategoryAttribute("Move Rate Person"), ReadOnlyAttribute(false)]
    [DescriptionAttribute("The description")]
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
    [DescriptionAttribute("The description")]
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

    [Browsable(true)]
    [CategoryAttribute("Color Settings")]
    [DefaultValue(typeof(Color), "Black")]
    // -> exclude from serialize
    // because XmlSerializer can't handle Color objects
    [ExcludeFromSerialization]
    public Color ColorSelection
    {
        get
        {
            return (Color)colorSelection;
        }
        set
        {
            colorSelection = value;
        }
    }

    [CategoryAttribute("Color Settings")]
    [Browsable(false)]
    [XmlElement("ColorSelection")]
    public string XmlColorSelection
    {
        get
        {
            xmlColorSelection = setting.ToXmlColor(ColorSelection);
            return xmlColorSelection;
        }
        set
        {
            xmlColorSelection = value;
            ColorSelection = setting.FromXmlColor(value);
        }
    }

    [CategoryAttribute("Color Toolbar Settings")]
    public Color ToolbarColor
    {
        get => toolbarColor;
        set => toolbarColor = value;
    }

    [CategoryAttribute("Color Toolbar Settings")]
    [Browsable(false)]
    [XmlElement("ColorSelection")]
    public string XmlToolbarColor
    {
        get
        {
            xmlToolbarColor = setting.ToXmlColor(ToolbarColor);
            return xmlToolbarColor;
        }
        set
        {
            xmlToolbarColor = value;
            ToolbarColor = setting.FromXmlColor(value);
        }
    }

    [CategoryAttribute("Color List Settings")]
    [Description("Not implemented yet - choose Grid Cell Colors")]
    public List<Color> ColorList
    {
        get
        {
            return colorList;
        }
        set
        {
            colorList.Add(ToolbarColor);
        }
    }

    //[CategoryAttribute("Font Settings")]
    //[ExcludeFromSerialization]
    //public Font WindowFont
    //{
    //    get => windowFont;
    //    set => windowFont = value;
    //}

    ////[XmlElement("WindowFont")]
    //[Browsable(false)]
    //[CategoryAttribute("Font Settings")]
    //public XmlFont XmlWindowFont
    //{
    //    get
    //    {                
    //        xmlWindowFont = xmlWindowFont.ToXmlFont(WindowFont);
    //        return xmlWindowFont;
    //    }
    //    set
    //    {
    //        xmlWindowFont = value;
    //        WindowFont = xmlWindowFont.FromXmlFont(xmlWindowFont);
    //    }
    //}


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
