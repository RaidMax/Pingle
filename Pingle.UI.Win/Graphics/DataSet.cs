namespace Pingle.UI.Win.Graphics;

public class DataSet
{
    public DisplayInfo DisplayInfo { get; set; } = new();
    public DataItem[] Points { get; set; } = Array.Empty<DataItem>();
}

public class DataItem
{
    public PointF? Point { get; set; }
}
