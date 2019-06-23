using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.AndroidFilesConditions
{
    public class AndroidContainsHdpiFileCondition : AssetCondition
    {
        public new static string Description => "Contains hdpi file asset";

        private readonly AssetAndroid assetAndroid;

        public AndroidContainsHdpiFileCondition(AssetAndroid assetAndroid)
        {
            this.assetAndroid = assetAndroid;
        }

        public override IList<Condition> Verify()
        {
            Condition conditionHdpiFile = new Condition(Description, !string.IsNullOrEmpty(assetAndroid.HdpiFilePath));

            return new List<Condition>() { conditionHdpiFile };
        }
    }
}
