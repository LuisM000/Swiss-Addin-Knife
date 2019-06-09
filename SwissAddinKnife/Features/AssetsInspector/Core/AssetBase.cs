using System;
namespace SwissAddinKnife.Features.AssetsInspector.Core
{
    public abstract class AssetBase
    {
        public string Identifier { get; }

        protected AssetBase(string identifier)
        {
            this.Identifier = identifier;
        }

        public abstract bool CanBeAdded(string filePath);
        public abstract bool Add(string filePath);
    }
}
