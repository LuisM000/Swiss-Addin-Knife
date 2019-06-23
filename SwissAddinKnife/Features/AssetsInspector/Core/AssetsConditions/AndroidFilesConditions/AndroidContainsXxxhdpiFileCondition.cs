using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.AndroidFilesConditions
{
    public class AndroidContainsXxxhdpiFileCondition : AssetCondition
    {
        public new static string Description => "Contains xxxhdpi file asset";

        private readonly AssetAndroid assetAndroid;

        public AndroidContainsXxxhdpiFileCondition(AssetAndroid assetAndroid)
        {
            this.assetAndroid = assetAndroid;
        }

        public override IList<Condition> Verify()
        {
            Condition conditionXxxhdpiFile = new Condition(Description, !string.IsNullOrEmpty(assetAndroid.XxxhdpiFilePath));

            return new List<Condition>() { conditionXxxhdpiFile };
        }
    }
}
