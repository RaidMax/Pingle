namespace Pingle.UI.Win.Graphics;

public class DataSet
{
    public DisplayInfo DisplayInfo { get; set; } = new();
    public PointF[] Points { get; set; } = Array.Empty<PointF>();
}
