using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.AndroidFilesConditions
{
    public class AndroidContainsXxxhdpiFileCondition : AssetCondition
    {
        private readonly AssetAndroid assetAndroid;

        public AndroidContainsXxxhdpiFileCondition(AssetAndroid assetAndroid)
        {
            this.assetAndroid = assetAndroid;
        }

        public override IList<Condition> Verify()
        {
            Condition conditionXxxhdpiFile = new Condition("Contains xxxhdpi file asset", !string.IsNullOrEmpty(assetAndroid.XxxhdpiFilePath));

            return new List<Condition>() { conditionXxxhdpiFile };
        }
    }
}
