using System;
using System.IO;

namespace SwissAddinKnife.Features.AssetsInspector.Core.File
{
    public sealed class FileiOS:FileBase
    {
        public override string ReducedPath { get; }

        public FileiOS(string filePath) : base(filePath)
        {
            var directory = new DirectoryInfo(filePath);
            this.ReducedPath = System.IO.Path.Combine(directory.Parent.Name, directory.Name);
        }
    }
}
