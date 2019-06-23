using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.AndroidFilesConditions
{
    public class AndroidContainsHdpiFileCondition : AssetCondition
    {
        private readonly AssetAndroid assetAndroid;

        public AndroidContainsHdpiFileCondition(AssetAndroid assetAndroid)
        {
            this.assetAndroid = assetAndroid;
        }

        public override IList<Condition> Verify()
        {
            Condition conditionHdpiFile = new Condition("Contains hdpi file asset", !string.IsNullOrEmpty(assetAndroid.HdpiFilePath));

            return new List<Condition>() { conditionHdpiFile };
        }
    }
}
