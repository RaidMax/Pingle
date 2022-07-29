namespace Pingle.UI.Win.Graphics;

public static class Graph
{
    public static void RenderGraphOnGraphics(System.Drawing.Graphics graphics, Rectangle boundingBox, params DataSet[] datum)
    {
        //var max = datum.SelectMany(data => data.Points).Max(point => point.Y);

        foreach (var set in datum)
        {
            var max = Math.Max(1, set.Points.Max(point => point.Y));
            const float pixelWidth = 1.5f;
            var i = set.Points.Length > boundingBox.Width
                ? set.Points.Length - boundingBox.Width
                : 0;

            float ScaledHeight(float y)
            {
                var scaledHeight = y / max * boundingBox.Height;
                return scaledHeight;
            }

            using var pen = new Pen(set.DisplayInfo.Color)
            {
                Width = pixelWidth
            };

            var j = 1;
            for (; i < set.Points.Length; i++)
            {
                var currentY = set.Points[i].Y;
                var previousY = i > 0 ? set.Points[i - 1].Y : currentY;
                var lineStart = new PointF(j - 1, boundingBox.Height - ScaledHeight(previousY));
                var lineEnd = new PointF(j, boundingBox.Height - ScaledHeight(currentY));
                graphics.DrawLine(pen, lineStart, lineEnd);

                j += 1;
            }
        }
    }
}
