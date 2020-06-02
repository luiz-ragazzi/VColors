using System.Collections.Generic;
using System.Windows.Media;

namespace VColors
{
    public class ColorBrush
    {
        public float Hue { get; set; }
        public List<SolidColorBrush> Brushes = new List<SolidColorBrush>();
    }
}
