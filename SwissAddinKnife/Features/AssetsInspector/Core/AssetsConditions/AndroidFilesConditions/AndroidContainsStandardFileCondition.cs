using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.AndroidFilesConditions
{
    public class AndroidContainsStandardFileCondition : AssetCondition
    {
        private readonly AssetAndroid assetAndroid;

        public AndroidContainsStandardFileCondition(AssetAndroid assetAndroid)
        {
            this.assetAndroid = assetAndroid;
        }

        public override IList<Condition> Verify()
        {
            Condition conditionStandardFile = new Condition("Contains standard file asset (x1)", !string.IsNullOrEmpty(assetAndroid.StandardFilePath));
           
            return new List<Condition>() { conditionStandardFile };
        }
    }
}
