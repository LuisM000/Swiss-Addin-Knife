using System;
using System.Collections.Generic;
using SwissAddinKnife.Features.AssetsInspector.Services;
using SwissAddinKnife.Extensions;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions
{
    public class SizesFilesiOSCondition : AssetCondition
    {
        public new static string Description => "Check the resolution of the included assets";

        private readonly AssetiOS assetiOS;
        private readonly int sizeMargin;
        private readonly IImageService imageService;

        public SizesFilesiOSCondition(AssetiOS assetiOS, int sizeMargin = 1, IImageService imageService = null)
        {
            this.assetiOS = assetiOS;
            this.sizeMargin = sizeMargin;
            if (imageService == null)
                imageService = new ImageService();
            this.imageService = imageService;
        }

        public override IList<Condition> Verify()
        {
            List<Condition> conditions = new List<Condition>();
            var standardSize = imageService.GetImageSize(assetiOS.StandardFilePath);

            if(!string.IsNullOrEmpty(assetiOS.X2FilePath))
            {
                var x2Size = imageService.GetImageSize(assetiOS.X2FilePath);
                Condition conditionX2Size = new Condition("@2x file asset has twice resolution as the standard asset",
                                 standardSize.Multiply(2).SizeEquals(x2Size, epsilon: sizeMargin));

                conditions.Add(conditionX2Size);
            }

            if (!string.IsNullOrEmpty(assetiOS.X3FilePath))
            {
                var x3Size = imageService.GetImageSize(assetiOS.X3FilePath);
                Condition conditionX3Size = new Condition("@3x file asset has triple resolution as the standard asset",
                    standardSize.Multiply(3).SizeEquals(x3Size, epsilon: sizeMargin));

                conditions.Add(conditionX3Size);
            }
          
            return conditions;
        }
    }
}