using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions
{
    public class DrawableAssetAndroidCondition : AssetCondition
    {
        public new static string Description => "The drawable file is not necessary, when all available resolutions exist (ldpi, mdpi...)";

        private readonly AssetAndroid assetAndroid;

        public DrawableAssetAndroidCondition(AssetAndroid assetAndroid)
        {
            this.assetAndroid = assetAndroid;
        }

        public override IList<Condition> Verify()
        {
            bool containsAllAssets = !string.IsNullOrEmpty(assetAndroid.StandardFilePath) && !string.IsNullOrEmpty(assetAndroid.LdpiFilePath) &&
                !string.IsNullOrEmpty(assetAndroid.MdpiFilePath) && !string.IsNullOrEmpty(assetAndroid.HdpiFilePath) &&
                !string.IsNullOrEmpty(assetAndroid.XhdpiFilePath) && !string.IsNullOrEmpty(assetAndroid.XxhdpiFilePath) &&
                !string.IsNullOrEmpty(assetAndroid.XxxhdpiFilePath);

            return new List<Condition>() { new Condition(Description, !containsAllAssets) };
        }
    }
}
