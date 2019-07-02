using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions
{
    public class DrawableAssetAndroidCondition : AssetCondition
    {
        public new static string Description => "The drawable asset is not necessary when mdpi asset exists";

        private readonly AssetAndroid assetAndroid;

        public DrawableAssetAndroidCondition(AssetAndroid assetAndroid)
        {
            this.assetAndroid = assetAndroid;
        }

        public override IList<Condition> Verify()
        {
            bool containsDrawableAndMdpi = !string.IsNullOrEmpty(assetAndroid.StandardFilePath) &&
                                            !string.IsNullOrEmpty(assetAndroid.MdpiFilePath);

            return new List<Condition>() { new Condition(Description, !containsDrawableAndMdpi) };
        }
    }
}
