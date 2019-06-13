using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions
{
    public class AllFilesiOSCondition : AssetCondition
    {
        private readonly AssetiOS assetiOS;

        public AllFilesiOSCondition(AssetiOS assetiOS)
        {
            this.assetiOS = assetiOS;
        }

        public override IList<Condition> Verify()
        {
            Condition conditionStandardFile = new Condition("Contains standard file asset (x1)", !string.IsNullOrEmpty(assetiOS.StandardFilePath));
            Condition conditionX2File = new Condition("Contains @2x file asset (x2)", !string.IsNullOrEmpty(assetiOS.X2FilePath));
            Condition conditionX3File = new Condition("Contains @3x file asset (x3)", !string.IsNullOrEmpty(assetiOS.X3FilePath));

            return new List<Condition>() { conditionStandardFile, conditionX2File, conditionX3File };
        }
    }
}
