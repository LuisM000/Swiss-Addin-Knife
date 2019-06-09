using System;
using System.IO;

namespace SwissAddinKnife.Features.AssetsInspector.Core
{
    public class AssetiOS : AssetBase
    {
        public string StandardFilePath { get; private set; }
        public string X2FilePath { get; private set; }
        public string X3FilePath { get; private set; }

        public AssetiOS(string filePath) : base(ExtractIdentifierFromFilePath(filePath))
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException(nameof(filePath) + " can't be null or empty", nameof(filePath));

            this.CheckAndSetFilePath(filePath);
        }


        public override bool CanBeAdded(string filePath)
        {
            bool isTheSameIdentifier = base.Identifier.Equals(ExtractIdentifierFromFilePath(filePath), StringComparison.Ordinal);
            return isTheSameIdentifier;
        }

        public override bool Add(string filePath)
        {
            if (!CanBeAdded(filePath))
                return false;

            this.CheckAndSetFilePath(filePath);

            return true;
        }


        private void CheckAndSetFilePath(string filePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            if (fileName.EndsWith("@2x", StringComparison.Ordinal))
                this.X2FilePath = filePath;
            else if (fileName.EndsWith("@3x", StringComparison.Ordinal))
                this.X3FilePath = filePath;
            else
                this.StandardFilePath = filePath;
        }

        private static string ExtractIdentifierFromFilePath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException(nameof(filePath) + " can't be null or empty", nameof(filePath));

            var identifier = Path.GetFileNameWithoutExtension(filePath);
            if (identifier.EndsWith("@2x", StringComparison.Ordinal) ||
                identifier.EndsWith("@3x", StringComparison.Ordinal))
            {
                identifier = identifier.Remove(identifier.Length - 3, 3);
            }

            return identifier;
        }       
    }
}
