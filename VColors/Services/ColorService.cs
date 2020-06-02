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
            Colors = new Dictionary<float, List<KnownColor>>();
            HueSet = new HashSet<float>();

            
            
        }
        Dictionary<float, List<KnownColor>> Colors { get; set; }
        public HashSet<float> HueSet { get; private set; }

        public async Task<Dictionary<float, List<KnownColor>>> ShowColors()
        {
            if (Colors == null || Colors.Count == 0)
            {
                Colors = await GetKnownColorsAsync();
            }
            return Colors;
        }
       
        private async Task<Dictionary<float, List<KnownColor>>> GetKnownColorsAsync()
        {
            var shades = new List<System.Drawing.Color>();
            var dic = new Dictionary<float, List<KnownColor>>();

            try
            {
                await Task.Run(() =>
                {
                    for (KnownColor enumValue = 0;
                    enumValue <= KnownColor.YellowGreen; enumValue++)
                    {

                        shades.Add(System.Drawing.Color.FromKnownColor(enumValue));
                    }

                    for (KnownColor enumValue = 0;
                        enumValue <= KnownColor.YellowGreen; enumValue++)
                    {
                        for (int i = 0; i < shades.Count; i++)
                        {

                            var someColor = System.Drawing.Color.FromKnownColor(enumValue);
                            var hue = someColor.GetHue();
                            if (shades[i].GetHue() == hue)
                            {
                                if (dic.ContainsKey(hue))
                                {
                                    dic[hue].Add(enumValue);

                                }
                                else
                                {
                                    dic.Add(hue, new List<KnownColor>() { enumValue });
                                    HueSet.Add(hue);
                                }
                            }
                        }
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
