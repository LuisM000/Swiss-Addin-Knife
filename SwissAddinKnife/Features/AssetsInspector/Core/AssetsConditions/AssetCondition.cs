using System;
using System.Collections.Generic;

namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions
{
    public abstract class AssetCondition
    {
        public abstract IList<Condition> Verify();
    }
}
