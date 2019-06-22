using System;
namespace SwissAddinKnife.Features.AssetsInspector.Core.File
{
    public abstract class FileBase
    {          
        public string Path { get; }
        public abstract string ReducedPath { get; }

        protected FileBase(string filePath)
        {
            this.Path = filePath;
        }
    }
}
