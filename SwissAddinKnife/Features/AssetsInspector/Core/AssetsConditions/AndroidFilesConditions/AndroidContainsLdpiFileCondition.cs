using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.AndroidFilesConditions
{
    public class AndroidContainsLdpiFileCondition : AssetCondition
    {
        private readonly AssetAndroid assetAndroid;

        public AndroidContainsLdpiFileCondition(AssetAndroid assetAndroid)
        {
            this.assetAndroid = assetAndroid;
        }

        public override IList<Condition> Verify()
        {
            Condition conditionLdpiFile = new Condition("Contains ldpi file asset", !string.IsNullOrEmpty(assetAndroid.LdpiFilePath));

            return new List<Condition>() { conditionLdpiFile };
        }
    }
}
