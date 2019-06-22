﻿using System;
using System.Collections.Generic;
using SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions;
using SwissAddinKnife.Features.AssetsInspector.Core.File;
using SwissAddinKnife.Utils;

namespace SwissAddinKnife.Features.AssetsInspector.Core
{
    public abstract class AssetBase
    {
        public string Identifier { get; }

        public abstract IList<FileBase> Files { get; }

        protected AssetBase(string identifier)
        {
            this.Identifier = identifier;
        }

        public abstract bool CanBeAdded(string filePath);
        public abstract bool Add(string filePath);

        public abstract Result<IList<Condition>> Analize();
    }
}
