using System;
using System.IO;

namespace SwissAddinKnife.Features.AssetsInspector.Core.File
{
    public sealed class FileAndroid:FileBase
    {
        public override string ReducedPath { get; }

        public FileAndroid(string filePath) : base(filePath)
        {
            var directory = new DirectoryInfo(filePath);
            ReducedPath = System.IO.Path.Combine(directory.Parent.Parent.Name, directory.Parent.Name, directory.Name);
        }
    }
}
