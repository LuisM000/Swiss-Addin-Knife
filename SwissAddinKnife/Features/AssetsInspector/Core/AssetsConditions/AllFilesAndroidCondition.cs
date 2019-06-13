using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions
{
    public class AllFilesAndroidCondition : AssetCondition
    {
        private readonly AssetAndroid assetAndroid;

        public AllFilesAndroidCondition(AssetAndroid assetAndroid)
        {
            this.assetAndroid = assetAndroid;
        }

        public override IList<Condition> Verify()
        {
            Condition conditionStandardFile = new Condition("Contains standard file asset (x1)", !string.IsNullOrEmpty(assetAndroid.StandardFilePath));
            Condition conditionLdpiFile = new Condition("Contains ldpi file asset", !string.IsNullOrEmpty(assetAndroid.LdpiFilePath));
            Condition conditionMdpiFile = new Condition("Contains mdpi file asset", !string.IsNullOrEmpty(assetAndroid.MdpiFilePath));
            Condition conditionHdpiFile = new Condition("Contains hdpi file asset", !string.IsNullOrEmpty(assetAndroid.HdpiFilePath));
            Condition conditionXhdpiFile = new Condition("Contains xhdpi file asset", !string.IsNullOrEmpty(assetAndroid.XhdpiFilePath));
            Condition conditionXxhdpiFile = new Condition("Contains xxhdpi file asset", !string.IsNullOrEmpty(assetAndroid.XxhdpiFilePath));
            Condition conditionXxxhdpiFile = new Condition("Contains xxxhdpi file asset", !string.IsNullOrEmpty(assetAndroid.XxxhdpiFilePath));

            return new List<Condition>() { conditionStandardFile, conditionLdpiFile, conditionMdpiFile, conditionHdpiFile,
                                            conditionXhdpiFile,conditionXxhdpiFile,conditionXxxhdpiFile};
        }
    }
}

