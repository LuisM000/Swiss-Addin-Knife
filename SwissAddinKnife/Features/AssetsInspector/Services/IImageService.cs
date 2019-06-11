using System;
using System.Drawing;

namespace SwissAddinKnife.Features.AssetsInspector.Services
{
    public interface IImageService
    {
        Size GetImageSize(string imagePath);
    }
}
