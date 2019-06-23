using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions
{
    public abstract class AssetCondition
    {
        public static string Description { get; }
        public abstract IList<Condition> Verify();
    }
}
