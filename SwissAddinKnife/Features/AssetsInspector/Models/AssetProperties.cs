using System;
using System.Collections.Generic;
using MonoDevelop.Projects;
using SwissAddinKnife.Features.AssetsInspector.Core;
using SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions;
using SwissAddinKnife.Utils;

namespace SwissAddinKnife.Features.AssetsInspector.Models
{
    public class AssetProperties
    {
        public AssetBase Asset { get; }
        public Project Project { get; }
        public Result<IList<Condition>> Result { get; set; }

        public AssetProperties(AssetBase asset, Project project)
        {
            Asset = asset;
            Project = project;
        }
    }
}
