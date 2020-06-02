
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


namespace VColors
{
    public class ImageModel
    {        
      
        public ImageModel()
        {
            service = new ColorService();
        }
        ColorService service = null;
        public async Task<RenderTargetBitmap> DrawColorSortedAsync()
        {
                 
            var colors = await service.ShowColors();
            RenderTargetBitmap bm = null;
            await Task.Run(() =>
            {
                bm = colors.DrawBitmapSorted(service.HueSet);
            });            

            return bm;            

        }

        public async Task<RenderTargetBitmap> DrawColorRandomAsync()
        {
          
            var colors = await service.ShowColors();
            RenderTargetBitmap bm = null;
            await Task.Run(() =>
            {
                bm = colors.DrawBitmap();
            });


            return bm;
           
        }       
    }
}
