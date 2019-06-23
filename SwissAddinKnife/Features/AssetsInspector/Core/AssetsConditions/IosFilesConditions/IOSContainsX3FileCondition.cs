using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.IosFilesConditions
{
    public class IOSContainsX3FileCondition : AssetCondition
    {
        public new static string Description => "Contains @3x file asset";

        private readonly AssetiOS assetiOS;

        public IOSContainsX3FileCondition(AssetiOS assetiOS)
        {
            this.assetiOS = assetiOS;
        }

        public override IList<Condition> Verify()
        {
            Condition conditionX3File = new Condition(Description, !string.IsNullOrEmpty(assetiOS.X3FilePath));

            return new List<Condition>() { conditionX3File };
        }
    }
}
