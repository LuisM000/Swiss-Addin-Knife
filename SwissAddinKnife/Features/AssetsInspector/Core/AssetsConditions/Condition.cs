using System;
namespace SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions
{
    public class Condition
    {
        public string Description { get; }
        public bool IsFulfilled { get; }

        public Condition(string description, bool isFulfilled)
        {
            Description = description;
            IsFulfilled = isFulfilled;
        }        
    }
}
