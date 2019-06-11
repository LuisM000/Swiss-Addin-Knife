using System;
using System.Drawing;
using Foundation;
using ImageIO;

namespace SwissAddinKnife.Features.AssetsInspector.Services
{
    public class ImageService : IImageService
    {
        public Size GetImageSize(string imagePath)
        {
            try
            {
                NSUrl url = new NSUrl(path: imagePath, isDir: false);
                CGImageSource myImageSource = CGImageSource.FromUrl(url, null);
                var ns = new NSDictionary();

                //Dimensions
                NSObject width;
                NSObject height;

                using (NSDictionary imageProperties = myImageSource.CopyProperties(ns, 0))
                {
                    var tiff = imageProperties.ObjectForKey(CGImageProperties.TIFFDictionary) as NSDictionary;
                    width = imageProperties[CGImageProperties.PixelWidth];
                    height = imageProperties[CGImageProperties.PixelHeight];
                }

                return new Size(Convert.ToInt32(height.ToString()),
                    Convert.ToInt32(width.ToString()));
            }
            catch
            {
                return Size.Empty;
            }
        }
    }
}
