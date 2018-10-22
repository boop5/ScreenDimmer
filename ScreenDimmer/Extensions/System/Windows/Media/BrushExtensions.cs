using System.Windows.Media;

namespace ScreenDimmer.Extensions.System.Windows.Media
{
    internal static class BrushExtensions
    {
        public static Color GetColor(this Brush brush)
        {
            var a = ((Color) brush.GetValue(SolidColorBrush.ColorProperty)).A;
            var g = ((Color) brush.GetValue(SolidColorBrush.ColorProperty)).G;
            var r = ((Color) brush.GetValue(SolidColorBrush.ColorProperty)).R;
            var b = ((Color) brush.GetValue(SolidColorBrush.ColorProperty)).B;

            return Color.FromArgb(a, r, g, b);
        }
    }
}