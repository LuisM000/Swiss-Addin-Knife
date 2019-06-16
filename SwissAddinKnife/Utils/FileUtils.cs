using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SwissAddinKnife.Utils
{
    public static class FileUtils
    {
        static readonly string[] validImageExtensions = { "jpg", "bmp", "png" };

        public static string[] GetFolderImages(string searchFolder)
        {
            List<string> filesFound = new List<string>();

            foreach (var filter in validImageExtensions)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, string.Format("*.{0}", filter), SearchOption.AllDirectories));
            }

            return filesFound.ToArray();
        }
    }
}
