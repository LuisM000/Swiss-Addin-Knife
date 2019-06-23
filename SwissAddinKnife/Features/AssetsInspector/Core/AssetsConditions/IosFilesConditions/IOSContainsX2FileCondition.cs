using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.IosFilesConditions
{
    public class IOSContainsX2FileCondition: AssetCondition
    {
        private readonly AssetiOS assetiOS;

        public IOSContainsX2FileCondition(AssetiOS assetiOS)
        {
            this.assetiOS = assetiOS;
        }

        public override IList<Condition> Verify()
        {
            Condition conditionX2File = new Condition("Contains @2x file asset (x2)", !string.IsNullOrEmpty(assetiOS.X2FilePath));
           
            return new List<Condition>() { conditionX2File };
        }
    }
}
