using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions;
using SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.AndroidFilesConditions;
using SwissAddinKnife.Features.AssetsInspector.Core.File;
using SwissAddinKnife.Utils;

namespace SwissAddinKnife.Features.AssetsInspector.Core
{
    public class AssetAndroid : AssetBase
    {       
        public virtual string StandardFilePath { get; private set; }
        public virtual string LdpiFilePath { get; private set; }
        public virtual string MdpiFilePath { get; private set; }
        public virtual string HdpiFilePath { get; private set; }
        public virtual string XhdpiFilePath { get; private set; }
        public virtual string XxhdpiFilePath { get; private set; }
        public virtual string XxxhdpiFilePath { get; private set; }

        public override IList<FileBase> Files
        {
            get
            {
                var files = new List<string>() { StandardFilePath, LdpiFilePath, MdpiFilePath, HdpiFilePath, HdpiFilePath, XhdpiFilePath, XxhdpiFilePath, XxxhdpiFilePath };
                return files.Where(f => !string.IsNullOrEmpty(f)).Select(f => new FileAndroid(f)).ToList<FileBase>();              
            }
        }

        public AssetAndroid(string filePath) : base(ExtractIdentifierFromFilePath(filePath))
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

        public override Result<IList<Condition>> Analize()
        {
            var assetConditions = new List<AssetCondition>()
            {
                new AllFilesAndroidCondition(this),
                new SizesFilesAndroidCondition(this)
            };

            IList<Condition> conditions = assetConditions.SelectMany(a => a.Verify()).ToList();

            return new Result<IList<Condition>>(conditions.All(a => a.IsFulfilled), conditions);
        }

        private void CheckAndSetFilePath(string filePath)
        {
            string lastFolderName = Path.GetFileName(Path.GetDirectoryName(filePath));
            switch(lastFolderName)
            {
                case "drawable":
                    this.StandardFilePath = filePath;
                    break;
                case "drawable-ldpi":
                    this.LdpiFilePath = filePath;
                    break;
                case "drawable-mdpi":
                    this.MdpiFilePath = filePath;
                    break;
                case "drawable-hdpi":
                    this.HdpiFilePath = filePath;
                    break;
                case "drawable-xhdpi":
                    this.XhdpiFilePath = filePath;
                    break;
                case "drawable-xxhdpi":
                    this.XxhdpiFilePath = filePath;
                    break;
                case "drawable-xxxhdpi":
                    this.XxxhdpiFilePath = filePath;
                    break;
            }
        }

        private static string ExtractIdentifierFromFilePath(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }
    }
}
