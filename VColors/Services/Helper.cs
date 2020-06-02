using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace VColors
{
    public static class Helper
    {
        public static RenderTargetBitmap DrawBitmapSorted(this Dictionary<float, List<KnownColor>> Colors, HashSet<float> set)
        {          

            var hueBrush = new List<ColorBrush>();

            var hues = (from x in set
                        orderby x descending
                        select x
                           ).ToHashSet();


            foreach (var hue in hues)
            {
                List<SolidColorBrush> Brushes = new List<SolidColorBrush>();
                var colors = Colors[hue];
                foreach (var c in colors)
                {
                    var color = System.Drawing.Color.FromKnownColor(c);
                    var brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
                    Brushes.Add(brush);
                }
                hueBrush.Add(new ColorBrush() { Brushes = Brushes, Hue = hue });
            }

            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                var rand = new Random();

                for (int a = 0; a < 100; a++)
                {
                    for (int i = 0; i < hueBrush.Count; i++)
                    {
                        for (int j = 0; j < hueBrush[i].Brushes.Count; j++)
                        {

                            dc.DrawRectangle(hueBrush[i].Brushes[j], null, new Rect(i + a, rand.NextDouble() * 200, 1, 1));
                        }
                    }
                }

                dc.Close();

            }

            RenderTargetBitmap rtb = new RenderTargetBitmap(200, 200, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(dv);
            rtb.Freeze();

            return rtb;
        }


        public static RenderTargetBitmap DrawBitmap(this Dictionary<float, List<KnownColor>> Colors)
        {
           
            var brushes = new List<SolidColorBrush>();

            foreach (KeyValuePair<float, List<KnownColor>> c in Colors)
            {
                foreach (var color in c.Value)
                {
                    var thecolor = System.Drawing.Color.FromKnownColor(color);
                    var brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(thecolor.A, thecolor.R, thecolor.G, thecolor.B));
                    brushes.Add(brush);
                }
            }

            var rtb = new RenderTargetBitmap(200, 200, 96, 96, PixelFormats.Pbgra32);

            var dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                Random rand = new Random();

                for (int x = 0; x < 50; x++)
                {
                    for (int i = 0; i < brushes.Count; i++)
                    {

                        dc.DrawRectangle(brushes[i], null, new Rect(rand.NextDouble() * 200, rand.NextDouble() * 200, 1, 1));

                    }
                }
                dc.Close();
            }
            rtb.Render(dv);
            rtb.Freeze();

            return rtb;

        }
    }
}
