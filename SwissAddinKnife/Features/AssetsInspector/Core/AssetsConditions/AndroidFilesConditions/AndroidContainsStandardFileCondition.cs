using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.AndroidFilesConditions
{
    public class AndroidContainsStandardFileCondition : AssetCondition
    {
        public new static string Description => "Contains standard (drawable) file asset (x1)";

        private readonly AssetAndroid assetAndroid;

        public AndroidContainsStandardFileCondition(AssetAndroid assetAndroid)
        {
            this.assetAndroid = assetAndroid;
        }

        public override IList<Condition> Verify()
        {
            Condition conditionStandardFile = new Condition(Description, !string.IsNullOrEmpty(assetAndroid.StandardFilePath));
           
            return new List<Condition>() { conditionStandardFile };
        }
    }
}
