using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.AndroidFilesConditions
{
    public class AndroidContainsLdpiFileCondition : AssetCondition
    {
        public new static string Description => "Contains ldpi file asset";

        private readonly AssetAndroid assetAndroid;

        public AndroidContainsLdpiFileCondition(AssetAndroid assetAndroid)
        {
            this.assetAndroid = assetAndroid;
        }

        public override IList<Condition> Verify()
        {
            Condition conditionLdpiFile = new Condition(Description, !string.IsNullOrEmpty(assetAndroid.LdpiFilePath));

            return new List<Condition>() { conditionLdpiFile };
        }
    }
}
