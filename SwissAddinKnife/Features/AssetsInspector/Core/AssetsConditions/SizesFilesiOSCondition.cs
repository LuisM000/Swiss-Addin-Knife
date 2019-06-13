using System;
using System.Collections.Generic;
using SwissAddinKnife.Features.AssetsInspector.Services;
using SwissAddinKnife.Extensions;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions
{
    public class SizesFilesiOSCondition : AssetCondition
    {
        private readonly AssetiOS assetiOS;
        private readonly IImageService imageService;

        public SizesFilesiOSCondition(AssetiOS assetiOS, IImageService imageService = null)
        {
            this.assetiOS = assetiOS;
            if (imageService == null)
                imageService = new ImageService();
            this.imageService = imageService;
        }

        public override IList<Condition> Verify()
        {            
            var standardSize = imageService.GetImageSize(assetiOS.StandardFilePath);
            var x2Size = imageService.GetImageSize(assetiOS.X2FilePath);
            var x3Size = imageService.GetImageSize(assetiOS.X3FilePath);

            Condition conditionX2Size = new Condition("@2x file asset has twice resolution as the standard asset",
                standardSize.Multiply(2).SizeEquals(x2Size, epsilon: 1));

            Condition conditionX3Size = new Condition("@3x file asset has triple resolution as the standard asset",
                standardSize.Multiply(3).SizeEquals(x3Size, epsilon: 1));

            return new List<Condition>() { conditionX2Size, conditionX3Size };
        }
    }
}