namespace Pingle.UI.Win.Graphics;

public static class Graph
{
    public static void RenderGraphOnGraphics(System.Drawing.Graphics graphics, Rectangle boundingBox,
        params DataSet[] dataSet)
    {
        foreach (var set in dataSet)
        {
            const float pixelWidth = 1.5f;
            const int padding = 5;

            var offset = set.Points.Length > boundingBox.Width
                ? set.Points.Length - boundingBox.Width
                : 0;

            var setSlice = set.Points[offset..];
            var max = Math.Max(0.1f, setSlice.Max(point => point.Point?.Y ?? 0));

            float GetScaledPoint(float y)
            {
                var scaledY = y / max * (boundingBox.Height - 2 * padding)+ padding;
                return scaledY;
            }
            
            using var pen = new Pen(set.DisplayInfo.Color)
            {
                Width = pixelWidth
            };

            for (var j = 1; j < setSlice.Length - padding * 2; j++)
            {
                if (setSlice[j].Point.HasValue && setSlice[j - 1].Point.HasValue)
                {
                    var currentY = setSlice[j].Point.Value.Y;
                    var previousY = j > 0 ? setSlice[j - 1].Point.Value.Y : currentY;
                    var lineStart = new PointF(j - 1 + padding, boundingBox.Height - GetScaledPoint(previousY));
                    var lineEnd = new PointF(j + padding, boundingBox.Height - GetScaledPoint(currentY));
                    graphics.DrawLine(pen, lineStart, lineEnd);
                }

                else
                {
                    var lineStart = new PointF(j - 1, 0);
                    var lineEnd = new PointF(j - 1, boundingBox.Height);
                    using var error = new Pen(Color.Red);
                    error.Width = 0.1f;
                    graphics.DrawLine(error, lineStart, lineEnd);
                }
            }
        }
    }
}
