using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.AndroidFilesConditions
{
    public class AndroidContainsMdpiFileCondition : AssetCondition
    {
        private readonly AssetAndroid assetAndroid;

        public AndroidContainsMdpiFileCondition(AssetAndroid assetAndroid)
        {
            this.assetAndroid = assetAndroid;
        }

        public override IList<Condition> Verify()
        {
            Condition conditionMdpiFile = new Condition("Contains mdpi file asset", !string.IsNullOrEmpty(assetAndroid.MdpiFilePath));

            return new List<Condition>() { conditionMdpiFile };
        }
    }
}
