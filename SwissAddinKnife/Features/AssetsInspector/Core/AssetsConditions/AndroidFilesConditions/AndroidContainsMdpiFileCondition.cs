using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.AndroidFilesConditions
{
    public class AndroidContainsMdpiFileCondition : AssetCondition
    {
        public new static string Description => "Contains mdpi file asset";

        private readonly AssetAndroid assetAndroid;

        public AndroidContainsMdpiFileCondition(AssetAndroid assetAndroid)
        {
            this.assetAndroid = assetAndroid;
        }

        public override IList<Condition> Verify()
        {
            Condition conditionMdpiFile = new Condition(Description, !string.IsNullOrEmpty(assetAndroid.MdpiFilePath));

            return new List<Condition>() { conditionMdpiFile };
        }
    }
}
