using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.AndroidFilesConditions
{
    public class AndroidContainsXxhdpiFileCondition : AssetCondition
    {
        public new static string Description => "Contains xxhdpi file asset";

        private readonly AssetAndroid assetAndroid;

        public AndroidContainsXxhdpiFileCondition(AssetAndroid assetAndroid)
        {
            this.assetAndroid = assetAndroid;
        }

        public override IList<Condition> Verify()
        {
            Condition conditionXxhdpiFile = new Condition(Description, !string.IsNullOrEmpty(assetAndroid.XxhdpiFilePath));

            return new List<Condition>() { conditionXxhdpiFile };
        }
    }
}
