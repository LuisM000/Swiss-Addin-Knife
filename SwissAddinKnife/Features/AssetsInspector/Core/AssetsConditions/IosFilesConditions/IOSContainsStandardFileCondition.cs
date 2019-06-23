using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.IosFilesConditions
{
    public class IOSContainsStandardFileCondition : AssetCondition
    {
        public new static string Description => "Contains standard file asset (x1)";

        private readonly AssetiOS assetiOS;

        public IOSContainsStandardFileCondition(AssetiOS assetiOS)
        {
            this.assetiOS = assetiOS;
        }

        public override IList<Condition> Verify()
        {
            Condition conditionStandardFile = new Condition(Description, !string.IsNullOrEmpty(assetiOS.StandardFilePath));
           
            return new List<Condition>() { conditionStandardFile };
        }
    }
}
