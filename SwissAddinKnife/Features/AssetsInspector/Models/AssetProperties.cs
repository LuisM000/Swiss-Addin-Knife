using System;
using MonoDevelop.Projects;
using SwissAddinKnife.Features.AssetsInspector.Core;

namespace SwissAddinKnife.Features.AssetsInspector.Models
{
    public class AssetProperties
    {
        public AssetBase Asset { get; }
        public Project Project { get; }

        public AssetProperties(AssetBase asset, Project project)
        {
            Asset = asset;
            Project = project;
        }
    }
}
