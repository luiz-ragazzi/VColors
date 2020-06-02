using System;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace VColors
{
    public class ColorViewModel : BaseViewModel
    {
        public ColorViewModel()
        {
            SortCommand = new Command(async () => await ExecuteSortCommand());
            RandomCommand = new Command(async () => await ExecuteRandomCommand());
            policy.AbsoluteExpiration =
                      DateTimeOffset.Now.AddSeconds(180.0);

            model = new ImageModel();

        }

        ImageModel model = null;

        RenderTargetBitmap bitmapRender;
        public RenderTargetBitmap BitmapRender { get => bitmapRender; set => SetProperty(ref bitmapRender, value); }

        public ICommand SortCommand { get; }
        public ICommand RandomCommand { get; }

        ObjectCache cache = MemoryCache.Default;

        CacheItemPolicy policy = new CacheItemPolicy();
      
        async Task ExecuteSortCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            RenderTargetBitmap bitmap = cache["sort"] as RenderTargetBitmap;

            try
            {
                if (bitmap == null)
                {
                   
                    bitmap = await model.DrawColorSortedAsync();
                    cache.Set("sort", bitmap, policy);
                }

                BitmapRender = bitmap;
            }
            finally
            {
                IsBusy = false;
            }

        }

        async Task ExecuteRandomCommand()
        {
            RenderTargetBitmap bitmap = cache["random"] as RenderTargetBitmap;

            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                if (bitmap == null)
                {
                    bitmap = await model.DrawColorRandomAsync();
                    cache.Set("random", bitmap, policy);

                }
                BitmapRender = bitmap;
            }
            finally
            {

                IsBusy = false;
            }




        }
    }
}

