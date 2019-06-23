using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.IosFilesConditions
{
    public class IOSContainsStandardFileCondition : AssetCondition
    {
        private readonly AssetiOS assetiOS;

        public IOSContainsStandardFileCondition(AssetiOS assetiOS)
        {
            this.assetiOS = assetiOS;
        }

        public override IList<Condition> Verify()
        {
            Condition conditionStandardFile = new Condition("Contains standard file asset (x1)", !string.IsNullOrEmpty(assetiOS.StandardFilePath));
           
            return new List<Condition>() { conditionStandardFile };
        }
    }
}
