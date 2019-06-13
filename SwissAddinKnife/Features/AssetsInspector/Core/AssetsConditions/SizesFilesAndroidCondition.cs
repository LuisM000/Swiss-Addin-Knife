using System;
using System.Collections.Generic;
using SwissAddinKnife.Features.AssetsInspector.Services;
using SwissAddinKnife.Extensions;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions
{
    public class SizesFilesAndroidCondition : AssetCondition
    {
        private readonly AssetAndroid assetAndroid;
        private readonly IImageService imageService;

        public SizesFilesAndroidCondition(AssetAndroid assetAndroid, IImageService imageService = null)
        {
            this.assetAndroid = assetAndroid;
            if (imageService == null)
                imageService = new ImageService();
            this.imageService = imageService;
        }

        public override IList<Condition> Verify()
        {
            var standardSize = imageService.GetImageSize(assetAndroid.StandardFilePath);
            var ldpiSize = imageService.GetImageSize(assetAndroid.LdpiFilePath);
            var mdpiSize = imageService.GetImageSize(assetAndroid.MdpiFilePath);
            var hdpiSize = imageService.GetImageSize(assetAndroid.HdpiFilePath);
            var xhdpiSize = imageService.GetImageSize(assetAndroid.XhdpiFilePath);
            var xxhdpiSize = imageService.GetImageSize(assetAndroid.XxhdpiFilePath);
            var xxxhdpiSize = imageService.GetImageSize(assetAndroid.XxxhdpiFilePath);

            Condition ldpiSizeCondition = new Condition("ldpi file asset has 0.75x resolution as the standard asset",
                standardSize.Multiply(0.75).SizeEquals(ldpiSize, epsilon: 1));
            Condition mdpiSizeCondition = new Condition("mdpi file asset has same resolution as the standard asset",
                standardSize.SizeEquals(mdpiSize, epsilon: 0));
            Condition hdpiSizeCondition = new Condition("hdpi file asset has 1.5x resolution as the standard asset",
                standardSize.Multiply(1.5).SizeEquals(hdpiSize, epsilon: 1));
            Condition xhdpiSizeCondition = new Condition("xhdpi file asset has 2x resolution as the standard asset",
                standardSize.Multiply(2).SizeEquals(xhdpiSize, epsilon: 1));
            Condition xxhdpiSizeCondition = new Condition("xxhdpi file asset has 3x resolution as the standard asset",
                standardSize.Multiply(3).SizeEquals(xxhdpiSize, epsilon: 1));
            Condition xxxhdpiSizeCondition = new Condition("xxxhdpi file asset has 4x resolution as the standard asset",
               standardSize.Multiply(4).SizeEquals(xxxhdpiSize, epsilon: 1));


            return new List<Condition>() { ldpiSizeCondition, mdpiSizeCondition, hdpiSizeCondition,
                                            xhdpiSizeCondition, xxhdpiSizeCondition, xxxhdpiSizeCondition};
        }
    }
}