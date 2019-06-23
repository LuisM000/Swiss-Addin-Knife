using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.AndroidFilesConditions
{
    public class AndroidContainsXhdpiFileCondition : AssetCondition
    {
        public new static string Description => "Contains xhdpi file asset";

        private readonly AssetAndroid assetAndroid;

        public AndroidContainsXhdpiFileCondition(AssetAndroid assetAndroid)
        {
            this.assetAndroid = assetAndroid;
        }

        public override IList<Condition> Verify()
        {
            Condition conditionXhdpiFile = new Condition(Description, !string.IsNullOrEmpty(assetAndroid.XhdpiFilePath));

            return new List<Condition>() { conditionXhdpiFile };
        }
    }
}
