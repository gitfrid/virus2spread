using VirusSpreadLibrary.Enum;

namespace VirusSpreadLibrary.Grid;

public class ColorList
{
    private List<ColorTranlation> _colorList = new()
    {
        new(CellState.Virus, Color.Red),
        new(CellState.PersonHelaty, Color.Green),
        new(CellState.PersonInfected, Color.LightBlue),
        new(CellState.EmtyCell, Color.Black)
    };
    public class ColorTranlation
    {
        public ColorTranlation(CellState cellState, Color cellColor)
        {
            CellState = cellState;
            CellColor = cellColor;
        }
        public CellState CellState { get; set; }
        public Color CellColor { get; set; }
    }
    public ColorList()
    {
    }

    public Color GetColor(CellState cellState)
    {
        Color CellColor = Color.Black;
        foreach (ColorTranlation ColModel in _colorList)
        {
            if (ColModel.CellState == cellState)
            {
                CellColor = ColModel.CellColor;
            }
        }
        return CellColor;
    }

}

