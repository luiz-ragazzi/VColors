using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;


namespace VColors
{

    
    public class ColorService
    {
        public ColorService()
        {
            Colors = new Dictionary<float, List<Color>>();
            HueSet = new HashSet<float>();

            
            
        }
        Dictionary<float, List<Color>> Colors { get; set; }
        public HashSet<float> HueSet { get; private set; }

        public async Task<Dictionary<float, List<Color>>> ShowColors()
        {
            if (Colors == null || Colors.Count == 0)
            {
                Colors = await GetKnownColorsAsync();
            }
            return Colors;
        }

        private List<Color> BuidColors()
        {
            var shades = new List<System.Drawing.Color>();
            var r = new Random();
           
            for (int i = 0; i < 500000; i++)
            {
                var alpha = r.Next(0, 255);
                var red = r.Next(0, 255);
                var green = r.Next(0, 255);
                var blue = r.Next(0, 255);
                var color = System.Drawing.Color.FromArgb(alpha, red, green, blue);
                shades.Add(color);
            }

            return shades;
           
        }
       
        private async Task<Dictionary<float, List<Color>>> GetKnownColorsAsync()
        {
            var shades = new List<System.Drawing.Color>();
            var dic = new Dictionary<float, List<Color>>();

            

            try
            {
                await Task.Run(() =>
                {
                    //for (KnownColor enumValue = 0;
                    //enumValue <= KnownColor.YellowGreen; enumValue++)
                    //{

                    //    shades.Add(System.Drawing.Color.FromKnownColor(enumValue));
                    //}
                    shades = BuidColors();

                    for (int i = 0; i < shades.Count; i++)
                    {

                        var hh = shades[i].GetHue();
                        if (dic.ContainsKey(hh))
                        {
                            dic[hh].Add(shades[i]);

                        }
                        else
                        {
                            dic.Add(hh, new List<Color>() { shades[i] });
                            HueSet.Add(hh);
                        }

                        ///

                    }
                    
                });

                return dic;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return null;

        }


    }
}
