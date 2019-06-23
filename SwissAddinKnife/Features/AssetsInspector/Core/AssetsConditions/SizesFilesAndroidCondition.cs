using System;
using System.Collections.Generic;
using SwissAddinKnife.Features.AssetsInspector.Services;
using SwissAddinKnife.Extensions;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions
{
    public class SizesFilesAndroidCondition : AssetCondition
    {
        public new static string Description => "Check the resolution of the included assets";

        private readonly AssetAndroid assetAndroid;
        private readonly int sizeMargin;
        private readonly IImageService imageService;

        public SizesFilesAndroidCondition(AssetAndroid assetAndroid, int sizeMargin = 1, IImageService imageService = null)
        {
            this.assetAndroid = assetAndroid;
            this.sizeMargin = sizeMargin;
            if (imageService == null)
                imageService = new ImageService();
            this.imageService = imageService;
        }

        public override IList<Condition> Verify()
        {
            List<Condition> conditions = new List<Condition>();
            var standardSize = imageService.GetImageSize(assetAndroid.StandardFilePath);
            var mdpiSize = imageService.GetImageSize(assetAndroid.MdpiFilePath);

            var baseSize = mdpiSize;
            if (baseSize.IsEmpty)
                baseSize = standardSize;

            if (!string.IsNullOrEmpty(assetAndroid.StandardFilePath) && !string.IsNullOrEmpty(assetAndroid.MdpiFilePath))
            {
                Condition sameResolutionCondition = new Condition("mdpi and standard (drawable) file asset have the same resolution",
                        mdpiSize.SizeEquals(standardSize, epsilon: 0));

                conditions.Add(sameResolutionCondition);
            }        

            if (!string.IsNullOrEmpty(assetAndroid.LdpiFilePath))
            {
                var ldpiSize = imageService.GetImageSize(assetAndroid.LdpiFilePath);
                Condition ldpiSizeCondition = new Condition("ldpi file asset has 0.75x resolution as the standard asset",
                        baseSize.Multiply(0.75).SizeEquals(ldpiSize, epsilon: sizeMargin));

                conditions.Add(ldpiSizeCondition);
            }

            if (!string.IsNullOrEmpty(assetAndroid.HdpiFilePath))
            {
                var hdpiSize = imageService.GetImageSize(assetAndroid.HdpiFilePath);
                Condition hdpiSizeCondition = new Condition("hdpi file asset has 1.5x resolution as the standard asset",
                        baseSize.Multiply(1.5).SizeEquals(hdpiSize, epsilon: sizeMargin));

                conditions.Add(hdpiSizeCondition);
            }

            if (!string.IsNullOrEmpty(assetAndroid.XhdpiFilePath))
            {
                var xhdpiSize = imageService.GetImageSize(assetAndroid.XhdpiFilePath);
                Condition xhdpiSizeCondition = new Condition("xhdpi file asset has 2x resolution as the standard asset",
                                    baseSize.Multiply(2).SizeEquals(xhdpiSize, epsilon: sizeMargin));

                conditions.Add(xhdpiSizeCondition);
            }

            if (!string.IsNullOrEmpty(assetAndroid.XxhdpiFilePath))
            {
                var xxhdpiSize = imageService.GetImageSize(assetAndroid.XxhdpiFilePath);
                Condition xxhdpiSizeCondition = new Condition("xxhdpi file asset has 3x resolution as the standard asset",
                                baseSize.Multiply(3).SizeEquals(xxhdpiSize, epsilon: sizeMargin));

                conditions.Add(xxhdpiSizeCondition);
            }

            if (!string.IsNullOrEmpty(assetAndroid.XxxhdpiFilePath))
            {
                var xxxhdpiSize = imageService.GetImageSize(assetAndroid.XxxhdpiFilePath);
                Condition xxxhdpiSizeCondition = new Condition("xxxhdpi file asset has 4x resolution as the standard asset",
                                baseSize.Multiply(4).SizeEquals(xxxhdpiSize, epsilon: sizeMargin));


                conditions.Add(xxxhdpiSizeCondition);
            }

            return conditions;
        }
    }
}