using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.AndroidFilesConditions
{
    public class AndroidContainsXhdpiFileCondition : AssetCondition
    {
        private readonly AssetAndroid assetAndroid;

        public AndroidContainsXhdpiFileCondition(AssetAndroid assetAndroid)
        {
            this.assetAndroid = assetAndroid;
        }

        public override IList<Condition> Verify()
        {
            Condition conditionXhdpiFile = new Condition("Contains xhdpi file asset", !string.IsNullOrEmpty(assetAndroid.XhdpiFilePath));

            return new List<Condition>() { conditionXhdpiFile };
        }
    }
}
